using Base.Rulesets.Objects;
using Base.Rulesets.Straight.Sheetmusics.Patterns;
using Base.Sheetmusics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Base.Rulesets.Sheetmusics.Patterns {
    public abstract class PatternGenerator {

        /// <summary>
        /// The number of columns available to create the pattern.
        /// </summary>
        protected readonly int AvailableColumns;

        /// <summary>
        /// The last pattern.
        /// </summary>
        protected readonly Pattern PreviousPattern;

        /// <summary>
        /// The hit object to create the pattern for.
        /// </summary>
        protected readonly HitObject HitObject;

        /// <summary>
        /// The beatmap which <see cref="HitObject"/> is a part of.
        /// </summary>
        protected readonly Sheetmusic Sheetmusic;

        protected PatternGenerator(HitObject hitObject, Sheetmusic sheetmusic, int availableColumns, Pattern previousPattern) {
            if (hitObject == null) throw new ArgumentNullException("PatternGenerator gets null hitObject");
            if (sheetmusic == null) throw new ArgumentNullException("PatternGenerator gets null sheetmusic");
            if (availableColumns <= 0) throw new ArgumentOutOfRangeException("PatternGenerator gets negative availableColumns");
            if (previousPattern == null) throw new ArgumentNullException("PatternGenerator gets null previousPattern");

            HitObject = hitObject;
            Sheetmusic = sheetmusic;
            AvailableColumns = availableColumns;
            PreviousPattern = previousPattern;
        }

        /// <summary>
        /// Generates the pattern for <see cref="HitObject"/>, filled with hit objects.
        /// </summary>
        /// <returns>The <see cref="Pattern"/> containing the hit objects.</returns>
        public abstract Pattern Generate();
    }
}