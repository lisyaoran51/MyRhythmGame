using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Play;
using Base.Utils;
using Base.Utils.Types;
using Base.Rulesets;
using Base.Rulesets.Straight.Configurations;
using Base.Rulesets.Straight;
using Base.Configurations;

namespace Base.Scenes {
    public class PlayMain : AppBase {

        public GameObject text;

        public GameObject Loader;

        public Player Player;


        private void Awake() {
            LazyConstruct();
        }

        void Start() {
            Debugger.button = button;

            InitializeView();
            //
            Cache(new StraightConfigManager()
                .Set(StraightSetting.StartPitch, Pitch.c1)
                .Set(StraightSetting.availableColumns, 36)
                .Set(StraightSetting.TargetLineHeight, 0f)
                .Set(StraightSetting.WhiteKeyLength, 130f)
                .Set(StraightSetting.WhiteKeyTarget, 10f / 11f)
                .Set(StraightSetting.BlackKeyLength, 9f)
                .Set(StraightSetting.BlackKeyTarget, 6f / 7f)
                .Set(StraightSetting.PixelToMillimeter, 0.195f));
            //
            Cache(new FrameworkConfigManager()
                .Set(FrameworkSetting.Width,  1920)
                .Set(FrameworkSetting.Height, 1080));
            //
            var loadMethods = new List<int>();

            Loader = GameObject.Find("Loader");
            Loader loader = Loader.GetComponent<Loader>();
            Cache(loader.LastScreen);
            //
            AddChild(Player = New<Player>(null));

            

        }

        private void InitializeView() {
            ViewConfig viewConfig = new ViewConfig();

            viewConfig.Set("StraightPlayField", new View() { })
                      .Set("Column", new View() {
                          //Scale = new Vector2(0.8f, 2f),
                          SpritePaths = new List<string>() { "Column4" },
                          SortingLayerName = "Column"
                      })
                      .Set("DrawableNote", new View() {
                          SpritePaths = new List<string>() { "Note5", "BlackNote2" },
                          SortingLayerName = "HitObject"
                      });

            Cache(viewConfig);
        }
    }
}