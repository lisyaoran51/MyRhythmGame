using System;
using System.Collections;
using System.Collections.Generic;
using Base.Rulesets.Objects.Drawables;
using UnityEngine;
using Base.Utils;

namespace Base.Rulesets.Timing {
    public class SpeedAdjustmentContainer<TObject> : Updated 
        where TObject : ScrollingHitObject 
    {

        public float VisibleTimeRange = 4f;

        public ControlPoint ControlPoint;

        //public DrawableScrollingHitObject<TObject> ScrollingHitObject;

        /// <summary>
        /// The axes which the content of this container will scroll through.
        /// </summary>
        public Axes ScrollingAxes {
            get { return scrollingContainer.ScrollingAxes; }
            set { scrollingContainer.ScrollingAxes = value; }
        }

        public ScrollingContainer<TObject> ScrollingContainer {
            get {
                return scrollingContainer;
            }
        }

        private ScrollingContainer<TObject> scrollingContainer;

        public SpeedAdjustmentContainer(ControlPoint controlPoint) {
            ControlPoint = controlPoint;
        }

        protected override void load() {
            scrollingContainer = CreateScrollingContainer();
            scrollingContainer.ControlPoint = ControlPoint;
            scrollingContainer.VisibleTimeRange = VisibleTimeRange;
        }

        protected override void update() {
            // no-op
        }
        
        private ScrollingContainer<TObject> CreateScrollingContainer() {
            return new LinearScrollingContainer<TObject>(ControlPoint) {
                Parent = this.Parent
            };
        }

        public void Add(DrawableScrollingHitObject<TObject> hitObject) {
            if(ScrollingContainer != null)
                ScrollingContainer.Add(hitObject);
            else {
                scrollingContainer = new LinearScrollingContainer<TObject>(ControlPoint);
                ScrollingContainer.Add(hitObject);
            }

        }
    }
}