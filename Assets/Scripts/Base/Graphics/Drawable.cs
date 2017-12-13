using Base.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Drawable : ChildAddable {
    protected bool isSprite;
    protected SpriteRenderer spriteRenderer;
    protected CanvasRenderer canvasRenderer;

    public string DrawableName { protected set; get; }

    private void construct() {
        DrawableName = GetType().ToString().Split('.').Last();
    }
    private void load(ViewConfig viewConfig) {
        viewConfig.Config(this);
    }
}


public enum Axes {

    None = 0,

    X = 1 << 0,
    Y = 1 << 1,

    Both = X | Y
}