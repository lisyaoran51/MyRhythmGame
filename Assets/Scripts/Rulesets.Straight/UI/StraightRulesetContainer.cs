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

        public bool IsModFlowOut = false;

        protected sealed override PlayField CreatePlayfield() {
            return New<StraightPlayField>(new object[] {IsModFlowOut});
        }

        protected override SheetmusicConverter<StraightHitObject> CreateSheetmusicConverter() {
            //if (IsForCurrentRuleset)
                //AvailableColumns = WorkingSheetmusic.SheetmusicInfo.BaseDifficulty.AvailableColumns;
            return new StraightSheetmusicConverter(IsForCurrentRuleset, AvailableColumns);
        }

        protected override DrawableHitObject<StraightHitObject> GetVisualRepresentation(StraightHitObject h) {
            Note note = h as Note;
            HoldNote holdNote = h as HoldNote;
            string noteName = "Note" + h.StartBar;
            if (note != null) {
                int spriteIndex = note.IsBlackKey() ? 1 : 0;
                return New<DrawableNote>(new object[] { note, spriteIndex }, noteName);
            }
            if (holdNote != null) {

                return null;//New<DrawableHoldNote>(new object[] { note, pitch }, noteName);
            }
            return null;
        }
    }
}