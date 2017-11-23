using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Rulesets.Objects.Parsers {
    public abstract class HitObjectParser {

        public abstract HitObject Parse(string text);
    }
}