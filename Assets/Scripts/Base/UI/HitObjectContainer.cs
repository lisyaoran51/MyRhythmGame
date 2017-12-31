using Base.Rulesets.Objects.Drawables;
using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Base.UI {
    public class HitObjectContainer : Updated{

        protected List<DrawableHitObject> hitObjects;

        protected override void load() {
            // no-op
        }

        protected override void update() {
            // no-op
        }

        /// <summary>在playField loadObject時加入hitObject的圖</summary>
        public virtual void Add(DrawableHitObject hitObject) {
            hitObjects.Add(hitObject);
        }

        
    }
}