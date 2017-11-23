using Base.Rulesets.Objects;
using Base.Rulesets.Objects.Parsers;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Base.Rulesets.Straight.Rulesets.Objects.Parsers {
    public class ConvertHitObjectParser : HitObjectParser {
        public override HitObject Parse(string text) {
            try     // x y  ConvertHitObjectType LegacySoundType curve|x:y|x:y repeatCount Length bank|add|vol 額外節點的bank 額外節點的SoundTypes
            {       //                                                        開始 中點 結束
                string[] split = text.Split(',');

                ConvertHitObjectType type = (ConvertHitObjectType)int.Parse(split[3]) & ~ConvertHitObjectType.ColourHax;
                bool combo = (type & ConvertHitObjectType.NewCombo) == ConvertHitObjectType.NewCombo;
                type &= ~ConvertHitObjectType.NewCombo;

                //var soundType = (LegacySoundType)int.Parse(split[4]);
                //var bankInfo = new SampleBankInfo();

                HitObject result = null;

                if ((type & ConvertHitObjectType.Note) > 0) {
                    result = CreateHit(int.Parse(split[0]), int.Parse(split[1]), combo);

                    //if (split.Length > 5)
                    //    readCustomSampleBanks(split[5], bankInfo);
                } else if ((type & ConvertHitObjectType.Slider) > 0) {
                    //CurveType curveType = CurveType.Catmull;
                    //double length = 0;
                    //var points = new List<Vector2> { new Vector2(int.Parse(split[0]), int.Parse(split[1])) };
                    //
                    //string[] pointsplit = split[5].Split('|');
                    //foreach (string t in pointsplit) {
                    //    if (t.Length == 1) {
                    //        switch (t) {
                    //            case @"C":
                    //                curveType = CurveType.Catmull;
                    //                break;
                    //            case @"B":
                    //                curveType = CurveType.Bezier;
                    //                break;
                    //            case @"L":
                    //                curveType = CurveType.Linear;
                    //                break;
                    //            case @"P":
                    //                curveType = CurveType.PerfectCurve;
                    //                break;
                    //        }
                    //        continue;
                    //    }
                    //
                    //    string[] temp = t.Split(':');
                    //    points.Add(new Vector2((int)Convert.ToDouble(temp[0], CultureInfo.InvariantCulture), (int)Convert.ToDouble(temp[1], CultureInfo.InvariantCulture)));
                    //}
                    //
                    //int repeatCount = Convert.ToInt32(split[6], CultureInfo.InvariantCulture);
                    //
                    //if (repeatCount > 9000)
                    //    throw new ArgumentOutOfRangeException(nameof(repeatCount), @"Repeat count is way too high");
                    //
                    //if (split.Length > 7)
                    //    length = Convert.ToDouble(split[7], CultureInfo.InvariantCulture);
                    //
                    //if (split.Length > 10)
                    //    readCustomSampleBanks(split[10], bankInfo);
                    //
                    //// One node for each repeat + the start and end nodes
                    //// Note that the first length of the slider is considered a repeat, but there are no actual repeats happening
                    //int nodes = Math.Max(0, repeatCount - 1) + 2;
                    //
                    //// Populate node sample bank infos with the default hit object sample bank
                    //var nodeBankInfos = new List<SampleBankInfo>();
                    //for (int i = 0; i < nodes; i++)
                    //    nodeBankInfos.Add(bankInfo.Clone());
                    //
                    //// Read any per-node sample banks
                    //if (split.Length > 9 && split[9].Length > 0) {
                    //    string[] sets = split[9].Split('|');
                    //    for (int i = 0; i < nodes; i++) {
                    //        if (i >= sets.Length)
                    //            break;
                    //
                    //        SampleBankInfo info = nodeBankInfos[i];
                    //        readCustomSampleBanks(sets[i], info);
                    //    }
                    //}
                    //
                    //// Populate node sound types with the default hit object sound type
                    //var nodeSoundTypes = new List<LegacySoundType>();
                    //for (int i = 0; i < nodes; i++)
                    //    nodeSoundTypes.Add(soundType);
                    //
                    //// Read any per-node sound types
                    //if (split.Length > 8 && split[8].Length > 0) {
                    //    string[] adds = split[8].Split('|');
                    //    for (int i = 0; i < nodes; i++) {
                    //        if (i >= adds.Length)
                    //            break;
                    //
                    //        int sound;
                    //        int.TryParse(adds[i], out sound);
                    //        nodeSoundTypes[i] = (LegacySoundType)sound;
                    //    }
                    //}
                    //
                    //// Generate the final per-node samples
                    //var nodeSamples = new List<SampleInfoList>(nodes);
                    //for (int i = 0; i <= repeatCount; i++)
                    //    nodeSamples.Add(convertSoundType(nodeSoundTypes[i], nodeBankInfos[i]));
                    //
                    //result = CreateSlider(new Vector2(int.Parse(split[0]), int.Parse(split[1])), combo, points, length, curveType, repeatCount, nodeSamples); // 把資料擺進去
                } else if ((type & ConvertHitObjectType.Hold) > 0) {
                    // Note: Hold is generated by BMS converts

                    double endTime = Convert.ToDouble(split[2]);

                    if (split.Length > 5 && !string.IsNullOrEmpty(split[5])) {
                        string[] ss = split[5].Split(':');
                        endTime = Convert.ToDouble(ss[0]);
                        //readCustomSampleBanks(string.Join(":", ss.Skip(1)), bankInfo);
                    }

                    result = CreateHold(int.Parse(split[0]), int.Parse(split[1]), combo, endTime);
                }

                if (result == null)
                    throw new InvalidOperationException(@"Unknown hit object type " + type);

                result.StartTime = Convert.ToDouble(split[2]);
                //result.Samples = convertSoundType(soundType, bankInfo);

                return result;
            } catch (FormatException) {
                throw new FormatException("One or more hit objects were malformed.");
            }
        }


    

        public HitObject CreateHit(int x, int y, bool combo) {
            return new ConvertHit {
                Column = x,
                NewCombo = combo,
            };
        }

        protected HitObject CreateHold(int x, int y, bool newCombo, double endTime) {
            return new ConvertHold {
                Column = x,
                EndTime = endTime
            };
        }
    }
}