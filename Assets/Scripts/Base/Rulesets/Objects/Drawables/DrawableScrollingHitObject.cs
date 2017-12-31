using Base.Rulesets.Objects.Drawables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Rulesets.Straight.Rulesets.Objects;
using System;

namespace Base.Rulesets.Objects.Drawables {

    public class DrawableScrollingHitObject : DrawableScrollingHitObject<ScrollingHitObject> { }

    public class DrawableScrollingHitObject<TObject> : DrawableHitObject<TObject>
        where TObject : ScrollingHitObject
    {

        public double LifetimeStart;
        public double LifetimeEnd;

        
    }
}