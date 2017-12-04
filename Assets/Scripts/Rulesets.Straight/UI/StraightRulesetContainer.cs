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

        public Pitch StartPitch;
        public int AvailableColumns;


        protected sealed override PlayField CreatePlayfield() {
            return New<StraightPlayField>(new object[] { StartPitch, AvailableColumns });
        }

        protected override SheetmusicConverter<StraightHitObject> CreateSheetmusicConverter() {
            if (IsForCurrentRuleset)
                AvailableColumns = WorkingSheetmusic.SheetmusicInfo.BaseDifficulty.AvailableColumns;
            return new StraightSheetmusicConverter(IsForCurrentRuleset, AvailableColumns);
        }

        protected override DrawableHitObject<StraightHitObject> GetVisualRepresentation(StraightHitObject h) {
            Pitch pitch = PlayField.Columns[h.Column].Pitch;
            Note note = h as Note;
            HoldNote holdNote = h as HoldNote;
            string noteName = "Note" + h.StartBar;
            if (note != null) {

                return New<DrawableNote>(new object[] { note, pitch }, noteName);
            }
            if (holdNote != null) {

                return New<DrawableHoldNote>(new object[] { note, pitch }, noteName);
            }
            return null;
        }
    }
}