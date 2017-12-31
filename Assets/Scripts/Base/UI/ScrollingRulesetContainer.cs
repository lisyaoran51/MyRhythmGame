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
        where TObject : ScrollingHitObject
        where TPlayField : ScrollingPlayField<TObject>
    {


        private List<DrawableHitObject> Notes;

        

        private void load() {

            List<ControlPoint> allTimingPoints = new List<ControlPoint>();
            allTimingPoints.AddRange(Sheetmusic.ControlPointInfo.TimingControlPoints);

            allTimingPoints.ForEach(c => PlayField.ApplySpeedAdjustment(c));
        }



        /* 下面這段移到playfield裡面做
        // TODO: 每個column都放一組SpeedAdjustmentContainer會很耗效能
        private void applySpeedAdjustment(ControlPoint controlPoint, ScrollingPlayField playField) {
            playField.HitObjects.AddSpeedAdjustment(CreateSpeedAdjustmentContainer(controlPoint));
            if(playField.NestedScrollingPlayField.Count > 0)
                playField.NestedScrollingPlayField.ForEach(p => applySpeedAdjustment(controlPoint, p));
        }
        
        private SpeedAdjustmentContainer CreateSpeedAdjustmentContainer(ControlPoint controlPoint) {
            return new SpeedAdjustmentContainer (controlPoint);
        }
        */
    }
}