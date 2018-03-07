using Base.Configurations;
using Base.IO.SerialPorts;
using Base.Rulesets.Objects.Drawables;
using Base.Rulesets.Straight.Configurations;
using Base.Rulesets.Straight.IO.SerialPorts;
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
        private float unitScreenHeight;     // ?
        private float targetLineHeight;
        private float whiteKeyLength;
        private float whiteKeyTarget;
        private float blackKeyLength;
        private float blackKeyTarget;
        private float pixelToMillimeter;

        private SerialPortManager serialPortManager;

        public bool IsModFlowOut = false;

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

            serialPortManager = Parent.GetCache(typeof(SerialPortManager)) as SerialPortManager;
            if (serialPortManager == null)
                throw new InvalidOperationException(
                    @"Type SerialPortManager is not registered, and is a dependency of LinearScrollingContainer.");


            screenHeight        = frameworkConfigManager.Get<int>(FrameworkSetting.Height);
            unitScreenHeight    = screenHeight / 100f;                                                  // TODO: 改成設定在設定裡
                                                                                                            // 為什麼要 / 100f?
            targetLineHeight    = straightConfigManager.Get<float>(StraightSetting.TargetLineHeight);
            whiteKeyLength      = straightConfigManager.Get<float>(StraightSetting.WhiteKeyLength);
            whiteKeyTarget      = straightConfigManager.Get<float>(StraightSetting.WhiteKeyTarget);
            blackKeyLength      = straightConfigManager.Get<float>(StraightSetting.BlackKeyLength);
            blackKeyTarget      = straightConfigManager.Get<float>(StraightSetting.BlackKeyTarget);
            pixelToMillimeter   = straightConfigManager.Get<float>(StraightSetting.PixelToMillimeter);
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

            speed = unitScreenHeight / VisibleTimeRange;

            if (IsModFlowOut) {
                /*
                 * 如果是音符流出螢幕模式，會先判斷是黑鍵還是白鍵，然後把音符掉下的位置調整往下移
                 */
                bool isBlackKey = false;
                float offset = 0f; ;

                StraightHitObject sho = hitObject.HitObject as StraightHitObject;
                if (sho != null) {
                    isBlackKey = sho.IsBlackKey();
                    offset = isBlackKey ? blackKeyLength * blackKeyTarget / pixelToMillimeter / 100f :
                                          whiteKeyLength * whiteKeyTarget / pixelToMillimeter / 100f;   // 為什麼是/ 100f已不可考
                }

                hitObject.transform.localPosition =
                    ScrollingAxes == Axes.X ? new Vector2(speed * ControlPoint.StartTime + targetLineHeight, 0) :
                   (ScrollingAxes == Axes.Y ? new Vector2(0, speed * (ControlPoint.StartTime) - unitScreenHeight / 2f - offset ) : Vector2.zero);

                /*
                 * 在音符碰觸到螢幕畫面下方時，預設好的Scheduler會在那個時後送出Event到Arduino
                 */
                Schedule(() => {
                    if(sho != null) {
                        // TODO: StraightEventType應該完全對應到Note的type，但現在只有Note，就不用特別調
                        serialPortManager.WriteToArduino(
                            new StraightEvent(StraightEventType.Note, sho.Pitch, offset / speed).ToString()
                            );
                    }
                }, (speed * (ControlPoint.StartTime) - unitScreenHeight / 2f - offset) / speed + 1f);

            } else {
                // TODO: 把設定高度的動做封裝起來，紙擺一個抽象化的指令
                hitObject.transform.localPosition =
                    ScrollingAxes == Axes.X ? new Vector2(speed * ControlPoint.StartTime + targetLineHeight, 0) :
                   (ScrollingAxes == Axes.Y ? new Vector2(0, speed * (ControlPoint.StartTime) - unitScreenHeight / 2f + targetLineHeight) : Vector2.zero);
            }
            
        }
    }
}

public static class Counter {
    public static int Count = 0;
}