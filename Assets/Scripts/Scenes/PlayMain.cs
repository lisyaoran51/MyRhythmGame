using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Play;
using Base.Utils;
using Base.Utils.Types;
using Base.Rulesets;
using Base.Rulesets.Straight.Configurations;
using Base.Rulesets.Straight;

namespace Base.Scenes {
    public class PlayMain : AppBase {


        // 在遊戲編輯器中refernece
        public GameObject Loader;

        public Player Player;
        

        void Start() {
            InitializeView();
            var loadMethods = new List<int>();

            Loader = GameObject.Find("Loader");
            Loader loader = Loader.GetComponent<Loader>();
            Cache(loader.LastScreen);

            Cache(new StraightConfigManager()
                .Set(StraightSetting.StartPitch, Pitch.c2)
                .Set(StraightSetting.availableColumns, 24));

            AddChild(Player = New<Player>(null));
        }

        private void InitializeView() {
            ViewConfig viewConfig = new ViewConfig();

            viewConfig.Set("StraightPlayField", new View() { })
                      .Set("Column", new View() { })
                      .Set("DrawableNote", new View() { });

            Cache(viewConfig);
        }
    }
}