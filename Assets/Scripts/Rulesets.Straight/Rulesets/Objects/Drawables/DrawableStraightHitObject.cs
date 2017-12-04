using Base.Rulesets.Straight;
using Base.Rulesets.Straight.Rulesets.Objects.Drawables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Rulesets.Straight.Rulesets.Objects;
using System;
using Base.Rulesets.Objects.Drawables;

public class DrawableStraightHitObject<TObject> : DrawableScrollingHitObject<StraightHitObject> 
    where TObject : StraightHitObject
{

    public Pitch Pitch;

    public new TObject HitObject;

    private void construct(TObject hitObject, Pitch pitch) {
        Pitch = pitch;
        HitObject = hitObject;
    }
}
