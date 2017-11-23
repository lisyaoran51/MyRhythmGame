using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Play;
using Base.Utils;
using Base.Utils.Types;

namespace Base.Scenes {
    public class PlayMain : ChildAddable {


        // 在遊戲編輯器中refernece
        public GameObject Loader;

        public Player Player;
        

        void Start() {
            Loader loader = Loader.GetComponent<Loader>();

            Cache(loader);
            AddChild(Player = New<Player>(null));
        }

    }
}