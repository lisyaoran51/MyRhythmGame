using System;
using System.Collections;
using System.Collections.Generic;
using Base.Rulesets.Straight;
using Base.Rulesets.Straight.Rulesets.Objects;
using UnityEngine;

public class DrawableNote : DrawableStraightHitObject<Note> {

    private void construct(Note hitObject, Pitch pitch) {
        Pitch = pitch;
        HitObject = hitObject;
    }
}
