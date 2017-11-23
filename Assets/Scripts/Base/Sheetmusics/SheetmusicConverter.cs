using Base.Rulesets.Objects;
using Base.Sheetmusics;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Sheetmusics {
    public abstract class SheetmusicConverter<T>
        where T : HitObject {

        public Sheetmusic<T> Convert(Sheetmusic original) {
            // We always operate on a clone of the original beatmap, to not modify it game-wide
            return ConvertSheetmusic(new Sheetmusic(original));
        }

        protected virtual Sheetmusic<T> ConvertSheetmusic(Sheetmusic original) {
            return new Sheetmusic<T> {
                SheetmusicInfo = original.SheetmusicInfo,
                ControlPointInfo = original.ControlPointInfo,
                HitObjects = original.HitObjects.SelectMany(h => convert(h, original)).ToList()
            };
        }

        private IEnumerable<T> convert(HitObject original, Sheetmusic sheetmusic) {
            // Check if the hitobject is already the converted type
            T tObject = original as T;
            if (tObject != null) {
                yield return tObject;
                yield break;
            }

            // Convert the hit object
            foreach (var obj in ConvertHitObject(original, sheetmusic)) {
                if (obj == null)
                    continue;

                yield return obj;
            }
        }

        protected abstract IEnumerable<T> ConvertHitObject(HitObject original, Sheetmusic sheetmusic);

    }

}