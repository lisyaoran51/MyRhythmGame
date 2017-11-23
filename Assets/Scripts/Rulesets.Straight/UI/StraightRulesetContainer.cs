using Base.Rulesets.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.Rulesets.Objects;
using Base.Sheetmusics;
using Base.UI;
using System;

namespace Base.Rulesets.Straight.UI {

    public class StraightRulesetContainer : ScrollingRulesetContainer<StraightPlayField, StraightHitObject> {

        public int AvailableColumns;

        protected sealed override PlayField CreatePlayfield() {
            return New<StraightPlayField>(new object[] { AvailableColumns });
        }
        

        protected override DrawableHitObject<StraightHitObject> GetVisualRepresentation(StraightHitObject h) {
            Pitch pitch = PlayField.Columns[h.Column].Pitch;
            Note note = h as Note;
            string noteName = "Note" + h.StartBar;

            return New<DrawableNote>(new object[] { note, pitch }, noteName);
        }
    }
}