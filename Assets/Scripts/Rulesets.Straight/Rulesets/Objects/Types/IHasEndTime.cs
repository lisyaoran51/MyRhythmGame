using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.Rulesets.Objects.Types {
    public interface IHasEndTime {
        double Duration { get; }
        double EndTime { get; }

    }
}