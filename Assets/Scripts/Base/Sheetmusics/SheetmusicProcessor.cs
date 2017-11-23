using Base.Rulesets.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Base.Sheetmusics {
    public class SheetmusicProcessor<T>
    where T : HitObject {
        public virtual void PostProcess(Sheetmusic<T> sheetmusic) {
        }
    }
}
