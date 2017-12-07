using Base.Utils.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils {
    public class Cachable : Newable, IGetParent, ICachable {

        private readonly Dictionary<Type, object> cache = new Dictionary<Type, object>();
        private readonly HashSet<Type> cacheable = new HashSet<Type>();


        /// <summary>
        /// 取得父object的腳本物件，限定每個物件只能有一個腳本
        /// </summary>
        /// <returns>有父物件腳本就回傳，沒有就null</returns>
        public Loadable GetParent() {
            var parent = transform.parent;
            if (parent == null) return null;

            var parents = parent.GetComponents<Loadable>() ?? new Loadable[0]; ;
            if (parents.Length > 1) {
                throw new InvalidOperationException(
                              @"Object " + this.name + "'s parent has more than 1 script.");
            } else
                return parents.Length == 1 ? parents[0] : null;
        }

        /// <summary>
        /// 根據type取出cache中存的遊戲暫存資料，如果沒有找到該資料就到父物件找
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetCache(Type type) {
            object cacheData;
            if (cache.TryGetValue(type, out cacheData))
                return cacheData;

            var parent = GetParent();
            if (parent != null)
                return parent.GetCache(type);
            else
                return null;
        }

        public void Cache(object o) {
            cache.Add(o.GetType(), o);
            cacheable.Add(o.GetType());
        }
    }
}