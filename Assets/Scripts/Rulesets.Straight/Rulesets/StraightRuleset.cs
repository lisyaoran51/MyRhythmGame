using Base.Rulesets.Straight.UI;
using Base.Sheetmusics;
using Base.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.Rulesets {
    public class StraightRuleset : Ruleset {

        public override RulesetContainer CreateRulesetContainerWith(WorkingSheetmusic sheetMusic, bool isForCurrentRuleset) {
            return New<StraightRulesetContainer>(new object[] { sheetMusic, isForCurrentRuleset });
        }
    }
}