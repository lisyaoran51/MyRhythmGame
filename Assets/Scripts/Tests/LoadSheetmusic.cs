using Base.Rulesets.Straight.Rulesets;
using Base.Scenes;
using Base.Sheetmusics;
using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Tests {
    public class LoadSheetmusic : AppBase {
        public SongSelect SongSelect;
        public GameObject Loader;
        // Use this for initialization
        void Start() {
            
            //Ruleset = new StraightRuleset();
            SheetmusicManager s = new SheetmusicManager();
            s.Import(@"D:\Users\TsaiJiaYu\documents\MyRhythmGame\test");
            Cache(s);
            
            SongSelect = New<SongSelect>(null);
            AddChild(SongSelect);
            
            WorkingSheetmusic = SongSelect.Select(s.SheetmusicInfos[0]);
            // 把SheetmusicManager和WorkingSheetmusic load進自己裡面
            LoadAsync();

            Loader loader = New<Loader>(new object[] { SongSelect });

            SceneManager.LoadScene("Play");
        }

    }
}