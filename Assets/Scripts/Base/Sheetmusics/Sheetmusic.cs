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



    }

    public class Sheetmusic : Sheetmusic<HitObject> {


        
    }
}