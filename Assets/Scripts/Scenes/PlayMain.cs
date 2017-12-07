using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Play;
using Base.Utils;
using Base.Utils.Types;
using Base.Rulesets;

namespace Base.Scenes {
    public class PlayMain : AppBase {


        // 在遊戲編輯器中refernece
        public GameObject Loader;

        public Player Player;
        

        void Start() {

            var loadMethods = new List<int>();
            

            Loader loader = Loader.GetComponent<Loader>();
            Cache(loader.LastScreen);

            AddChild(Player = New<Player>(null));
        }

    }
}