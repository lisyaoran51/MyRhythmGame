using Base.Rulesets.Straight.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightView : ViewConfig {

	public StraightView() {
        /*
        views.Add(
            "DrawableNote", new View {
                SpritePath = "DrawableNote",
                });
        views.Add(
            "Column", new View {
                SpritePath = "Column",
                SpecificConfigFunc = (Drawable drawable) => {
                    Column column = drawable as Column;

                    drawable.transform.localPosition = new Vector2(
                        (float)column.Pitch * 10f,
                        drawable.transform.localPosition.y
                    );
                    return true;
                }
            }
        );
        */
    }
}
