using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Scenes{

	public class Loader : Updatable {
        public IntangibleScreen LastScreen;
        protected void construct(UI.Screen screen) {
            construct();

            LastScreen = screen.AsIntangible();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}