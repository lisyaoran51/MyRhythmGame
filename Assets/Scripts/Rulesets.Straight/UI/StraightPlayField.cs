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

namespace Base.Rulesets.Straight.UI {
    public class StraightPlayField : ScrollingPlayField {

        public Pitch StartPitch;

        public int ColumnCount;

        public FlowContainer<Column> ColumnArranger = new FillFlowContainer<Column>();

        public List<Column> Columns = new List<Column>();

        private void construct() {
        }

        private void load(StraightConfigManager configManager) {

            StartPitch = configManager.Get<Pitch>(StraightSetting.StartPitch);
            // Column
            ColumnCount = configManager.Get<int>(StraightSetting.availableColumns); ;

            for (int i = 0; i < ColumnCount; i++) {

                Column c = New<Column>(new object[] { StartPitch + i, VisibleTimeRange }, "Column " + (StartPitch + i));
                AddChild(c);
                ColumnArranger.Add(c);
                Columns.Add(c);
                addNestedPlayField(c);
            }
        }

        private void addNestedPlayField(Column c) {
            NestedScrollingPlayField.Add(c);
        }

        public override void Add(DrawableHitObject h) {
            Columns[((StraightHitObject)h.HitObject).Column].Add(h);
            Columns[((StraightHitObject)h.HitObject).Column].AddChild(h);
        }
    }
}