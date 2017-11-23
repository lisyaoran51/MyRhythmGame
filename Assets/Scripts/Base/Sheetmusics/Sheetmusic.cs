using Base.Rulesets.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Sheetmusics {

    public class Sheetmusic<T>
            where T : HitObject 
    {
        public SheetmusicInfo SheetmusicInfo;
        public ControlPointInfo ControlPointInfo;
        public SheetmusicMetadata SheetmusicMetadata;

        // The HitObjects this SheetMusic contains.
        public List<T> HitObjects = new List<T>();

        public Sheetmusic(Sheetmusic original) {
            SheetmusicInfo = original.SheetmusicInfo;
            ControlPointInfo = original.ControlPointInfo;
            SheetmusicMetadata = original.SheetmusicMetadata;
            HitObjects = original.HitObjects;
        }
    }

    public class Sheetmusic : Sheetmusic<HitObject> {
       

        public Sheetmusic(Sheetmusic original)
            : base(original) {
        }
    }
}