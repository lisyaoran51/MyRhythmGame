using Base.Threading;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils {
    public abstract class Schedulable : Updatable{
        
        private Scheduler scheduler;

        protected Scheduler Scheduler {
            set { throw new InvalidOperationException("Schedulable.Scheduler is for get only."); }
            get {
                return scheduler;
            }
        }

        protected new void Update() {
            base.Update();
            update();
        }

        /// <summary>
        /// 根據載入狀態，考慮是否執行scheduler裡的工作
        /// </summary>
        private void update() {
            if (LoadState < LoadState.Ready)
                return;

            if (LoadState == LoadState.Ready)
                //loadComplete();
                return;

            Debug.Assert(LoadState == LoadState.Loaded);

            if (scheduler != null) {
                int amountScheduledTasks = scheduler.Update();
                //FrameStatistics.Add(StatisticsCounterType.ScheduleInvk, amountScheduledTasks);
            }
        }

        protected double Delay {
            private set; get;
        }

        protected internal ScheduledDelegate Schedule(Action action, double delay) {
            return Scheduler.AddDelayed(action, delay);
        }

        protected internal ScheduledDelegate Schedule(Action action) {
            return Scheduler.AddDelayed(action, Delay);
        }
    }
}