using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Objects.Drawables {

    public class DrawableHitObject<TObject> : DrawableHitObject 
        where TObject : HitObject
    {

        public new TObject HitObject {
            protected set { base.HitObject = value; }
            get { return (TObject)base.HitObject; }
        }

        public event Action<DrawableHitObject, Judgement> OnJudgement;

        protected void construct(TObject hitObject, int spriteIndex = 0) {
            construct(spriteIndex);
            HitObject = hitObject;
        }
    }

    public class DrawableHitObject : Drawable {

        public HitObject HitObject;
        
    }

    
}