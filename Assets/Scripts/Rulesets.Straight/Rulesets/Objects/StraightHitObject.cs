using Base.Audio;
using Base.Rulesets.Straight.Rulesets.Objects.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Straight.Rulesets.Objects {
    public class StraightHitObject : ScrollingHitObject, IHasColumn {
        
        public double StartBar {
            private set; get;// TODO:StartBar.get
        }

        public virtual int Column { get; set; }

        public Pitch Pitch;

        public bool IsBlackKey() {
            if( ((int)Pitch) % 12 == 1 ||
                ((int)Pitch) % 12 == 3 ||
                ((int)Pitch) % 12 == 6 ||
                ((int)Pitch) % 12 == 8 ||
                ((int)Pitch) % 12 == 10 )
                return true;
            return false;
        }
        
    }
}