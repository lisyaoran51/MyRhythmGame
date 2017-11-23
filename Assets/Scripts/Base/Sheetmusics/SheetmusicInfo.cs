using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Sheetmusics {
    public class SheetmusicInfo {

        public int ID { get; set; }

        //TODO: should be in database
        public int SheetmusicVersion;

        public SheetmusicMetadata Metadata { get; set; }

        public int BaseDifficultyID { get; set; }

        public SheetmusicDifficulty BaseDifficulty { get; set; }

        public string Path { get; set; }

        public int RulesetID { get; set; }
        public int AudioLeadIn { get; internal set; }
        public string Version { get; internal set; }
        public int OnlineSheetmusicID { get; internal set; }
        public int OnlineSheetmusicSetID { get; internal set; }

        public Ruleset Ruleset;

        public RulesetInfo RulesetInfo;
        

    }
}