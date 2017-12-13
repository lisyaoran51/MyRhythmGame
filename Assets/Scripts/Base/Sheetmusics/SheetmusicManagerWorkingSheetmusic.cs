
using Base.Sheetmusics.Formats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Base.Sheetmusics {
    public class SheetmusicManagerWorkingSheetmusic : WorkingSheetmusic {
        public SheetmusicManagerWorkingSheetmusic(SheetmusicInfo sheetmusicInfo) : base(sheetmusicInfo) {
        }

        /// <summary>
        /// 當workingSheetmusic的sheetmusic是null時，就會呼叫GetSheetmusic來擺進Sheetmusic裡
        /// </summary>
        /// <returns></returns>
        protected override Sheetmusic GetSheetmusic() {
            try {
                Sheetmusic sheetmusic;

                SheetmusicDecoder decoder;
                // TODO: 把音樂擺的位置設成可變動的參數，應該是存在SheetmusicInfo，一開始就知道的path位置
                using (var stream = new StreamReader((Application.dataPath +"/Resources/Sheetmusics/"+ SheetmusicInfo.Path))) {
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