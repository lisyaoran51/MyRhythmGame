using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewConfig : IViewConfig {

    protected Dictionary<string, IView> views;

    public bool Config(Drawable drawable) {
        if (views[drawable.DrawableName] != null)
            return views[drawable.DrawableName].Config(drawable);
        else return false;
        // throw...
    }
}
