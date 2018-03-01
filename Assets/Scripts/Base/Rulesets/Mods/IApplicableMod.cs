using Base.Rulesets.Objects;
using Base.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Mods {

    /// <summary>
    /// An interface for mods that are applied to a RulesetContainer.
    /// </summary>
    /// <typeparam name="TObject">The type of HitObject the RulesetContainer contains.</typeparam>
    public interface IApplicableMod<TObject>
        where TObject : HitObject {
        /// <summary>
        /// Applies the mod to a RulesetContainer.
        /// </summary>
        /// <param name="rulesetContainer">The RulesetContainer to apply the mod to.</param>
        void ApplyToRulesetContainer(RulesetContainer<TObject> rulesetContainer);
    }
    
}