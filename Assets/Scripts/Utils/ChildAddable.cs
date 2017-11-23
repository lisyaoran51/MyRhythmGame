using Base.Utils.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

namespace Base.Utils {
    public class ChildAddable : Loadable, IAddChild {
        public void AddChild(ChildAddable child) {
            child.transform.parent = transform;

            /*
             * 這邊的意思跟 Load<child.type>(child); 一樣，只是c#不支援動態的T
             */
            Type genericType = Assembly.GetExecutingAssembly().GetType(child.GetType().ToString());
            typeof(ChildAddable)
                .GetMethod("Load")
                .MakeGenericMethod(genericType)
                .Invoke(this, new object[] {child});
        }
    }
}