using System;
using System.Collections;
using System.Collections.Generic;
using Base.Rulesets.Objects.Drawables;
using UnityEngine;
using Base.Utils;

namespace Base.Rulesets.Timing {
    public class SpeedAdjustmentContainer : ChildAddable {

        public float VisibleTimeRange;

        public ControlPoint ControlPoint;

        public DrawableScrollingHitObject<ScrollingHitObject> ScrollingHitObject;

        public ScrollingContainer ScrollingContainer {
            get {
                return scrollingContainer;
            }
        }

        private ScrollingContainer scrollingContainer;


        private void construct(ControlPoint controlPoint) {

            ControlPoint = controlPoint;
            scrollingContainer = CreateScrollingContainer();
            scrollingContainer.ControlPoint = controlPoint;
            scrollingContainer.VisibleTimeRange = VisibleTimeRange;
            AddChild(scrollingContainer);
        }

        private ScrollingContainer CreateScrollingContainer() {
            return New<LinearScrollingContainer>(new object[] { ControlPoint });
        }

        internal void Add(DrawableHitObject hitObject) {
            DrawableScrollingHitObject<ScrollingHitObject> s = (DrawableScrollingHitObject<ScrollingHitObject>)hitObject;
            ScrollingHitObject = s;
            ScrollingContainer.Add(s);
        }
    }
}