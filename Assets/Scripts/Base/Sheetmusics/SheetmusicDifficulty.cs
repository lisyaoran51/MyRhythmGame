using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Sheetmusics {
    public class SheetmusicDifficulty {

        public int AvailableColumns { get; set; }
        public float ApproachRate { get; internal set; }
        public float DrainRate { get; internal set; }
        public float OverallDifficulty { get; internal set; }
        public float SliderMultiplier { get; internal set; }
        public float SliderTickRate { get; internal set; }
    }
}