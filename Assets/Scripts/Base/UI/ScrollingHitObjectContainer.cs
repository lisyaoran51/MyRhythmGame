using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using Base.Rulesets.Objects.Drawables;
using System;
using System.Linq;
using Base.Rulesets.Timing;

namespace Base.UI {
    public class ScrollingHitObjectContainer : HitObjectContainer {

        public float VisibleTimeRange;

        protected new List<DrawableScrollingHitObject<ScrollingHitObject>> hitObjects;

        private List<SpeedAdjustmentContainer> speedAdjustmentContainers;

        public List<SpeedAdjustmentContainer> SpeedAdjustmentContainers;

        private SpeedAdjustmentContainer defaultSpeedAdjustmentContainer;

        private Axes scrollingAxes;

        public void AddSpeedAdjustment(SpeedAdjustmentContainer speedAdjustmentContainer) {
            speedAdjustmentContainer.VisibleTimeRange = VisibleTimeRange;
            AddChild(speedAdjustmentContainer);
        }

        private void construct() {
            SpeedAdjustmentContainers = new List<SpeedAdjustmentContainer>();
        }

        public override void Add(DrawableHitObject hitObject) {
            adjustmentContainerAt(hitObject.HitObject.StartTime).Add(hitObject);
        }

        private SpeedAdjustmentContainer adjustmentContainerAt(double startTime) {
            return speedAdjustmentContainers.FindAll(delegate (SpeedAdjustmentContainer s) {
                if (s.ControlPoint.StartTime == startTime) return true;
                else return false;
            }).FirstOrDefault<SpeedAdjustmentContainer>();

        }
    }
}