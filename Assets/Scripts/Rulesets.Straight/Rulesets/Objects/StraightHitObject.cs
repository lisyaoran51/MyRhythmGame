using Base.Rulesets.Straight.Rulesets.Objects.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.Rulesets.Objects {
    public class StraightHitObject : ScrollingHitObject, IHasColumn {

        public double StartTime;

        public double StartBar {
            private set; get;// TODO:StartBar.get
        }

        public virtual int Column { get; set; }

        public SampleInfoList Samples;
    }
}