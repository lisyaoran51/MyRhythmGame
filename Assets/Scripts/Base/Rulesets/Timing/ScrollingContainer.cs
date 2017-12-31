using Base.Rulesets.Objects.Drawables;
using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Timing {
    public abstract class ScrollingContainer<TObject> : Updated 
        where TObject : ScrollingHitObject
    {
        public int DebugCount;


        public ControlPoint ControlPoint;
        public float VisibleTimeRange;
        public DrawableScrollingHitObject<TObject> ScrollingHitObject;

        /// <summary>
        /// The axes through which this <see cref="ScrollingContainer"/> scrolls. This is set by the <see cref="SpeedAdjustmentContainer"/>.
        /// </summary>
        public Axes ScrollingAxes;

        public ScrollingContainer(ControlPoint controlPoint) {
            ControlPoint = controlPoint;
        }

        public virtual void Add(DrawableScrollingHitObject<TObject> hitObject) {
            ScrollingHitObject = hitObject;
        }

    }
}