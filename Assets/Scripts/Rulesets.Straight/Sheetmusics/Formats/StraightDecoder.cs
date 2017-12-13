using Base.Rulesets.Objects.Parsers;
using Base.Sheetmusics;
using Base.Sheetmusics.ControlPoints;
using Base.Sheetmusics.Formats;
using Base.Sheetmusics.Timing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Base.Rulesets.Straight.Sheetmusics.Formats {
    public class StraightDecoder : SheetmusicDecoder {

        private int sheetmusicVersion;

        private int defaultSampleVolume = 100;

        private HitObjectParser parser;

        internal static void Register() {
            AddDecoder<StraightDecoder>(@"straight file format v0");
        }

        public StraightDecoder(string header) {
            sheetmusicVersion = int.Parse(header.Substring(22));
        }

        protected override void ParseFile(StreamReader stream, Sheetmusic sheetmusic) // 解碼
        {
            sheetmusic.SheetmusicInfo.SheetmusicVersion = sheetmusicVersion;

            Section section = Section.None;
            bool hasCustomColours = false;

            string line;
            while ((line = stream.ReadLine()) != null) {
                if (line.Equals(" ") || line.Equals(""))
                    continue;

                if (line.StartsWith("//"))
                    continue;

                if (line.StartsWith(@"straight file format v")) {
                    sheetmusic.SheetmusicInfo.SheetmusicVersion = int.Parse(line.Substring(13));
                    continue;
                }

                if (line.StartsWith(@"[") && line.EndsWith(@"]")) {
                    section = (Section)Enum.Parse(typeof(Section), line.Substring(1, line.Length - 2));
                    continue;
                }

                switch (section) {
                    case Section.General:
                        handleGeneral(sheetmusic, line);
                        break;
                    case Section.Metadata:
                        handleMetadata(sheetmusic, line);
                        break;
                    case Section.Difficulty:
                        handleDifficulty(sheetmusic, line);
                        break;
                    //case Section.Events:
                    //    handleEvents(sheetmusic, line, ref storyboardSprite, ref timelineGroup);
                    //    break;
                    case Section.TimingPoints:
                        handleTimingPoints(sheetmusic, line);
                        break;
                    //case Section.Colours:
                    //    handleColours(sheetmusic, line, ref hasCustomColours);
                    //    break;
                    case Section.HitObjects:

                        // If the ruleset wasn't specified, assume the osu!standard ruleset.
                        if (parser == null)
                            parser = new Rulesets.Objects.Parsers.ConvertHitObjectParser();

                        var obj = parser.Parse(line);

                        if (obj != null)
                            sheetmusic.HitObjects.Add(obj);

                        break;
                    //case Section.Variables:
                    //    handleVariables(line);
                    //    break;
                }
            }

            foreach (var hitObject in sheetmusic.HitObjects)
                hitObject.ApplyDefaults(sheetmusic.ControlPointInfo, sheetmusic.SheetmusicInfo.BaseDifficulty);
        }
        

        private void handleGeneral(Sheetmusic sheetmusic, string line)        // general區塊
        {
            var pair = splitKeyVal(line, ':');

            var metadata = sheetmusic.SheetmusicInfo.Metadata;
            switch (pair.Key) {
                case @"AudioFilename":
                    metadata.AudioFile = pair.Value;
                    break;
                case @"AudioLeadIn":
                    sheetmusic.SheetmusicInfo.AudioLeadIn = int.Parse(pair.Value);
                    break;
                case @"PreviewTime":
                    metadata.PreviewTime = int.Parse(pair.Value);
                    break;
                //case @"Countdown":
                //    sheetmusic.SheetmusicInfo.Countdown = int.Parse(pair.Value) == 1;
                //    break;
                //case @"SampleSet":
                //    defaultSampleBank = (LegacySampleBank)Enum.Parse(typeof(LegacySampleBank), pair.Value);
                //    break;
                //case @"SampleVolume":
                //    defaultSampleVolume = int.Parse(pair.Value);
                //    break;
                //case @"StackLeniency":
                //    beatmap.BeatmapInfo.StackLeniency = float.Parse(pair.Value, NumberFormatInfo.InvariantInfo);
                //    break;
                case @"Mode":
                    sheetmusic.SheetmusicInfo.RulesetID = int.Parse(pair.Value);
                
                    switch (sheetmusic.SheetmusicInfo.RulesetID) {
                        case 0:
                            parser = new Base.Rulesets.Straight.Rulesets.Objects.Parsers.ConvertHitObjectParser();
                            break;
                    }
                    break;
                //case @"LetterboxInBreaks":
                //    beatmap.BeatmapInfo.LetterboxInBreaks = int.Parse(pair.Value) == 1;
                //    break;
                //case @"SpecialStyle":
                //    beatmap.BeatmapInfo.SpecialStyle = int.Parse(pair.Value) == 1;
                //    break;
                //case @"WidescreenStoryboard":
                //    beatmap.BeatmapInfo.WidescreenStoryboard = int.Parse(pair.Value) == 1;
                //    break;
            }
        }

        private void handleMetadata(Sheetmusic sheetmusic, string line) {
            var pair = splitKeyVal(line, ':');

            var metadata = sheetmusic.SheetmusicInfo.Metadata;
            switch (pair.Key) {
                case @"Title":
                    metadata.Title = pair.Value;
                    break;
                case @"TitleUnicode":
                    metadata.TitleUnicode = pair.Value;
                    break;
                case @"Artist":
                    metadata.Artist = pair.Value;
                    break;
                case @"ArtistUnicode":
                    metadata.ArtistUnicode = pair.Value;
                    break;
                case @"Creator":
                    metadata.AuthorString = pair.Value;
                    break;
                case @"Version":
                    sheetmusic.SheetmusicInfo.Version = pair.Value;
                    break;
                case @"Source":
                    sheetmusic.SheetmusicInfo.Metadata.Source = pair.Value;
                    break;
                case @"Tags":
                    sheetmusic.SheetmusicInfo.Metadata.Tags = pair.Value;
                    break;
                case @"SheetmusicID":
                    sheetmusic.SheetmusicInfo.OnlineSheetmusicID = int.Parse(pair.Value);
                    break;
                case @"SheetmusicSetID":
                    sheetmusic.SheetmusicInfo.OnlineSheetmusicSetID = int.Parse(pair.Value);
                    metadata.OnlineSheetmusicSetID = int.Parse(pair.Value);
                    break;
            }
        }

        private void handleDifficulty(Sheetmusic sheetmusic, string line) {
            var pair = splitKeyVal(line, ':');

            var difficulty = sheetmusic.SheetmusicInfo.BaseDifficulty;
            switch (pair.Key) {
                case @"HPDrainRate":
                    difficulty.DrainRate = float.Parse(pair.Value);
                    break;
                case @"OverallDifficulty":
                    difficulty.OverallDifficulty = float.Parse(pair.Value);
                    break;
                case @"ApproachRate":
                    difficulty.ApproachRate = float.Parse(pair.Value);
                    break;
                case @"SliderMultiplier":
                    difficulty.SliderMultiplier = float.Parse(pair.Value);
                    break;
                case @"SliderTickRate":
                    difficulty.SliderTickRate = float.Parse(pair.Value);
                    break;
            }
        }

        private void handleTimingPoints(Sheetmusic sheetmusic, string line) {
            // 音 時間 長度 加速 三分/四分 音量 時間改變
            string[] split = line.Split(',');

            int column = int.Parse(split[0].Trim());
            float time = float.Parse(split[1].Trim());
            float noteLength = float.Parse(split[2].Trim());
            float speedMultiplier = noteLength < 0 ? 100f / -noteLength : 1;

            TimeSignatures timeSignature = TimeSignatures.SimpleQuadruple;
            if (split.Length >= 4)
                timeSignature = split[3][0] == '0' ? TimeSignatures.SimpleQuadruple : (TimeSignatures)int.Parse(split[2]);

            //LegacySampleBank sampleSet = defaultSampleBank;
            //if (split.Length >= 4)
            //    sampleSet = (LegacySampleBank)int.Parse(split[3]);

            //SampleBank sampleBank = SampleBank.Default;
            //if (split.Length >= 5)
            //    sampleBank = (SampleBank)int.Parse(split[4]);

            int sampleVolume = defaultSampleVolume;
            if (split.Length >= 5)
                sampleVolume = int.Parse(split[4]);

            bool timingChange = true;
            if (split.Length >= 6)
                timingChange = split[5][0] == '1';

            //bool kiaiMode = false;
            //bool omitFirstBarSignature = false;
            //if (split.Length >= 8) {
            //    int effectFlags = int.Parse(split[7]);
            //    kiaiMode = (effectFlags & 1) > 0;
            //    omitFirstBarSignature = (effectFlags & 8) > 0;
            //}

            //string stringSampleSet = sampleSet.ToString().ToLower();
            //if (stringSampleSet == @"none")
            //    stringSampleSet = @"normal";

            //DifficultyControlPoint difficultyPoint = beatmap.ControlPointInfo.DifficultyPointAt(time);
            //SoundControlPoint soundPoint = beatmap.ControlPointInfo.SoundPointAt(time);
            //EffectControlPoint effectPoint = beatmap.ControlPointInfo.EffectPointAt(time);

            if (timingChange) {
                sheetmusic.ControlPointInfo.TimingControlPoints.Add(new TimingControlPoint {
                    Column = column,
                    Time = time,
                    NoteLength = noteLength,
                    TimeSignature = timeSignature
                });
            }

            //if (speedMultiplier != difficultyPoint.SpeedMultiplier) {
            //    beatmap.ControlPointInfo.DifficultyPoints.RemoveAll(x => x.Time == time);
            //    beatmap.ControlPointInfo.DifficultyPoints.Add(new DifficultyControlPoint {
            //        Time = time,
            //        SpeedMultiplier = speedMultiplier
            //    });
            //}

            //if (stringSampleSet != soundPoint.SampleBank || sampleVolume != soundPoint.SampleVolume) {
            //    beatmap.ControlPointInfo.SoundPoints.Add(new SoundControlPoint {
            //        Time = time,
            //        SampleBank = stringSampleSet,
            //        SampleVolume = sampleVolume
            //    });
            //}

            //if (kiaiMode != effectPoint.KiaiMode || omitFirstBarSignature != effectPoint.OmitFirstBarLine) {
            //    beatmap.ControlPointInfo.EffectPoints.Add(new EffectControlPoint {
            //        Time = time,
            //        KiaiMode = kiaiMode,
            //        OmitFirstBarLine = omitFirstBarSignature
            //    });
            //}
        }

        private KeyValuePair<string, string> splitKeyVal(string line, char separator) {
            var split = line.Trim().Split(new[] { separator }, 2);

            return new KeyValuePair<string, string>
            (
                split[0].Trim(),
                split.Length > 1 ? split[1].Trim() : string.Empty
            );
        }

        private enum Section {
            None,
            General,
            Editor,
            Metadata,
            Difficulty,
            Events,
            TimingPoints,
            Colours,
            HitObjects,
            Variables,
        }
    }
}