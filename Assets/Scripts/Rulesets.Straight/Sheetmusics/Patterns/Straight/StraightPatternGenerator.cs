using System.Collections;
using System.Collections.Generic;
using Base.Rulesets.Objects;
using Base.Sheetmusics;
using UnityEngine;
using Base.Rulesets.Sheetmusics.Patterns;
using Base.Rulesets.Straight.Sheetmusics.Patterns;

namespace Base.Rulesets.Sheetmusics.Patterns {
    public abstract class StraightPatternGenerator : PatternGenerator {

        protected StraightPatternGenerator(HitObject hitObject, Sheetmusic sheetmusic, int availableColumns, Pattern previousPattern)
             : base(hitObject, sheetmusic, availableColumns, previousPattern) {
        }
    }
}