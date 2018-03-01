using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Mods {
    /// <summary>
    /// The base class for gameplay modifiers.
    /// </summary>
    public abstract class Mod {
        /// <summary>
        /// The name of this mod.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The shortened name of this mod.
        /// </summary>
        public abstract string ShortenedName { get; }

        /// <summary>
        /// The icon of this mod.
        /// </summary>
        //public virtual FontAwesome Icon;

        /// <summary>
        /// The type of this mod.
        /// </summary>
        public ModType Type;

        /// <summary>
        /// The user readable description of this mod.
        /// </summary>
        public string Description;

        /// <summary>
        /// The score multiplier of this mod.
        /// </summary>
        public abstract double ScoreMultiplier { get; }

        /// <summary>
        /// Returns if this mod is ranked.
        /// </summary>
        public bool Ranked;

        /// <summary>
        /// The mods this mod cannot be enabled with.
        /// </summary>
        public Type[] IncompatibleMods;

        /// <summary>
        /// Whether we should allow failing at the current point in time.
        /// </summary>
        public bool AllowFail;
    }
}

