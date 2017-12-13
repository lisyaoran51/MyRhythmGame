using Base.Rulesets;
using Base.Rulesets.Straight.Rulesets;
using Base.Sheetmusics.Formats;
using Base.Sheetmusics.IO;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;


namespace Base.Sheetmusics {
    public class SheetmusicManager {

        public List<SheetmusicInfo> SheetmusicInfos;

        /// <summary>
        /// 之後可以寫成跟資料庫連結，直接讀檔案來輸入ruleset
        /// </summary>
        private List<Ruleset> rulesets = new List<Ruleset>();

        public SheetmusicManager() {
            rulesets.Add(new StraightRuleset(
                new RulesetInfo {
                    ID = 0,
                    Name = "Straight",
                    InstantiationInfo = "StraightRuleset"
                }
            ));
        }

        public void Import(params string[] paths) {
            foreach (string path in paths) {
                using (FileReader reader = new FileReader(path))  // LegacyFilesystemReader
                    SheetmusicInfos = import(reader); // TODO: 把讀取的普應該存在資料庫，而不是記憶體
            }
        }

        private List<SheetmusicInfo> import(FileReader fileReader) {
            return importToStorage(fileReader);
        }

        private List<SheetmusicInfo> importToStorage(FileReader reader) {

            List<SheetmusicInfo> sheetmusicInfos = new List<SheetmusicInfo>();
            var sheetNames = reader.Filenames.Where(f => f.EndsWith(".sm"));

            foreach (string name in sheetNames) {
                using (var raw = reader.GetStream(name))
                using (var ms = new MemoryStream()) //we need a memory stream so we can seek and shit
                using (var sr = new StreamReader(ms)) {
                    copyTo(raw, ms);
                    ms.Position = 0;

                    var decoder = SheetmusicDecoder.GetDecoder(sr);
                    Sheetmusic sheetmusic = decoder.Decode(sr);
                    sheetmusic.SheetmusicInfo.Path = name;
                    RulesetInfo rulesetInfo = rulesets.Where(r => r.RulesetInfo.ID == sheetmusic.SheetmusicInfo.RulesetID)
                                                      .FirstOrDefault().RulesetInfo;
                    sheetmusic.SheetmusicInfo.RulesetInfo = rulesetInfo;

                    sheetmusicInfos.Add(sheetmusic.SheetmusicInfo);
                }
            }
            return sheetmusicInfos;
        }

        /// <summary>
        /// Stream.CopyTo
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        private static void copyTo(Stream input, Stream output) {
            byte[] buffer = new byte[16 * 1024]; // Fairly arbitrary size
            int bytesRead;

            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0) {
                output.Write(buffer, 0, bytesRead);
            }
        }

        /// <summary>
        /// Retrieve a <see cref="WorkingSheetmusic"/> instance for the provided <see cref="SheetmusicInfo"/>
        /// </summary>
        /// <param name="SheetmusicInfo">The beatmap to lookup.</param>
        /// <param name="previous">The currently loaded <see cref="WorkingSheetmusic"/>. Allows for optimisation where elements are shared with the new beatmap.</param>
        /// <returns>A <see cref="WorkingSheetmusic"/> instance correlating to the provided <see cref="SheetmusicInfo"/>.</returns>
        public WorkingSheetmusic GetWorkingSheetmusic(SheetmusicInfo sheetmusicInfo) {
            if (sheetmusicInfo == null)
                return null;

            WorkingSheetmusic working = new SheetmusicManagerWorkingSheetmusic(sheetmusicInfo);
            
            return working;
        }
    }
}