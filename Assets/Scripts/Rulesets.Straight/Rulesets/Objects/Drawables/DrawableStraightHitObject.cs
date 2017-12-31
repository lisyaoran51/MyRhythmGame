using Base.Rulesets.Straight;
using Base.Rulesets.Straight.Rulesets.Objects.Drawables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Rulesets.Straight.Rulesets.Objects;
using System;
using Base.Rulesets.Objects.Drawables;

public class DrawableStraightHitObject<TObject> : DrawableScrollingHitObject<TObject>
    where TObject : StraightHitObject
{

    public Pitch Pitch;

    public new TObject HitObject {
        protected set { base.HitObject = value; }
        get { return (TObject)base.HitObject; } }

    protected void construct(TObject hitObject, int spriteIndex = 0) {
        base.construct(hitObject, spriteIndex);

        Pitch = hitObject.Pitch;
        base.HitObject = hitObject;
    }
}
