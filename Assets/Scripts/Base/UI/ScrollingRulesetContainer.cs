using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.UI;
using Base.Sheetmusics;
using Base.Rulesets.Timing;
using Base.Rulesets.Objects;

namespace Base.Rulesets.UI {
    public abstract class ScrollingRulesetContainer<TPlayField, TObject> : RulesetContainer<TPlayField, TObject>
        where TPlayField : ScrollingPlayField
        where TObject : HitObject
    {


        private List<DrawableHitObject> Notes;

        

        private void load() {

            List<ControlPoint> allTimingPoints = new List<ControlPoint>();
            allTimingPoints.AddRange(Sheetmusic.ControlPointInfo.TimingControlPoints);

            allTimingPoints.ForEach(c => applySpeedAdjustment(c, PlayField));
        }




        // TODO: 每個column都放一組SpeedAdjustmentContainer會很耗效能
        private void applySpeedAdjustment(ControlPoint controlPoint, ScrollingPlayField playField) {
            playField.HitObjects.AddSpeedAdjustment(CreateSpeedAdjustmentContainer(controlPoint));
            playField.NestedScrollingPlayField.ForEach(p => applySpeedAdjustment(controlPoint, p));
        }
        
        private SpeedAdjustmentContainer CreateSpeedAdjustmentContainer(ControlPoint controlPoint) {
            return New<SpeedAdjustmentContainer>(new object[] { controlPoint }, "SpeedAdjustment" + controlPoint.StartTime);
        }
        
    }
}