using Base.Configurations;
using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.Configurations;
using Base.Rulesets.Straight.Rulesets.Objects;
using Base.Rulesets.Straight.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Base.Rulesets.Timing {
    public class LinearScrollingContainer<TObject> : ScrollingContainer<TObject> 
        where TObject : ScrollingHitObject
    {
        private float speed;
        private float screenHeight;
        private float unitScreenHeight;
        private float targetLineHeight;

        public LinearScrollingContainer(ControlPoint controlPoint)
            : base(controlPoint) { }
        
        protected override void load() {
            //DebugCount = Counter.Count++;
            //Debug.Log(((Column)Parent).Pitch + " build this scroller-" + DebugCount);
            FrameworkConfigManager frameworkConfigManager = Parent.GetCache(typeof(FrameworkConfigManager)) as FrameworkConfigManager;
            if (frameworkConfigManager == null) 
                throw new InvalidOperationException(
                    @"Type FrameworkConfigManager is not registered, and is a dependency of LinearScrollingContainer.");

            StraightConfigManager straightConfigManager = Parent.GetCache(typeof(StraightConfigManager)) as StraightConfigManager;
            if (frameworkConfigManager == null) 
                throw new InvalidOperationException(
                    @"Type StraightConfigManager is not registered, and is a dependency of LinearScrollingContainer.");
        
            screenHeight = frameworkConfigManager.Get<int>(FrameworkSetting.Height);
            unitScreenHeight = screenHeight / 100f; // TODO: 改成設定在設定裡
            targetLineHeight = straightConfigManager.Get<float>(StraightSetting.TargetLineHeight);
        }


        // Update is called once per frame
        protected override void update() {
            if (ScrollingHitObject != null) {
                ScrollingHitObject.transform.Translate(Vector2.down * speed * Time.deltaTime);
            } else {
                Debug.Log(((Column)Parent).Pitch + ":this " + DebugCount + ", parent's child's "
                    + ((Column)Parent).HitObjects.SpeedAdjustmentContainers.First().ScrollingContainer.DebugCount);
            }
        }

        public override void Add(DrawableScrollingHitObject<TObject> hitObject) {
            ScrollingHitObject = hitObject;

            // TODO: 把設定高度的動做封裝起來，紙擺一個抽象化的指令
            speed = unitScreenHeight / VisibleTimeRange;
            hitObject.transform.localPosition =
                ScrollingAxes == Axes.X ? new Vector2(speed * ControlPoint.StartTime + targetLineHeight, 0) :
               (ScrollingAxes == Axes.Y ? new Vector2(0, speed * (ControlPoint.StartTime) - unitScreenHeight / 2f + targetLineHeight) : Vector2.zero);
        }
    }
}

public static class Counter {
    public static int Count = 0;
}