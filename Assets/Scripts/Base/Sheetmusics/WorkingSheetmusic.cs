using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Sheetmusics {
    public abstract class WorkingSheetmusic {
        
        public readonly SheetmusicInfo SheetmusicInfo;

        public readonly SheetmusicMetadata Metadata;

        protected WorkingSheetmusic(SheetmusicInfo sheetmusicInfo) {
            SheetmusicInfo = sheetmusicInfo;
            Metadata = sheetmusicInfo.Metadata ?? new SheetmusicMetadata();
            
        }

        protected abstract Sheetmusic GetSheetmusic();

        private Sheetmusic sheetmusic;

        public Sheetmusic Sheetmusic {
            get {
                if (sheetmusic != null) return sheetmusic;

                sheetmusic = GetSheetmusic();

                // use the database-backed info.
                sheetmusic.SheetmusicInfo = SheetmusicInfo;

                return sheetmusic;
            }
        }

        private Texture background;
        public Texture Background {
            get {
                return null;//background ?? (background = GetBackground());
                
            }
        }

        private void applyRateAdjustments() {
            var t = track;
            if (t == null) return;

            //t.ResetSpeedAdjustments();
        }

        private Track track;
        private readonly object trackLock = new object();
        public Track Track {
            get {
                lock (trackLock) {
                    if (track != null) return track;

                    // we want to ensure that we always have a track, even if it's a fake one.
                    //track = GetTrack() ?? new TrackVirtual();

                    applyRateAdjustments();
                    return track;
                }
            }
        }
    }
}