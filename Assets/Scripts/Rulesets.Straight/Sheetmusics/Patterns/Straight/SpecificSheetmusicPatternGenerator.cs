using Base.Rulesets.Objects;
using Base.Rulesets.Sheetmusics.Patterns;
using Base.Rulesets.Straight.Rulesets.Objects;
using Base.Rulesets.Straight.Rulesets.Objects.Types;
using Base.Sheetmusics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.Sheetmusics.Patterns.Straight {
    public class SpecificSheetmusicPatternGenerator : StraightPatternGenerator {

        public SpecificSheetmusicPatternGenerator(HitObject hitObject, Sheetmusic sheetmusic, int availableColumns, Pattern previousPattern)
                : base(hitObject, sheetmusic, availableColumns, previousPattern)
            {
        }

        public override Pattern Generate() {
            var endTimeData = HitObject as IHasEndTime;
            var positionData = HitObject as IHasColumn;

            int column = positionData.Column;

            var pattern = new Pattern();

            if (endTimeData != null) {
                pattern.Add(new HoldNote {
                    StartTime = HitObject.StartTime,
                    Duration = endTimeData.Duration,
                    Column = column,
                    Head = { Samples = sampleInfoListAt(HitObject.StartTime) },
                    Tail = { Samples = sampleInfoListAt(endTimeData.EndTime) },
                });
            } else if (positionData != null) {
                pattern.Add(new Note {
                    StartTime = HitObject.StartTime,
                    Samples = HitObject.Samples,
                    Column = column
                });
            }

            return pattern;
        }
    }
}