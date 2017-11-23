using Base.Rulesets.Objects.Drawables;
using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingContainer : ChildAddable {

    internal ControlPoint ControlPoint;
    internal double VisibleTimeRange;
    public DrawableScrollingHitObject<ScrollingHitObject> ScrollingHitObject;

    internal void Add(DrawableScrollingHitObject<ScrollingHitObject> hitObject) {
        ScrollingHitObject = hitObject;
        AddChild(hitObject);
    }
}
