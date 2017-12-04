using Base.Sheetmusics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Screen = Base.UI.Screen;

namespace Base.Tests {
    public class SongSelect : Screen {

        private SheetmusicManager sheetmusicManager;
        private void load(SheetmusicManager sheetmusicManager) {
            this.sheetmusicManager = sheetmusicManager;
        }

        public WorkingSheetmusic Select(SheetmusicInfo sheetmusicInfo) {
            return WorkingSheetmusic = sheetmusicManager.GetWorkingSheetmusic(sheetmusicInfo);
        }
    }
}