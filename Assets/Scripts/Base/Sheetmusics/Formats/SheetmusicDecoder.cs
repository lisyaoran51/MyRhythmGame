using Base.Rulesets.Straight.Sheetmusics.Formats;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Base.Sheetmusics.Formats {
    public abstract class SheetmusicDecoder {

        private static readonly Dictionary<string, Type> decoders = new Dictionary<string, Type>();

        static SheetmusicDecoder() {
            StraightDecoder.Register();
        }

        public static SheetmusicDecoder GetDecoder(StreamReader stream) {
            string line;
            do {
                line = stream.ReadLine();
                if (line != null)
                    line = line.Trim();
            }
            while (line != null && line.Length == 0);

            if (line == null || !decoders.ContainsKey(line))
                throw new IOException(@"Unknown file format");
            return (SheetmusicDecoder)Activator.CreateInstance(decoders[line], line);
        }

        protected static void AddDecoder<T>(string version) where T : SheetmusicDecoder {
            decoders[version] = typeof(T);
        }

        public Sheetmusic Decode(StreamReader stream) {
            return ParseFile(stream);
        }

        protected virtual Sheetmusic ParseFile(StreamReader stream) {
            var sheetmusic = new Sheetmusic {
                SheetmusicInfo = new SheetmusicInfo {
                    Metadata = new SheetmusicMetadata(),
                    BaseDifficulty = new SheetmusicDifficulty(),
                },

            };

            ParseFile(stream, sheetmusic);
            return sheetmusic;
        }

        protected abstract void ParseFile(StreamReader stream, Sheetmusic sheetmusic);
    }
}