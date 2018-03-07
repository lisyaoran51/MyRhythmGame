using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using System;
using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.UI;
using Base.Rulesets.Timing;

namespace Base.UI {
    public abstract class ScrollingPlayField<TObject> : PlayField 
        where TObject : ScrollingHitObject
    {


        public float VisibleTimeRange = 2.5f;

        public new ScrollingHitObjectContainer<TObject> HitObjects {
            get { return (ScrollingHitObjectContainer<TObject>)base.HitObjects; }
        }

        private List<ScrollingPlayField<TObject>> nestedScrollingPlayField;

        public List<ScrollingPlayField<TObject>> NestedScrollingPlayField = new List<ScrollingPlayField<TObject>>();

        protected void construct(Axes scrollingAxes) 
        {
            construct();


            // HitObjectContainer
            base.HitObjects = new ScrollingHitObjectContainer<TObject>(scrollingAxes) {
                VisibleTimeRange = this.VisibleTimeRange,
                Parent = this
            };
        }

        public virtual void ApplySpeedAdjustment(ControlPoint controlPoint) {
            HitObjects.AddSpeedAdjustment(CreateSpeedAdjustmentContainer(controlPoint));
            if (NestedScrollingPlayField.Count > 0)
                NestedScrollingPlayField.ForEach(p => p.ApplySpeedAdjustment(controlPoint));
        }

        protected SpeedAdjustmentContainer<TObject> CreateSpeedAdjustmentContainer(ControlPoint controlPoint) {
            return new SpeedAdjustmentContainer<TObject>(controlPoint);
        }


        public new void Update() {
            base.Update();
        }

        // when removing SpeedAdjustContainer, add hitobjects back to default SpeedAdjustContainer
        //public abstract void Add(DrawableHitObject hitObject);
    }
}