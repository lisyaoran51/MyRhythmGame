﻿using System;
using System.Collections;
using System.Collections.Generic;
using Base.Rulesets.Straight;
using Base.Rulesets.Straight.Rulesets.Objects;
using UnityEngine;

namespace Base.Rulesets.Straight.Rulesets.Objects.Drawables {
    class DrawableHoldNote : DrawableStraightHitObject<HoldNote> {
        protected new void construct(HoldNote hitObject) {
            base.construct(hitObject);
        }
    }
}
