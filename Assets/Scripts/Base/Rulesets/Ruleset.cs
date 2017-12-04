using Base.Sheetmusics;
using Base.UI;
using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets {
    public abstract class Ruleset {
        

        public RulesetInfo RulesetInfo;

        public Ruleset(RulesetInfo rulesetInfo) {
            RulesetInfo = rulesetInfo;
        }
        

        public abstract RulesetContainer CreateRulesetContainerWith(WorkingSheetmusic workingSheetmusic, bool isForCurrentRuleset);

    }
}