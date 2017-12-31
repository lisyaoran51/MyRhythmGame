using Base.Rulesets.Straight.UI;
using Base.Sheetmusics;
using Base.UI;
using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.Rulesets {
    public class StraightRuleset : Ruleset {

        public StraightRuleset(RulesetInfo rulesetInfo) : base(rulesetInfo) {
        }

        public override RulesetContainer CreateRulesetContainerWith(WorkingSheetmusic sheetMusic, bool isForCurrentRuleset) {

            /*
             * 因為ruleset不是gameobject，但是又要進行new，所以要Delegate給一個gameobject來new，new完再刪掉gameobject
             */
            GameObject newable = new GameObject("NewHandler");
            Newable newHandler = newable.AddComponent<Newable>();
            RulesetContainer rc = newHandler.New<StraightRulesetContainer>(new object[] { sheetMusic, isForCurrentRuleset });
            Object.Destroy(newable);
            return rc;
        }
    }
}