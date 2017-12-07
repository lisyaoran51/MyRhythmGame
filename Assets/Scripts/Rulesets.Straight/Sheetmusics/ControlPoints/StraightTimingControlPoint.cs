using Base.Rulesets.Straight.Rulesets.Objects.Types;
using Base.Sheetmusics.ControlPoints;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.SheetMusics.ControlPoints {
    public class StraightTimingControlPoint : TimingControlPoint, IHasColumn {

        public int Column {
            protected set; get;
        }

    }
}