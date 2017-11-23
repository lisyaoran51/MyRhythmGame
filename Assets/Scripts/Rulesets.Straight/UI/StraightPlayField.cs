using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.Rulesets.Objects;
using Base.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Base.Rulesets.Straight.UI {
    public class StraightPlayField : ScrollingPlayField {

        public int ColumnCount;

        public FlowContainer<Column> ColumnArranger;

        public List<Column> Columns;

        private void construct(int availableColumns) {
            
            // Column
            ColumnCount = availableColumns;

            for (int i = 0; i < ColumnCount; i++) {
                Column c = New<Column>(new object[] { VisibleTimeRange }, "Column" + Columns.Count);
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
        }
    }
}