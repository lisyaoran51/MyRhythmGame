using UnityEngine;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Base.Extensions;

namespace Base.Threading {
    /// <summary>
    /// Marshals delegates to run from the Scheduler's base thread in a threadsafe manner
    /// </summary>
    public class Scheduler : IDisposable {
        private readonly Queue<Action> schedulerQueue = new Queue<Action>();
        private readonly List<ScheduledDelegate> timedTasks = new List<ScheduledDelegate>();
        private readonly List<ScheduledDelegate> perUpdateTasks = new List<ScheduledDelegate>();
        private int mainThreadId;

        //private IClock clock; // unused in unity
        private double currentTime {
            set {
                throw new InvalidOperationException("Scheduler.currentTime is for get only.");
            }
            get {
                return Time.time;
            }
        }

        /// <summary>
        /// The base thread is assumed to be the the thread on which the constructor is run.
        /// </summary>
        public Scheduler() {
            SetCurrentThread();
        }

        /// <summary>
        /// The base thread is assumed to be the the thread on which the constructor is run.
        /// </summary>
        public Scheduler(Thread mainThread) {
            SetCurrentThread(mainThread);
        }

        /// <summary>
        /// Returns whether we are on the main thread or not.
        /// </summary>
        protected virtual bool IsMainThread {
            set {
                throw new InvalidOperationException("Scheduler.IsMainThread is for get only.");
            }
            get {
                return Thread.CurrentThread.ManagedThreadId == mainThreadId;
            }
        }


        private readonly List<ScheduledDelegate> tasksToSchedule = new List<ScheduledDelegate>();
        private readonly List<ScheduledDelegate> tasksToRemove = new List<ScheduledDelegate>();

        /// <summary>
        /// Run any pending work tasks.
        /// </summary>
        /// <returns>true if any tasks were run.</returns>
        public virtual int Update() {
            lock (timedTasks) {
                double currentTimeLocal = currentTime;

                if (timedTasks.Count > 0) {
                    foreach (var sd in timedTasks) {
                        if (sd.ExecutionTime <= currentTimeLocal) {
                            tasksToRemove.Add(sd);

                            if (sd.Cancelled) continue;

                            schedulerQueue.Enqueue(sd.RunTask);

                            if (sd.RepeatInterval >= 0) {
                                if (timedTasks.Count > 1000)
                                    throw new ArgumentException("Too many timed tasks are in the queue!");

                                sd.ExecutionTime += sd.RepeatInterval;
                                tasksToSchedule.Add(sd);
                            }
                        }
                    }

                    foreach (var t in tasksToRemove)
                        timedTasks.Remove(t);

                    tasksToRemove.Clear();

                    foreach (var t in tasksToSchedule)
                        timedTasks.AddInPlace(t);

                    tasksToSchedule.Clear();
                }
            }

            for (int i = 0; i < perUpdateTasks.Count; i++) {
                ScheduledDelegate task = perUpdateTasks[i];
                if (task.Cancelled) {
                    perUpdateTasks.RemoveAt(i--);
                    continue;
                }

                schedulerQueue.Enqueue(task.RunTask);
            }

            int countRun = 0;

            Action action;
            while (schedulerQueue.TryDequeue(out action)) {
                //todo: error handling
                action.Invoke();
                countRun++;
            }

            return countRun;
        }

        /// <summary>
        /// Cancel any pending work tasks.
        /// </summary>
        public void CancelDelayedTasks() {
            lock (timedTasks) {
                foreach (var t in timedTasks)
                    t.Cancel();
                timedTasks.Clear();
            }
        }

        internal void SetCurrentThread(Thread thread) {
            // don't use this
            mainThreadId = thread.ManagedThreadId;
        }

        internal void SetCurrentThread() {
            mainThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        /// <summary>
        /// Add a task to be scheduled.
        /// </summary>
        /// <param name="task">The work to be done.</param>
        /// <param name="forceScheduled">If set to false, the task will be executed immediately if we are on the main thread.</param>
        /// <returns>Whether we could run without scheduling</returns>
        public bool Add(Action task, bool forceScheduled = true) {
            if (!forceScheduled && IsMainThread) {
                //We are on the main thread already - don't need to schedule.
                task.Invoke();
                return true;
            }

            schedulerQueue.Enqueue(task);

            return false;
        }

        public void Add(ScheduledDelegate task) {
            lock (timedTasks) {
                if (task.RepeatInterval == 0)
                    perUpdateTasks.Add(task);
                else
                    timedTasks.AddInPlace(task);
            }
        }

        /// <summary>
        /// Add a task which will be run after a specified delay.
        /// </summary>
        /// <param name="task">The work to be done.</param>
        /// <param name="timeUntilRun">Milliseconds until run.</param>
        /// <param name="repeat">Whether this task should repeat.</param>
        public ScheduledDelegate AddDelayed(Action task, double timeUntilRun, bool repeat = false) {
            // We are locking here already to make sure we have no concurrent access to currentTime
            lock (timedTasks) {
                ScheduledDelegate del = new ScheduledDelegate(task, currentTime + timeUntilRun, repeat ? timeUntilRun : -1);
                Add(del);
                return del;
            }
        }

        /// <summary>
        /// Adds a task which will only be run once per frame, no matter how many times it was scheduled in the previous frame.
        /// </summary>
        /// <param name="task">The work to be done.</param>
        /// <returns>Whether this is the first queue attempt of this work.</returns>
        public bool AddOnce(Action task) {
            if (schedulerQueue.Contains(task))
                return false;

            schedulerQueue.Enqueue(task);

            return true;
        }

        #region IDisposable Support

        private bool isDisposed; // To detect redundant calls

        protected virtual void Dispose(bool disposing) {
            if (!isDisposed) {
                isDisposed = true;
            }
        }

        public void Dispose() {
            Dispose(true);
        }

        #endregion
    }
}