using Base.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Rulesets.Objects.Drawables;

namespace Base.Rulesets.Straight.UI {
    public class Column : ScrollingPlayField {

        public override void Add(DrawableHitObject hitObject) {
            HitObjects.Add(hitObject);
        }

        private void construct(double visibleTimeRange) {
            VisibleTimeRange = visibleTimeRange;
        }
    }
}