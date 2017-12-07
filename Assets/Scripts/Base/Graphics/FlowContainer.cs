using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowContainer<T> : ChildAddable 
    where T : Drawable{

    public List<T> Drawables {
        private set; get;
    }
    

    internal void Add(T drawable) {
        Drawables.Add(drawable);
    }
}
