using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowContainer<T> : Newable 
    where T : Drawable{

    public List<T> Drawables {
        private set; get;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void Add(T drawable) {
        Drawables.Add(drawable);
    }
}
