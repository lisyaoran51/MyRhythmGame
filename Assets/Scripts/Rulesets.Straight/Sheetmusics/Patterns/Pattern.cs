using Base.Rulesets.Straight.Rulesets.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.Sheetmusics.Patterns {
    public class Pattern {

        private readonly List<StraightHitObject> hitObjects = new List<StraightHitObject>();

        /// <summary>
        /// All the hit objects contained in this pattern.
        /// </summary>
        public IEnumerable<StraightHitObject> HitObjects {
            get { return hitObjects; }
        }

        /// <summary>
        /// Adds a hit object to this pattern.
        /// </summary>
        /// <param name="hitObject">The hit object to add.</param>
        public void Add(StraightHitObject hitObject) {
            hitObjects.Add(hitObject);
        }
    }

    
}