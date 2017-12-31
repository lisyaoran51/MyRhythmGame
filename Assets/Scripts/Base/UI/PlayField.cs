using Base.Rulesets.Objects.Drawables;
using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.UI {
    public class PlayField : Drawable {

        public HitObjectContainer HitObjects;

        protected new void Update() {
            base.Update();
        }

        /// <summary>
        /// Triggered when a new <see cref="Judgement"/> occurs on a <see cref="DrawableHitObject"/>.
        /// </summary>
        /// <param name="judgedObject">The object that <paramref name="judgement"/> occured for.</param>
        /// <param name="judgement">The <see cref="Judgement"/> that occurred.</param>
        public virtual void OnJudgement(DrawableHitObject judgedObject, Judgement judgement) { }

        /// <summary>
        /// Adds a DrawableHitObject to this Playfield.
        /// </summary>
        /// <param name="h">The DrawableHitObject to add.</param>
        public virtual void Add(DrawableHitObject h) {
            HitObjects.Add(h);
        }

        /// <summary>
        /// Performs post-processing tasks (if any) after all DrawableHitObjects are loaded into this Playfield.
        /// </summary>
        public virtual void PostProcess() { }
    }
}