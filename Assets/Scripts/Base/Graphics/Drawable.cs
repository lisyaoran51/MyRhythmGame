using Base.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Drawable : Updatable {

    protected bool isSprite;

    protected SpriteRenderer spriteRenderer;

    public SpriteRenderer SpriteRenderer {
        get {
            if (spriteRenderer != null)
                return spriteRenderer;
            spriteRenderer = GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }
    }

    public int SpriteIndex {
        private set; get;
    }

    protected CanvasRenderer canvasRenderer;

    public string DrawableName { protected set; get; }

    public Vector2 Pivot {
        set { 
            // 還不知道怎麼寫
        }
        get {
            if (SpriteRenderer == null)
                throw new InvalidOperationException("Failed to get sprite pivot: " + DrawableName + " dosen't have sprite renderer.");
            if (SpriteRenderer.sprite == null)
                throw new InvalidOperationException("Failed to get sprite pivot: " + DrawableName + " dosen't have sprite.");
            return SpriteRenderer.sprite.pivot;

            /*
            if (spriteRenderer == null) return Vector2.zero;
            Bounds bounds = spriteRenderer.bounds;
            Vector2 position = transform.position;
            Vector2 min = bounds.min;
            Vector2 size = bounds.size;
            Vector2 offsetOfAbsolutePositionRelativelyToMinOfBounds = position - min;
            return new Vector2(
                            offsetOfAbsolutePositionRelativelyToMinOfBounds.x
                                    /
                                  size.x,
                            offsetOfAbsolutePositionRelativelyToMinOfBounds.y
                                    /
                                  size.y
                    );
            */
        }
    }

    public Bounds Bounds {
        get {
            if (SpriteRenderer == null)
                throw new InvalidOperationException("Failed to get sprite bound: " + gameObject.name + " dosen't have sprite renderer.");
            if (SpriteRenderer.sprite == null)
                throw new InvalidOperationException("Failed to get sprite bound: " + DrawableName + "dosen't have sprite.");
            return SpriteRenderer.sprite.bounds;
        }
    }

    protected new void construct(int spriteIndex = 0) {
        base.construct();
        DrawableName = GetType().ToString().Split('.').Last();
        SpriteIndex = spriteIndex;
    }

    private void load(ViewConfig viewConfig) {
        viewConfig.Config(this);
    }

    protected new void Update() {
        base.Update();
    }
}


public enum Axes {

    None = 0,

    X = 1 << 0,
    Y = 1 << 1,

    Both = X | Y
}