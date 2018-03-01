using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.UI;
using Base.Rulesets.Objects.Drawables;
using System;
using System.Linq;
using Base.Rulesets.Timing;

namespace Base.UI {
    public class ScrollingHitObjectContainer<TObject> : HitObjectContainer 
        where TObject : ScrollingHitObject
    {

        public float VisibleTimeRange;

        public bool IsModFlowOut;

        //protected new List<DrawableScrollingHitObject<ScrollingHitObject>> hitObjects;

        private List<SpeedAdjustmentContainer<TObject>> speedAdjustmentContainers = new List<SpeedAdjustmentContainer<TObject>>();

        private List<SpeedAdjustmentContainer<TObject>> defaultSpeedAdjustments = new List<SpeedAdjustmentContainer<TObject>>();

        public List<SpeedAdjustmentContainer<TObject>> SpeedAdjustmentContainers {
            get { return speedAdjustmentContainers; }
        }

        private Axes scrollingAxes;

        public ScrollingHitObjectContainer(Axes scrollingAxes) {
            this.scrollingAxes = scrollingAxes;
        }


        /// <summary>
        /// 
        /// 流程： ScrollingRulesetContainer.load() -> StraightPlayField.ApplySpeedAdjustment(every timing point)
        ///        -> Column.ApplySpeedAdjustment(specific timing point) -> HitObjContainer.AddSpeedAdjustment(create new container)
        /// 
        /// </summary>
        /// <param name="speedAdjustmentContainer"></param>
        public void AddSpeedAdjustment(SpeedAdjustmentContainer<TObject> speedAdjustmentContainer) 
        {
            speedAdjustmentContainer.ScrollingAxes = scrollingAxes;
            speedAdjustmentContainer.VisibleTimeRange = VisibleTimeRange;
            speedAdjustmentContainer.IsModFlowOut = IsModFlowOut;
            SpeedAdjustmentContainers.Add(speedAdjustmentContainer);
            speedAdjustmentContainer.Parent = Parent;                       /*執行這行時，會執行load()*/
            /*
             * 把default speed adjustment裡的元素拿出來，擺進現在的speed adjust裡
             */
            var defaultSpeedAdjust = adjustmentContainerAt(speedAdjustmentContainer.ControlPoint.StartTime, true);
            speedAdjustmentContainer.Add(defaultSpeedAdjust.ScrollingContainer.ScrollingHitObject);
            defaultSpeedAdjustments.Remove(defaultSpeedAdjust);
        }

        public override void Add(DrawableHitObject hitObject) {
            var h = hitObject as DrawableScrollingHitObject<TObject>;
            if (h == null)
                throw new ArgumentException("Failed to Add drawable hit object: input value should be in DrawableScrollingHitObject type.");

            var speedAdjustment = adjustmentContainerAt(h.HitObject.StartTime);
            if (speedAdjustment != null)
                speedAdjustment.Add(h);
            else {
                speedAdjustment = new SpeedAdjustmentContainer<TObject>(
                    new ControlPoint { StartTime = h.HitObject.StartTime }
                );
                speedAdjustment.Add(h);
                defaultSpeedAdjustments.Add(speedAdjustment);
                /*
                 * what is default for?
                 */
            }
        }

        private SpeedAdjustmentContainer<TObject> adjustmentContainerAt(float startTime, bool findDefault = false) {
            /*
             * 先判斷要找default還是一般的speed adjust，然後去那一串尋找
             */
            var containers = findDefault ?
                defaultSpeedAdjustments.FindAll(
                delegate (SpeedAdjustmentContainer<TObject> s) {
                    if (s.ControlPoint.StartTime.Equals(startTime)) return true;
                    else return false;
                })
                :
                SpeedAdjustmentContainers.FindAll(
                delegate (SpeedAdjustmentContainer<TObject> s) {
                    if (s.ControlPoint.StartTime.Equals(startTime)) return true;
                    else return false;
                });

            if (containers.Count > 0)
                return containers.FirstOrDefault<SpeedAdjustmentContainer<TObject>>();
            else return null;
        }
    }
}