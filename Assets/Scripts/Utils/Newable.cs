using Base.Utils.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Base.Utils {
    public class Newable : MonoBehaviour, INewable {


        public GameObject button;


        public bool IsConstructed { get { return constructState >= ConstructState.Constructed; } }

        private volatile ConstructState constructState = ConstructState.NotConstructed;

        private delegate object ObjectActivator(object instance, object[] param);

        private Dictionary<Type, ObjectActivator> activators = new Dictionary<Type, ObjectActivator>();

        protected void construct() {
            // TODO: 之後要調整成所有construct都呼叫完才變成ready，所以可能是
            // 在construct之前呼叫一次，全部Construct完再呼叫一次
            constructState = ConstructState.Ready;
        }


        // 會抓到很多construct，要比對parameter
        private MethodInfo getConstructor(Type type, object[] param) {
            /*
             * 要把抓到的construct比對輸入參數是否一樣多
             */
            int paramLength = param != null ? param.Length : 0;
            Type[] paramType = new Type[paramLength];
            for(int i = 0; i < paramLength; i++) {
                paramType[i] = param[i].GetType();
            }

            return type.GetMethod("construct", BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                paramType,
                null);
        }

        private void registerConstruct(Type type, object[] param)
        {
            /*
             * 找出最外層的construct並執行
             */
            var constructMethod = new KeyValuePair<MethodInfo, int>();
            MethodInfo construct = getConstructor(type, param);
            if (construct != null) {
                int paramLen = construct.GetParameters().Length;
                constructMethod = new KeyValuePair<MethodInfo, int>(construct, paramLen);
            } else
                throw new NotImplementedException("Failed to construct " + type.Name + ". constructor not implemented.");

            /*
             * 把每個constructor轉成一串action
             * *更新 改成紙轉換一個
             */
            MethodInfo met = constructMethod.Key;
            int len = constructMethod.Value;

            var constructAction = new Action<object, object[]>((_instance, _param) => {
                var thisParam = new object[len];
                if(_param != null)
                    Array.Copy(_param, 0, thisParam, 0, len);
                met.Invoke(_instance, thisParam);
            });
            /*
             * 把每個action都執行一遍，注意 雖然activator存在父object中，但是invoke的對象是子object，
             * 因此執行construct的是子object
             */
            activators[type] = (_instance, _param) => {
                constructAction(_instance, _param);
                return _instance;
            };
        }

        public T New<T>(object[] param, string name = null)
            where T : Newable {
            /* 
             * 把要使用的腳本加入新產生的gameObject中，
             * 然後return出去
             */
            GameObject newObject = new GameObject();
            name = name ?? typeof(T).ToString();
            newObject.name = name.Split('.').Last();
            T script = newObject.AddComponent<T>();

            Construct(script, param);
            
            return script;
        }

        // pass type to generic method: https://stackoverflow.com/questions/1606966/generic-method-executed-with-a-runtime-type
        // problem: 忘記為什麼不直接呼叫 New<T>，改用New<T>
        /*
        public INewable New( string script, object[] param, string name = null) {
            Type type = Assembly.GetExecutingAssembly().GetType(script);
            type = type.MakeGenericType(type);
            return (INewable)typeof(Newable)
                .GetMethod("New")
                .MakeGenericMethod(type)
                .Invoke(this, new object[] { param, name });
        }
        */

        public void Construct<T>(T instance, object[] param)
            where T : Newable
        {
            if (instance.IsConstructed)
                return;
            /* 
             * 註冊這個腳本的constructor，然後照順序執行
             */
            registerConstruct(typeof(T), param);

            ObjectActivator activator;

            if (!activators.TryGetValue(typeof(T), out activator))
                throw new InvalidOperationException(typeof(T).Name + " Constructor failed.");

            activator(instance, param);
        }

        /// <summary>
        /// 跟LoadAsync是ㄧ樣的意思，就是在需要用的時候才construct
        /// </summary>
        /// <param name="param"></param>
        public void LazyConstruct(object[] param = null) {
            switch (constructState) {
                case ConstructState.Ready:
                case ConstructState.Constructed:
                    return;
                case ConstructState.Constructing:
                    break;
                case ConstructState.NotConstructed:
                    constructState = ConstructState.Constructing;
                    break;
            }
            /*
             * 這邊的意思跟 Load<child.type>(child); 一樣，只是c#不支援動態的T
             */
            Type genericType = Assembly.GetExecutingAssembly().GetType(this.GetType().ToString());
            typeof(ChildAddable)
                .GetMethod("Construct")
                .MakeGenericMethod(genericType)
                .Invoke(this, new object[] { this, param });
        }
    }


    /// <summary>
    /// Possible states of a <see cref="Drawable"/> within the loading pipeline.
    /// </summary>
    public enum ConstructState {
        /// <summary>
        /// Not loaded, and no load has been initiated yet.
        /// </summary>
        NotConstructed,
        /// <summary>
        /// Currently loading (possibly and usually on a background
        /// thread via <see cref="Drawable.LoadAsync(Game, Drawable, Action)"/>).
        /// </summary>
        Constructing,
        /// <summary>
        /// Loading is complete, but has not yet been finalized on the update thread
        /// (<see cref="Drawable.LoadComplete"/> has not been called yet, which
        /// always runs on the update thread and requires <see cref="Drawable.IsAlive"/>).
        /// </summary>
        Ready,
        /// <summary>
        /// Loading is fully completed and the Drawable is now part of the scene graph.
        /// </summary>
        Constructed
    }
}