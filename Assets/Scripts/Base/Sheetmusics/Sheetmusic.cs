using Base.IO.Serialization;
using Base.Rulesets.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Sheetmusics {

    public class Sheetmusic<T>
            where T : HitObject 
    {
        public SheetmusicInfo SheetmusicInfo = new SheetmusicInfo();
        public ControlPointInfo ControlPointInfo = new ControlPointInfo();
        public SheetmusicMetadata SheetmusicMetadata = new SheetmusicMetadata();

        // The HitObjects this SheetMusic contains.
        public List<T> HitObjects = new List<T>();

        public Sheetmusic(Sheetmusic<T> original = null) {
            if(original != null) {
                SheetmusicInfo = original.SheetmusicInfo.DeepClone();
                ControlPointInfo = original.ControlPointInfo;
                SheetmusicMetadata = original.SheetmusicMetadata;
                HitObjects = original.HitObjects;
            }
        }
    }

    public class Sheetmusic : Sheetmusic<HitObject> {
       

        public Sheetmusic(Sheetmusic original = null)
            : base(original) {
        }
    }
}