using System;
using System.Collections;
using System.Collections.Generic;
using Base.Rulesets.Straight;
using Base.Rulesets.Straight.Rulesets.Objects;
using UnityEngine;

public class DrawableNote : DrawableStraightHitObject<StraightHitObject> {

    protected new void construct(Note hitObject, int spriteIndex = 0) {
        base.construct(hitObject, spriteIndex);

        Pitch = hitObject.Pitch;
        HitObject = hitObject;
    }
}
