using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewConfig : IViewConfig {

    private Dictionary<string, IView> views;

    public ViewConfig() {
        views = new Dictionary<string, IView>();
    }

    public void Config(Drawable drawable) {
        IView view;
        if (!views.TryGetValue(drawable.DrawableName, out view))
            throw new InvalidOperationException(drawable.DrawableName + " ViewConfig not found.");

        view.Config(drawable);
    }

    public ViewConfig Set(string name, IView view) {
        views.Add(name, view);
        return this;
    }
}
