using Base.Rulesets;
using Base.Sheetmusics;
using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Types;

namespace Base.UI{
	

	public class Screen : ChildAddable {

        protected Screen lastScreen;

        private SheetmusicManager sheetmusicManager;

        private WorkingSheetmusic workingSheetmusic;

        public WorkingSheetmusic WorkingSheetmusic {
            protected set{
                workingSheetmusic = value;
            }
            get {
                return workingSheetmusic;
            }
        }

        private RulesetInfo rulesetInfo;

		public RulesetInfo RulesetInfo{
            
			get{
				return rulesetInfo;
			}
		}




        private void load(AppBase appBase) {
            rulesetInfo = appBase.RulesetInfo;
            workingSheetmusic = appBase.WorkingSheetmusic;
        }

    }
}