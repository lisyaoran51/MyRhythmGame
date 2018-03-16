using UnityEngine;
using System;
//using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Base.Threading {

    public class ScheduledDelegate : IComparable<ScheduledDelegate> {
        public ScheduledDelegate(Action task, double executionTime, double repeatInterval = -1) {
            ExecutionTime = executionTime;
            RepeatInterval = repeatInterval;
            this.task = task;
        }

        /// <summary>
        /// The work task.
        /// </summary>
        private readonly Action task;

        /// <summary>
        /// Set to true to skip scheduled executions until we are ready.
        /// </summary>
        internal bool Waiting;

        public void Wait() {
            Waiting = true;
        }

        public void Continue() {
            Waiting = false;
        }

        public void RunTask() {
            if (!Waiting)
                task();
            Completed = true;
        }

        public bool Completed;

        public bool Cancelled { get; private set; }

        public void Cancel() {
            Cancelled = true;
        }

        /// <summary>
        /// The earliest ElapsedTime value at which we can be executed.
        /// </summary>
        public double ExecutionTime;

        /// <summary>
        /// Time in milliseconds between repeats of this task. -1 means no repeats.
        /// </summary>
        public double RepeatInterval;

        public int CompareTo(ScheduledDelegate other) {
            return ExecutionTime == other.ExecutionTime ? -1 : ExecutionTime.CompareTo(other.ExecutionTime);
        }
    }

    /// <summary>
    /// A scheduler which doesn't require manual updates (and never uses the main thread).
    /// </summary>
    public class ThreadedScheduler : Scheduler {
        private bool isDisposed;
        private readonly Thread workerThread;

        public ThreadedScheduler(string threadName = null, int runInterval = 50) {
            workerThread = new Thread(() => {
                while (!isDisposed) {
                    Update();
                    Thread.Sleep(runInterval);
                }
            }) {
                IsBackground = true,
                Name = threadName
            };

            workerThread.Start();
        }

        protected override void Dispose(bool disposing) {
            isDisposed = true;

            workerThread.Join();

            base.Dispose(disposing);
        }

        protected override bool IsMainThread {
            set { throw new InvalidOperationException("ThreadedScheduler.IsMainThread is for get only."); }
            get { return false; }
        }
    }
}
