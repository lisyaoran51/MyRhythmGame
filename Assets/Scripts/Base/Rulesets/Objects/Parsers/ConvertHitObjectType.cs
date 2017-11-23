using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Objects.Parsers {
    [Flags]
    public enum ConvertHitObjectType {
        Note = 1 << 0,      // 0000001
        Slider = 1 << 1,    // 0000010
        NewCombo = 1 << 2,  // 0000100
        //Spinner = 1 << 3,   // 0001000
        ColourHax = 120,    // 1111000
        Hold = 1 << 7       //10000000
    }
}
