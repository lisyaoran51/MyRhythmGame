using Base.Threading;
using Base.Utils.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Base.Utils {

    public abstract class Updated : IUpdatable {

        #region Updated

        public bool IsLoaded { get { return loadState >= LoadState.Ready; } }

        private volatile LoadState loadState = LoadState.NotLoaded;

        private Updatable parent;

        public Updatable Parent {
            set {
                value.AddSubTree(this);
                parent = value;
                load();
                loadState = LoadState.Ready;
            }
            get {
                return parent;
            }
        }

        private Updated updatedParent;

        public Updated UpdatedParent {
            set {
                value.AddSubTree(this);
                updatedParent = value;
                load();
                loadState = LoadState.Ready;
            }
            get {
                return updatedParent;
            }
        }
        
        private event Action onUpdateSubTree;

        protected abstract void load();

        protected abstract void update();

        public virtual void UpdateSubTree() {
            if (!IsLoaded) return;

            if (scheduler != null) {
                int amountScheduledTasks = scheduler.Update();
                //FrameStatistics.Add(StatisticsCounterType.ScheduleInvk, amountScheduledTasks);
            }

            update();
            if (onUpdateSubTree != null)
                onUpdateSubTree.Invoke();
        }
        
        public void AddSubTree(Updatable subObject) {
            onUpdateSubTree += () => { subObject.UpdateSubTree(); };
        }

        public void AddSubTree(Updated subObject) {
            onUpdateSubTree += () => { subObject.UpdateSubTree(); };

        }

        #endregion


        #region Schedulable

        /*
         * 把schedule的功能放到updated裡面來，暫時不把兩個公能分開
         * 
         */

        private Scheduler scheduler;

        protected Scheduler Scheduler {
            set { throw new InvalidOperationException("Schedulable.Scheduler is for get only."); }
            get {
                return scheduler;
            }
        }

        protected double Delay {
            private set; get;
        }

        protected internal ScheduledDelegate Schedule(Action action, double delay) {
            if (Scheduler == null)
                scheduler = new Scheduler();
            return Scheduler.AddDelayed(action, delay);
        }

        protected internal ScheduledDelegate Schedule(Action action) {
            if (Scheduler == null)
                scheduler = new Scheduler();
            return Scheduler.AddDelayed(action, Delay);
        }

        #endregion
    }
}