using Base.Utils.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Base.Utils {
    public class Newable : MonoBehaviour, INewable {


        private delegate object ObjectActivator(object instance, object[] param);

        private Dictionary<Type, ObjectActivator> activators = new Dictionary<Type, ObjectActivator>();

        // 目前寫法，一層class只能有一個construct
        private MethodInfo getConstructor(Type type) {
            return type.GetMethod("construct", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private void registerConstruct(Type type)
        {
            /*
             * 把每一個繼承類別的constructor取出來擺入list，然後做一個action可以把全部
             * constructor執行一遍
             * 每個constructor只看得到它需要的參數，寫constructor時參數必須照順序排
             */
            var constructMethods = new List<KeyValuePair<MethodInfo, int>>();

            for (Type t = type; t != typeof(MonoBehaviour); t = t.BaseType) {
                MethodInfo construct = getConstructor(t);
                if (construct != null) {
                    int paramLen = construct.GetParameters().Length;
                    constructMethods.Insert(0, new KeyValuePair<MethodInfo, int>(construct, paramLen));
                }
            }
            /*
             * 把每個constructor轉成一串action
             */
            var constructors = constructMethods.Select(constructor => {
                MethodInfo met = constructor.Key;
                int len = constructor.Value;

                return new Action<object, object[]>((_instance, _param) => {
                    var thisParam = new object[len];
                    if(_param != null)
                        Array.Copy(_param, 0, thisParam, 0, len);
                    met.Invoke(_instance, thisParam);
                });
            }).ToList();
            /*
             * 把每個action都執行一遍，注意 雖然activator存在父object中，但是invoke的對象是子object，
             * 因此執行construct的是子object
             */
            activators[type] = (_instance, _param) => {
                constructors.ForEach(construct => construct(_instance, _param));
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
            /* 
             * 註冊這個腳本的constructor，然後照順序執行
             */
            registerConstruct(typeof(T));

            ObjectActivator activator;

            if (!activators.TryGetValue(typeof(T), out activator))
                throw new InvalidOperationException(typeof(T).Name + " Constructor failed.");

            activator(script, param);

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


    }
}