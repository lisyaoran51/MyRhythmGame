using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : IView {

    public Vector2 Position;
    public Vector2 Rotation;
    public Vector2 Scale;
    public Axes RelativePositionAxes;
    public Axes RelativeScaleAxes;

    public double Height;
    public double Width;

    public RectOffset MarginPadding;
    public Color BorderColor;
    public Color BackgroundColor;

    public bool IsSprite;

    public Sprite Sprite;
    public string SpritePath;
    public SortingLayer Layer;
    public int Depth;

    public delegate void SpecificConfig(Drawable drawable);

    public SpecificConfig SpecificConfigFunc;

    public View() {
        SpecificConfigFunc = (d) => {
            // no op
        };
    }

    public virtual bool Config(Drawable drawable) {
        
        drawable.transform.localPosition = Position;
        drawable.transform.localScale = Scale;

        if(SpritePath != null) {
            SpriteRenderer spriteRenderer = drawable.gameObject.AddComponent<SpriteRenderer>();
            Sprite = Resources.Load<Sprite>(SpritePath);
        }
        SpecificConfigFunc(drawable);
        return true;
    }

    public virtual bool TriggerOnMouseClick(Drawable drawable) {
        return true;
    }

    public virtual bool TriggerOnMouseDown(Drawable drawable) {
        return true;
    }

    public virtual bool TriggerOnMouseUp(Drawable drawable) {
        return true;
    }

    public virtual bool TriggeOnKeyDown(Drawable drawable) {
        return true;
    }

    public virtual bool TriggeOnKeyUp(Drawable drawable) {
        return true;
    }

    public virtual bool TriggerOnFocus(Drawable drawable) {
        return true;
    }

    public virtual bool TriggerOnFocusLost(Drawable drawable) {
        return true;
    }

}
