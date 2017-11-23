using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Screen = Base.UI.Screen;
using Base.Sheetmusics;
using Base.UI;

namespace Base.Play{

	public class Player : Screen {

		private RulesetInfo rulesetInfo;

		private RulesetContainer rulesetContainer;

		public RulesetContainer RulesetContainer;

		private ScoreProcessor scoreProcessor;

		private PlayField playField;

		public PlayField PlayField;

        
        private void load(Screen lastScreen) {
            
            Sheetmusic sheetmusic = WorkingSheetmusic.Sheetmusic;

            rulesetInfo = sheetmusic.SheetmusicInfo.RulesetInfo;
            var rulesetInstance = rulesetInfo.CreateInstance(this);

            RulesetContainer = rulesetInstance.CreateRulesetContainerWith(
                WorkingSheetmusic, 
                Ruleset.ID == sheetmusic.SheetmusicInfo.Ruleset.ID);


            scoreProcessor = RulesetContainer.CreateScoreProcessor();


            AddChild(RulesetContainer);
        }
	}

}
