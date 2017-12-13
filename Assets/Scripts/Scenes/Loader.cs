using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Scenes{

	public class Loader : Newable {
        public IntangibleScreen LastScreen;
        private void construct(UI.Screen screen) {
            LastScreen = screen.AsIntangible();
            DontDestroyOnLoad(this.gameObject);
        }
    }
}