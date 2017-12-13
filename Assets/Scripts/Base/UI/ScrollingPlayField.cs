using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using System;
using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.UI;

namespace Base.UI {
    public abstract class ScrollingPlayField : PlayField {


        public float VisibleTimeRange = 1000f;

        internal new ScrollingHitObjectContainer HitObjects;

        private List<ScrollingPlayField> nestedScrollingPlayField;

        public List<ScrollingPlayField> NestedScrollingPlayField;

        private void construct() 
        {
            // HitObjectContainer
            HitObjects = New<ScrollingHitObjectContainer>(null, "HitObjects");
            HitObjects.VisibleTimeRange = VisibleTimeRange;
            AddChild(HitObjects);
            NestedScrollingPlayField = new List<ScrollingPlayField>();
        }

        // when removing SpeedAdjustContainer, add hitobjects back to default SpeedAdjustContainer
        public abstract void Add(DrawableHitObject hitObject);
    }
}