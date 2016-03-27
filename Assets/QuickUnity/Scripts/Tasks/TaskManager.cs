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

using QuickUnity.Patterns;
using System.Collections.Generic;

namespace QuickUnity.Tasks
{
    /// <summary>
    /// Manage all task objects.
    /// </summary>
    public class TaskManager : MonoBehaviourSingleton<TaskManager>
    {
        /// <summary>
        /// The task dictionary.
        /// </summary>
        private Dictionary<string, ITask> m_tasks;

        /// <summary>
        /// Awake this script.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            // Initialize task list.
            m_tasks = new Dictionary<string, ITask>();
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove all tasks.
            RemoveAllTasks();
            m_tasks = null;
        }

        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <param name="task">The task object.</param>
        public void AddTask(string taskName, ITask task)
        {
            if (string.IsNullOrEmpty(taskName) || task == null)
                return;

            m_tasks.Add(taskName, task);
            task.AddEventListener(TaskEvent.TaskStop, OnTaskStop);
            StartCoroutine(task.RoutineWrapper());
        }

        /// <summary>
        /// Gets the task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        /// <returns>ITask.</returns>
        public ITask GetTask(string taskName)
        {
            if (!string.IsNullOrEmpty(taskName))
            {
                if (m_tasks.ContainsKey(taskName))
                    return m_tasks[taskName];
            }

            return null;
        }

        /// <summary>
        /// Gets the name of the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <returns>System.String.</returns>
        public string GetTaskName(ITask task)
        {
            if (task != null)
            {
                foreach (KeyValuePair<string, ITask> kvp in m_tasks)
                {
                    if (kvp.Value == task)
                        return kvp.Key;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes the task.
        /// </summary>
        /// <param name="taskName">Name of the task.</param>
        public void RemoveTask(string taskName)
        {
            if (!string.IsNullOrEmpty(taskName))
            {
                ITask task = GetTask(taskName);

                if (task != null)
                {
                    task.RemoveEventListenersByTarget(this);
                    StopCoroutine(task.RoutineWrapper());
                    m_tasks.Remove(taskName);
                }
            }
        }

        /// <summary>
        /// Removes the task.
        /// </summary>
        /// <param name="task">The task.</param>
        public void RemoveTask(ITask task)
        {
            string taskName = GetTaskName(task);

            if (!string.IsNullOrEmpty(taskName))
                RemoveTask(taskName);
        }

        /// <summary>
        /// Removes all tasks.
        /// </summary>
        public void RemoveAllTasks()
        {
            foreach (KeyValuePair<string, ITask> kvp in m_tasks)
            {
                ITask task = kvp.Value;

                if (task != null)
                    task.RemoveEventListenersByTarget(this);
            }

            StopAllCoroutines();
            m_tasks.Clear();
        }

        /// <summary>
        /// Called when [task stop].
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void OnTaskStop(QuickUnity.Events.Event evt)
        {
            TaskEvent taskEvent = (TaskEvent)evt;
            ITask task = taskEvent.task;

            if (taskEvent.task != null)
            {
                StopCoroutine(taskEvent.task.RoutineWrapper());

                // Remove the task automatically.
                if (task.removedWhenStop)
                    RemoveTask(task);
            }
        }
    }
}