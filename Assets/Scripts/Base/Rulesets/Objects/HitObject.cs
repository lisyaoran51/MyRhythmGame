using System;
using System.Collections;
using System.Collections.Generic;
using Base.Sheetmusics;
using UnityEngine;
using Base.Utils;
using Base.Audio;

namespace Base.Rulesets.Objects {
    public class HitObject{

        public float StartTime;

        public SampleInfoList Samples { get; internal set; }

        public void ApplyDefaults(ControlPointInfo controlPointInfo, SheetmusicDifficulty baseDifficulty) {

        }
    }
}