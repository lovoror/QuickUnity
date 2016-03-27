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

using QuickUnity.Events;
using System;
using System.Collections;

namespace QuickUnity.Tasks
{
    /// <summary>
    /// The enum state of task.
    /// </summary>
    public enum TaskState
    {
        Running,
        Pause,
        Stop
    }

    /// <summary>
    /// A base <c>ITask</c> implementation.
    /// </summary>
    public class Task : EventDispatcher, ITask
    {
        /// <summary>
        /// The global unique identifier
        /// </summary>
        protected Guid m_guid = Guid.Empty;

        /// <summary>
        /// Gets the global unique identifier.
        /// </summary>
        /// <value>
        /// The global unique identifier.
        /// </value>
        public Guid guid
        {
            get
            {
                if (m_guid == Guid.Empty)
                    m_guid = Guid.NewGuid();

                return m_guid;
            }
        }

        /// <summary>
        /// <c>true</c> if [removed from TaskManager when stop]; otherwise, <c>false</c>.
        /// </summary>
        protected bool m_removedWhenStop = false;

        /// <summary>
        /// Gets or sets a value indicating whether [removed from TaskManager when stop].
        /// </summary>
        /// <value>
        /// <c>true</c> if [removed from TaskManager when stop]; otherwise, <c>false</c>.
        /// </value>
        public bool removedWhenStop
        {
            get { return m_removedWhenStop; }
            set { m_removedWhenStop = value; }
        }

        /// <summary>
        /// Whether this task is running or not.
        /// </summary>
        protected bool m_running = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITask" /> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool running
        {
            get { return m_running; }
        }

        /// <summary>
        /// The task state.
        /// </summary>
        protected TaskState m_taskState;

        /// <summary>
        /// Gets the state of the task.
        /// </summary>
        /// <value>The state of the task.</value>
        public TaskState taskState
        {
            get { return m_taskState; }
        }

        /// <summary>
        /// The function need to be executed.
        /// </summary>
        protected IEnumerator m_routine;

        /// <summary>
        /// Gets the function need to be executed.
        /// </summary>
        /// <value>The routine.</value>
        public IEnumerator routine
        {
            get { return m_routine; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Task" /> class.
        /// </summary>
        /// <param name="routine">The function need to be routine.</param>
        /// <param name="removedWhenStop">if set to <c>true</c> [removed from TaskManager when stop].</param>
        public Task(IEnumerator routine, bool removedWhenStop = false)
            : base()
        {
            m_routine = routine;
            m_removedWhenStop = removedWhenStop;
        }

        /// <summary>
        /// The wrapper of routine.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        public IEnumerator RoutineWrapper()
        {
            yield return null;

            while (m_running)
            {
                if (m_taskState == TaskState.Pause)
                {
                    yield return null;
                }
                else
                {
                    if (m_routine != null && m_routine.MoveNext())
                        yield return m_routine.Current;
                    else
                        Stop();
                }
            }
        }

        /// <summary>
        /// Starts this task.
        /// </summary>
        public void Start()
        {
            m_running = true;
            m_taskState = TaskState.Running;
            DispatchEvent(new TaskEvent(TaskEvent.TaskStart, this));
        }

        /// <summary>
        /// Stops this task.
        /// </summary>
        public void Stop()
        {
            m_running = false;
            m_taskState = TaskState.Stop;
            DispatchEvent(new TaskEvent(TaskEvent.TaskStop, this));
        }

        /// <summary>
        /// Pauses this task.
        /// </summary>
        public void Pause()
        {
            m_taskState = TaskState.Pause;
            DispatchEvent(new TaskEvent(TaskEvent.TaskPause, this));
        }

        /// <summary>
        /// Resumes this task.
        /// </summary>
        public void Resume()
        {
            m_taskState = TaskState.Running;
            DispatchEvent(new TaskEvent(TaskEvent.TaskResume, this));
        }
    }
}