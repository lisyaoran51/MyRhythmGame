using Base.Rulesets.Straight.Mods;
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

        private void Awake() {
            LazyConstruct();
        }

        // Use this for initialization
        void Start() {
            
        }

        void Update() {
            if (Input.GetKey(KeyCode.Alpha1)) {
                //Ruleset = new StraightRuleset();
                SheetmusicManager s = new SheetmusicManager();
                s.Import(@"D:\Users\TsaiJiaYu\documents\MyRhythmGame\test");
                Cache(s);
                Cache(new IntangibleScreen(new RulesetInfo("Base.Rulesets.Straight.Rulesets.StraightRuleset"), null));

                SongSelect = New<SongSelect>(null);
                AddChild(SongSelect);

                WorkingSheetmusic = SongSelect.Select(s.SheetmusicInfos[0]);
                // 把SheetmusicManager和WorkingSheetmusic load進自己裡面

                /* 把mod改變為flow out*/
                //WorkingSheetmusic.Mods.Add(new StraightModFlowOut());


                Loader loader = New<Loader>(new object[] { SongSelect });

                SceneManager.LoadScene("TestPlay");
				//SceneManager.LoadScene("Play");
            }

        }

    }
}