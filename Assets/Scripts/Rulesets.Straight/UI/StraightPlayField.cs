using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.Rulesets.Objects;
using Base.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Base.Rulesets.Straight.Configurations;
using Base.Configurations;
using Base.Graphics;
using System.Linq;
using Base.Rulesets.Straight.Rulesets.Objects.Parsers;
using Base.Rulesets.Straight.SheetMusics.ControlPoints;
using Base.Sheetmusics.ControlPoints;
using Base.Rulesets.Straight.Graphics;

namespace Base.Rulesets.Straight.UI {
    public class StraightPlayField : ScrollingPlayField<StraightHitObject> {

        public Pitch StartPitch;

        public int ColumnCount;

        public FlowContainer<Column> ColumnArranger = new StraightFillFlowContainer();

        public List<Column> Columns = new List<Column>();

        private event Action onLoadComplete;

        protected new void construct() {
            construct(Axes.Y);
            onLoadComplete += ColumnArranger.Update;
        }

        private void load(StraightConfigManager configManager) {

            StartPitch = configManager.Get<Pitch>(StraightSetting.StartPitch);
            // Column
            ColumnCount = configManager.Get<int>(StraightSetting.availableColumns); ;

            for (int i = 0; i < ColumnCount; i++) {

                Column c = New<Column>(new object[] { StartPitch + i, i, VisibleTimeRange }, "Column " + (StartPitch + i));
                AddChild(c);
                ColumnArranger.Add(c);
                Columns.Add(c);
                addNestedPlayField(c);
            }
            /*
             * 應該寫成自動在load之後執行
             */
            onLoadComplete();
        }

        public new void Update() {
            base.Update();
        }

        private void addNestedPlayField(Column c) {
            NestedScrollingPlayField.Add(c);
        }

        /// <summary>
        /// 將timingPoints加入本身的speedAdjust中，並且尋找下面的column，再把timingpoint所在的
        /// column加入這個timingpoint的speedAdjust
        /// </summary>
        /// <param name="controlPoint"></param>
        public override void ApplySpeedAdjustment(ControlPoint controlPoint) {
            /*
             * 本來是每個playfield全部都有一整組speed adjust，改成只有column有各自的speed adjust
             * TODO: 如果有其他timing point如變速、特效，應該也要加入
             */
            SpeedControlPoint s = controlPoint as SpeedControlPoint;
            if(s != null) {
                NestedScrollingPlayField.ForEach(p => p.ApplySpeedAdjustment(s));
            }
            else if (NestedScrollingPlayField.Count > 0)
                NestedScrollingPlayField.Where(p => ((Column)p).Pitch.Equals(((StraightTimingControlPoint)controlPoint).Pitch))
                    .First().ApplySpeedAdjustment(controlPoint);
        }

        public override void Add(DrawableHitObject h) {
            try {
                var column = Columns.Where(c =>
                    c.Pitch.Equals(((StraightHitObject)h.HitObject).Pitch)).First();
                column.Add(h);
                column.AddChild(h);
            }
            catch(InvalidOperationException e) {
                throw new InvalidOperationException("Failed to add note to column. Note's pitch: " + ((StraightHitObject)h.HitObject).Pitch + e);
            }
        }


    }
}