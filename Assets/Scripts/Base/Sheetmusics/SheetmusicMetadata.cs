using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Sheetmusics {
    public class SheetmusicMetadata {
        public string Artist { get; internal set; }
        public string ArtistUnicode { get; internal set; }
        public string AudioFile { get; internal set; }
        public string AuthorString { get; internal set; }
        public int OnlineSheetmusicSetID { get; internal set; }
        public int PreviewTime { get; internal set; }
        public string Source { get; internal set; }
        public string Tags { get; internal set; }
        public string Title { get; internal set; }
        public string TitleUnicode { get; internal set; }

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}