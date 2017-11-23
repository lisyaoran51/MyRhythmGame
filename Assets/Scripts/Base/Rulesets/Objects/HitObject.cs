using System;
using System.Collections;
using System.Collections.Generic;
using Base.Sheetmusics;
using UnityEngine;
using Base.Utils;

namespace Base.Rulesets.Objects {
    public class HitObject : ChildAddable {

        public double StartTime;

        public SampleInfoList Samples { get; internal set; }

        public void ApplyDefaults(ControlPointInfo controlPointInfo, SheetmusicDifficulty baseDifficulty) {

        }
    }
}