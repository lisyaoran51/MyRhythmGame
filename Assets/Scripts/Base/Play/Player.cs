using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Screen = Base.UI.Screen;
using Base.Sheetmusics;
using Base.UI;
using Base.Scenes;
using System.Linq;

namespace Base.Play{

	public class Player : Screen {

		private RulesetInfo rulesetInfo;

		private RulesetContainer rulesetContainer;

		public RulesetContainer RulesetContainer;

		private ScoreProcessor scoreProcessor;

		private PlayField playField;

		public PlayField PlayField;

        protected new void construct() {
            base.construct();
        } //實驗

        private void load() {

            Sheetmusic sheetmusic = WorkingSheetmusic.Sheetmusic;

            rulesetInfo = RulesetInfo ?? sheetmusic.SheetmusicInfo.RulesetInfo;
            var rulesetInstance = rulesetInfo.CreateInstance();

            RulesetContainer = rulesetInstance.CreateRulesetContainerWith(
                WorkingSheetmusic,
                rulesetInfo.ID == sheetmusic.SheetmusicInfo.RulesetID);

            //scoreProcessor = RulesetContainer.CreateScoreProcessor();

            AddChild(RulesetContainer);
            
        }
	}

}
