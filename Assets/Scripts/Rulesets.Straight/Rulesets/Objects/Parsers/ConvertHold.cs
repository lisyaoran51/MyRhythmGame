using Base.Rulesets.Objects;
using Base.Rulesets.Straight.Rulesets.Objects.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.Rulesets.Objects.Parsers {
    public class ConvertHold : HitObject, IHasColumn {
        public int Column { get; internal set; }
        public double EndTime { get; internal set; }

    }
}