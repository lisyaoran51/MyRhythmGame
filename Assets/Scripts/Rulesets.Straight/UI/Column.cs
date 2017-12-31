using Base.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.SheetMusics.ControlPoints;
using Base.Rulesets.Straight.Rulesets.Objects;

namespace Base.Rulesets.Straight.UI {
    public class Column : ScrollingPlayField<StraightHitObject> {
        public Pitch Pitch { get; set; }

        public int ColumnNum;

        protected void construct(Pitch pitch, int columnNum, float visibleTimeRange) {
            construct(Axes.Y);
            Pitch = pitch;
            ColumnNum = columnNum;
            VisibleTimeRange = visibleTimeRange;


        }

        protected new void Update() {
            base.Update();
        }

        /// <summary>
        /// 將timing Points加入本身的speed Adjust中，如果是音符的timing point，就要看這個timing point是不是這個column的pitch
        /// ，式的話再加進去
        /// </summary>
        /// <param name="controlPoint"></param>
        public override void ApplySpeedAdjustment(ControlPoint controlPoint) {
            StraightTimingControlPoint s = controlPoint as StraightTimingControlPoint;
            /*
             * 如果pitch相同，才加進去
             */
            if (s == null) {
                HitObjects.AddSpeedAdjustment(CreateSpeedAdjustmentContainer(controlPoint));
            } else if(s.Pitch == Pitch)
                HitObjects.AddSpeedAdjustment(CreateSpeedAdjustmentContainer(controlPoint));
            
            if (NestedScrollingPlayField.Count > 0)
                NestedScrollingPlayField.ForEach(p => p.ApplySpeedAdjustment(controlPoint));
        }

        public new void Add(DrawableHitObject hitObject) {
            HitObjects.Add(hitObject);
        }
        


    }
}