using Base.Rulesets.Mods;
using Base.Rulesets.Straight.Rulesets.Objects;
using Base.Rulesets.Straight.UI;
using Base.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Base.Rulesets.Straight.Mods {
    public class StraightModFlowOut : Mod, IApplicableMod<StraightHitObject>
    {
        public override string Name {
            get {
                return "Flow Out";
            }
        }

        public override double ScoreMultiplier {
            get {
                return 1.0;
            }
        }

        public override string ShortenedName {
            get {
                return "FO";
            }
        }

        public void ApplyToRulesetContainer(RulesetContainer<StraightHitObject> rulesetContainer) {

            /* In this moment, the speed adjustments had been added.
             * We change the configuration of this ruleset container, then it would generate speed adjustments
             * with this configuration itself.
             */
            StraightRulesetContainer src = rulesetContainer as StraightRulesetContainer;

            if(src != null) {
                src.IsModFlowOut = true;
            } else {
                throw new InvalidCastException("Failed to cast ruleset container to straight ruleset container in mode flow out.");
            }

            /*
            // We have to generate one speed adjustment per hit object for gravity
            foreach (ManiaHitObject obj in rulesetContainer.Objects.OfType<ManiaHitObject>()) {
                MultiplierControlPoint controlPoint = rulesetContainer.CreateControlPointAt(obj.StartTime);
                // Beat length has too large of an effect for gravity, so we'll force it to a constant value for now
                controlPoint.TimingPoint.BeatLength = 1000;

                hitObjectTimingChanges[obj.Column].Add(new ManiaSpeedAdjustmentContainer(controlPoint, ScrollingAlgorithm.Gravity));
            }

            // Like with hit objects, we need to generate one speed adjustment per bar line
            foreach (DrawableBarLine barLine in rulesetContainer.BarLines) {
                var controlPoint = rulesetContainer.CreateControlPointAt(barLine.HitObject.StartTime);
                // Beat length has too large of an effect for gravity, so we'll force it to a constant value for now
                controlPoint.TimingPoint.BeatLength = 1000;

                barlineTimingChanges.Add(new ManiaSpeedAdjustmentContainer(controlPoint, ScrollingAlgorithm.Gravity));
            }
            */
        }
    }
}
