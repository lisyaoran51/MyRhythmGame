using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class View : IView {

    public Vector2 Position = new Vector2(0, 0);
    public Vector2 Rotation = new Vector2(0, 0);
    public Vector2 Scale = new Vector2(1f, 1f);
    public Axes RelativePositionAxes;
    public Axes RelativeScaleAxes;

    public double Height;
    public double Width;

    public RectOffset MarginPadding;
    public Color BorderColor;
    public Color BackgroundColor;

    public bool IsSprite;

    public Sprite Sprite;
    public List<string> SpritePaths = new List<string>();

    public string SortingLayerName;
    public int Depth;

    public delegate void SpecificConfig(Drawable drawable);

    public SpecificConfig SpecificConfigFunc;

    public View() {
        SpecificConfigFunc = (d) => {
            // no op
        };
    }

    public virtual bool Config(Drawable drawable) {
        
        //drawable.transform.localPosition = Position;
        drawable.transform.localScale = Scale;
        if(SpritePaths.Count > drawable.SpriteIndex) {
            SpriteRenderer spriteRenderer = drawable.gameObject.AddComponent<SpriteRenderer>();
            Sprite = Resources.Load<Sprite>(SpritePaths[drawable.SpriteIndex]);
            spriteRenderer.sprite = Sprite;
            spriteRenderer.sortingLayerName = SortingLayerName;

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
