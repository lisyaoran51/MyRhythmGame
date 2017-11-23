using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using System;
using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.UI;

namespace Base.UI {
    public abstract class ScrollingPlayField : PlayField {


        public double VisibleTimeRange = 1000;

        internal new ScrollingHitObjectContainer HitObjects;

        private List<ScrollingPlayField> nestedScrollingPlayField;

        public List<ScrollingPlayField> NestedScrollingPlayField;

        private void construct() 
        {
            // HitObjectContainer
            HitObjects = New<ScrollingHitObjectContainer>(null, "HitObjects");
            HitObjects.VisibleTimeRange = VisibleTimeRange;
        }

        // when removing SpeedAdjustContainer, add hitobjects back to default SpeedAdjustContainer
        public abstract void Add(DrawableHitObject hitObject);
    }
}