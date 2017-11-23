using Base.Utils.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.Linq;

namespace Base.Utils {
    /// <summary>
    /// Loadable只能去load父或父父物件(以此類推)的資料，不能load sibling物件的資料
    /// </summary>
    public class Loadable : Cachable, ILoadable {

        public bool IsLoaded { get { return loadState >= LoadState.Loaded;  } }

        private volatile LoadState loadState;

        /// <summary>
        /// Describes the current state of this Drawable within the loading pipeline.
        /// </summary>
        public LoadState LoadState;

        private delegate object ObjectActivator(object instance);

        private Dictionary<Type, ObjectActivator> activators = new Dictionary<Type, ObjectActivator>();

        private MethodInfo getLoader(Type type) {
            return type.GetMethod("load");
        }

        private void registerLoad(Type type) {
            /*
             * 把每一個繼承類別的load取出來擺入list，然後做一個action可以把全部
             * load執行一遍
             * 每個load用的到的參數只有父物件與父父物件(以此類推)的Cache，寫load時參數必須是父或父父(以此類推)物件的cache包含
             */
            var loadMethods = new List<MethodInfo>();

            for (Type t = type; t != typeof(Newable); t = t.BaseType) {
                MethodInfo load = getLoader(type);
                if (load != null) {
                    loadMethods.Insert(0, load);
                }
            }
            /*
             * 把每個load轉成一串action
             */
            var loaders = loadMethods.Select(loader => {

                var parameters = loader.GetParameters().Select(p => p.ParameterType)
                                           .Select(t => new Func<object>(() => {
                                               var val = GetCache(t);
                                               if (val == null) {
                                                   throw new InvalidOperationException(
                                                       @"Type " + t.FullName + " is not registered, and is a dependency of " + type.FullName);
                                               }
                                               return val;
                                           })).ToList();

                return new Action<object>(instance => {
                    var p = parameters.Select(pa => pa()).ToArray();
                    loader.Invoke(instance, p);
                });
            }).ToList();
            /*
             * 把每個action都執行一遍，注意 雖然activator存在父object中，但是invoke的對象是子load，
             * 因此執行load的是子object
             */
            activators[type] = instance => {
                loaders.ForEach(load => load(instance));
                return instance;
            };
        }
        
        /// <summary>
        /// 會去執行這個物件的每一個load函數
        /// </summary>
        /// <typeparam name="T">這個物件的型別</typeparam>
        /// <param name="instance">要執行load的物件</param>
        public void Load<T>(T instance) {
            switch (loadState) {
                case LoadState.Ready:
                case LoadState.Loaded:
                    return;
                case LoadState.Loading:
                    break;
                case LoadState.NotLoaded:
                    loadState = LoadState.Loading;
                    break;
            }

            registerLoad(typeof(T));

            ObjectActivator activator;

            if (!activators.TryGetValue(typeof(T), out activator))
                throw new InvalidOperationException(typeof(T).Name + " loader failed.");

            activator(instance);
        }
    }
}

/// <summary>
/// Possible states of a <see cref="Drawable"/> within the loading pipeline.
/// </summary>
public enum LoadState {
    /// <summary>
    /// Not loaded, and no load has been initiated yet.
    /// </summary>
    NotLoaded,
    /// <summary>
    /// Currently loading (possibly and usually on a background
    /// thread via <see cref="Drawable.LoadAsync(Game, Drawable, Action)"/>).
    /// </summary>
    Loading,
    /// <summary>
    /// Loading is complete, but has not yet been finalized on the update thread
    /// (<see cref="Drawable.LoadComplete"/> has not been called yet, which
    /// always runs on the update thread and requires <see cref="Drawable.IsAlive"/>).
    /// </summary>
    Ready,
    /// <summary>
    /// Loading is fully completed and the Drawable is now part of the scene graph.
    /// </summary>
    Loaded
}