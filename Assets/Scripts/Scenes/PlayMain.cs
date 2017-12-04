using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Play;
using Base.Utils;
using Base.Utils.Types;

namespace Base.Scenes {
    public class PlayMain : AppBase {


        // 在遊戲編輯器中refernece
        public GameObject Loader;

        public Player Player;
        

        void Start() {
            Loader loader = Loader.GetComponent<Loader>();
            WorkingSheetmusic = loader.WorkingSheetmusic;
            Ruleset = loader.Ruleset;

            AddChild(Player = New<Player>(null));
        }

    }
}