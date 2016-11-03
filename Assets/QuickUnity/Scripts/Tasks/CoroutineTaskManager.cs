/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2016 Jerry Lee
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using QuickUnity.Extensions;
using QuickUnity.Patterns;
using System.Collections.Generic;

namespace QuickUnity.Tasks
{
    /// <summary>
    /// Class to globally manage CoroutineTask objects.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.SingletonBehaviour{QuickUnity.Tasks.CoroutineTaskManager}"/>
    public class CoroutineTaskManager : SingletonBehaviour<CoroutineTaskManager>
    {
        /// <summary>
        /// The CoroutineTask object list.
        /// </summary>
        private List<ICoroutineTask> m_tasks;

        /// <summary>
        /// Called when script receive message Awake.
        /// </summary>
        protected override void OnAwake()
        {
            base.OnAwake();

            m_tasks = new List<ICoroutineTask>();
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled () or inactive.
        /// </summary>
        private void OnDisable()
        {
            if (m_tasks != null)
            {
                m_tasks.ForEach(coroutineTask =>
                {
                    coroutineTask.Stop();
                });
            }
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            RemoveAllTasks();
            m_tasks = null;

            StopAllCoroutines();
        }

        #region Public Functions

        /// <summary>
        /// Contains the task.
        /// </summary>
        /// <param name="coroutineTask">The coroutine task.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ContainTask(ICoroutineTask coroutineTask)
        {
            if (m_tasks != null && coroutineTask != null)
            {
                return m_tasks.Contains(coroutineTask);
            }

            return false;
        }

        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        public void AddTask(ICoroutineTask task, bool autoStart = true)
        {
            if (m_tasks != null && task != null)
            {
                m_tasks.AddUnique(task);
                task.AddEventListener<CoroutineTaskEvent>(CoroutineTaskEvent.CoroutineTaskStart, OnTaskStart);
                task.AddEventListener<CoroutineTaskEvent>(CoroutineTaskEvent.CoroutineTaskStop, OnTaskStop);

                if (autoStart)
                {
                    task.Start();
                }
            }
        }

        /// <summary>
        /// Starts all tasks.
        /// </summary>
        public void StartAllTasks()
        {
            if (m_tasks != null)
            {
                m_tasks.ForEach(task =>
                {
                    task.Start();
                });
            }
        }

        /// <summary>
        /// Pauses all tasks.
        /// </summary>
        public void PauseAllTasks()
        {
            if (m_tasks != null)
            {
                m_tasks.ForEach(task =>
                {
                    task.Pause();
                });
            }
        }

        /// <summary>
        /// Resumes all tasks.
        /// </summary>
        public void ResumeAllTasks()
        {
            if (m_tasks != null)
            {
                m_tasks.ForEach(task =>
                {
                    task.Resume();
                });
            }
        }

        /// <summary>
        /// Stops all tasks.
        /// </summary>
        public void StopAllTasks()
        {
            if (m_tasks != null)
            {
                m_tasks.ForEach(task =>
                {
                    task.Stop();
                });
            }
        }

        /// <summary>
        /// Removes the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        /// <returns><c>true</c> if task is successfully removed; <c>false</c> otherwise.</returns>
        public bool RemoveTask(ICoroutineTask task, bool autoStop = true)
        {
            if (task != null)
            {
                if (autoStop)
                {
                    task.Stop();
                }

                task.RemoveEventListener<CoroutineTaskEvent>(CoroutineTaskEvent.CoroutineTaskStart, OnTaskStart);
                task.RemoveEventListener<CoroutineTaskEvent>(CoroutineTaskEvent.CoroutineTaskStop, OnTaskStop);

                if (m_tasks != null)
                {
                    return m_tasks.Remove(task);
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all tasks.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        public void RemoveAllTasks(bool autoStop = true)
        {
            if (m_tasks != null)
            {
                m_tasks.ForEach(task =>
                {
                    if (task != null)
                    {
                        if (autoStop)
                        {
                            task.Stop();
                        }

                        task.RemoveEventListener<CoroutineTaskEvent>(CoroutineTaskEvent.CoroutineTaskStart, OnTaskStart);
                        task.RemoveEventListener<CoroutineTaskEvent>(CoroutineTaskEvent.CoroutineTaskStop, OnTaskStop);
                    }
                });

                m_tasks.Clear();
            }
        }

        #endregion Public Functions

        /// <summary>
        /// Runs the task.
        /// </summary>
        /// <param name="task">The task.</param>
        private void RunTask(ICoroutineTask task)
        {
            if (task != null)
            {
                StartCoroutine(task.CoroutineWrapper());
            }
        }

        /// <summary>
        /// Stops the task.
        /// </summary>
        /// <param name="task">The task.</param>
        private void StopTask(ICoroutineTask task)
        {
            if (task != null)
            {
                StopCoroutine(task.CoroutineWrapper());
            }
        }

        /// <summary>
        /// Called when [task start].
        /// </summary>
        /// <param name="taskEvent">The task event.</param>
        private void OnTaskStart(CoroutineTaskEvent taskEvent)
        {
            if (taskEvent != null && taskEvent.coroutineTask != null)
            {
                RunTask(taskEvent.coroutineTask);
            }
        }

        /// <summary>
        /// Called when [task stop].
        /// </summary>
        /// <param name="taskEvent">The task event.</param>
        private void OnTaskStop(CoroutineTaskEvent taskEvent)
        {
            if (taskEvent != null && taskEvent.coroutineTask != null)
            {
                StopTask(taskEvent.coroutineTask);
            }
        }
    }
}