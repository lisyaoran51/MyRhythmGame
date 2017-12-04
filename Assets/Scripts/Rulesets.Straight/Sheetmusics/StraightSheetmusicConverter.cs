using Base.Rulesets.Straight.Rulesets.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Rulesets.Objects;
using Base.Sheetmusics;
using System;
using Base.Rulesets.Straight.Sheetmusics.Patterns.Straight;
using Base.Rulesets.Straight.Sheetmusics.Patterns;
using Base.Rulesets.Straight.Rulesets.Objects.Types;

public class StraightSheetmusicConverter : SheetmusicConverter<StraightHitObject> {

    private Pattern lastPattern = new Pattern();
    private Sheetmusic sheetmusic;

    private readonly int availableColumns;
    private readonly bool isForCurrentRuleset;

    public StraightSheetmusicConverter(bool isForCurrentRuleset, int availableColumns) {

        this.isForCurrentRuleset = isForCurrentRuleset;
        this.availableColumns = availableColumns;
    }




    protected override IEnumerable<StraightHitObject> ConvertHitObject(HitObject original, Sheetmusic sheetmusic) {
        var straightOriginal = original as StraightHitObject;
        if (straightOriginal != null) {
            yield return straightOriginal;
            yield break;
        }

        var objects = isForCurrentRuleset ? generateSpecific(original) : generateConverted(original);

        if (objects == null)
            yield break;

        foreach (StraightHitObject obj in objects)
            yield return obj;
    }

    /// <summary>
    /// Method that generates hit objects for osu!mania specific beatmaps.
    /// </summary>
    /// <param name="original">The original hit object.</param>
    /// <returns>The hit objects generated.</returns>
    private IEnumerable<StraightHitObject> generateSpecific(HitObject original) {
        var generator = new SpecificSheetmusicPatternGenerator(original, sheetmusic, availableColumns, lastPattern);

        Pattern newPattern = generator.Generate();
        lastPattern = newPattern;

        return newPattern.HitObjects;
    }

    /// <summary>
    /// Method that generates hit objects for non-osu!mania beatmaps.
    /// </summary>
    /// <param name="original">The original hit object.</param>
    /// <returns>The hit objects generated.</returns>
    private IEnumerable<StraightHitObject> generateConverted(HitObject original) {
        //var endTimeData = original as IHasEndTime;
        //var distanceData = original as IHasDistance;
        //var positionData = original as IHasColumn;
        //
        //// Following lines currently commented out to appease resharper
        //
        //Patterns.PatternGenerator conversion = null;
        //
        //if (distanceData != null)
        //    conversion = new DistanceObjectPatternGenerator(random, original, beatmap, availableColumns, lastPattern);
        //else if (endTimeData != null)
        //    conversion = new EndTimeObjectPatternGenerator(random, original, beatmap, availableColumns);
        //else if (positionData != null) {
        //    computeDensity(original.StartTime);
        //
        //    conversion = new HitObjectPatternGenerator(random, original, beatmap, availableColumns, lastPattern, lastTime, lastPosition, density, lastStair);
        //
        //    recordNote(original.StartTime, positionData.Position);
        //}
        //
        //if (conversion == null)
        //    return null;
        //
        //Pattern newPattern = conversion.Generate();
        //lastPattern = newPattern;
        //
        //var stairPatternGenerator = (HitObjectPatternGenerator)conversion;
        //lastStair = stairPatternGenerator.StairType;
        //
        //return newPattern.HitObjects;
        return null;
    }
}
