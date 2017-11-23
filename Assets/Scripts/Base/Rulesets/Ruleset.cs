using Base.Sheetmusics;
using Base.UI;
using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ruleset : ChildAddable {

    public int ID;

    public RulesetInfo RulesetInfo;
    
    private void construct(RulesetInfo rulesetInfo) {
        RulesetInfo = rulesetInfo;
    }

    public abstract RulesetContainer CreateRulesetContainerWith(WorkingSheetmusic workingSheetmusic, bool isForCurrentRuleset);

}
