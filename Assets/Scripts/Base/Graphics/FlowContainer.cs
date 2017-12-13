using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Graphics {
    public abstract class FlowContainer<T> : ChildAddable
        where T : Drawable {

        public List<T> Drawables {
            private set; get;
        }

        public FlowContainer() {
            Drawables = new List<T>();
        }

        internal void Add(T drawable) {
            Drawables.Add(drawable);
        }
    }
}