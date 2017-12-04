using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Objects.Drawables {

    public class DrawableHitObject<TObject> : DrawableHitObject 
        where TObject : HitObject
    {

        public new TObject HitObject;

        public event Action<DrawableHitObject, Judgement> OnJudgement;

        private void construct(TObject hitObject) {
            HitObject = hitObject;
        }
    }

    public class DrawableHitObject : Drawable {

        public HitObject HitObject;
        
    }

    
}