using System;
using System.Collections;
using System.Collections.Generic;
using Base.Rulesets.Straight;
using Base.Rulesets.Straight.Rulesets.Objects;
using UnityEngine;

public class DrawableNote : DrawableStraightHitObject<Note> {

    internal new DrawableNote construct(Note hitObject, Pitch pitch) {
        base.construct(hitObject, pitch);
        return this;
    }
}
