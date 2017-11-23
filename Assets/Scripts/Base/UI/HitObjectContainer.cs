using Base.Rulesets.Objects.Drawables;
using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.UI {
    public class HitObjectContainer : ChildAddable {

        protected List<DrawableHitObject> hitObjects;


        /// <summary>在playField loadObject時加入hitObject的圖</summary>
        public virtual void Add(DrawableHitObject hitObject) {
            hitObjects.Add(hitObject);
        }
    }
}