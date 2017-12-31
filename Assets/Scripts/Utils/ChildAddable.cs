using Base.Utils.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace Base.Utils {
    public class ChildAddable : Loadable, IAddChild {

        private event Action<ChildAddable> onAdd;

        protected new void construct() {
            base.construct();
            onAdd += resetTransform;
        }

        public void AddChild(ChildAddable child) {
            child.transform.parent = transform;

            onAdd(child);

            /*
             * 這邊的意思跟 Load<child.type>(child); 一樣，只是c#不支援動態的T
             */
            Type genericType = Assembly.GetExecutingAssembly().GetType(child.GetType().ToString());
            typeof(ChildAddable)
                .GetMethod("Load")
                .MakeGenericMethod(genericType)
                .Invoke(this, new object[] {child});

            
        }

        private void resetTransform(ChildAddable child) {
            child.transform.localPosition = new Vector2(0, 0);
        }

    }
}