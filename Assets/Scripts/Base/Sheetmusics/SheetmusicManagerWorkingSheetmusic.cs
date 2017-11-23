
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Base.Sheetmusics {
    public class SheetmusicManagerWorkingSheetmusic : WorkingSheetmusic {
        public SheetmusicManagerWorkingSheetmusic(SheetmusicInfo sheetmusicInfo) : base(sheetmusicInfo) {
        }

        protected override Sheetmusic GetSheetmusic() {
            try {
                Sheetmusic sheetmusic;

                SheetmusicDecoder decoder;
                using (var stream = new StreamReader((Application.dataPath +"/"+ SheetmusicInfo.Path))) {
                    decoder = SheetmusicDecoder.GetDecoder(stream);
                    sheetmusic = decoder.Decode(stream);
                }
                return sheetmusic;
            } catch {
                return null;
            }
        }
    }
}