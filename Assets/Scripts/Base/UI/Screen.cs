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
            get {
                return workingSheetmusic;
            }
        }

        private Ruleset ruleset;

		public Ruleset Ruleset{
			get{
				return ruleset;
			}
		}

        
        

        internal void load(Transform transform, UI.Screen lastScreen) {
            workingSheetmusic = lastScreen.workingSheetmusic;
            ruleset = lastScreen.Ruleset;
        }
        
    }
}