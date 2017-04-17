/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2017 Jerry Lee
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
using System.Collections;

namespace QuickUnity.Tasks
{
    /// <summary>
    /// A base <c>ICoroutineTask</c> implementation.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.EventDispatcher"/>
    /// <seealso cref="QuickUnity.Tasks.ICoroutineTask"/>
    public class CoroutineTask : EventDispatcher, ICoroutineTask
    {
        /// <summary>
        /// The state of task.
        /// </summary>
        protected TaskState m_taskState;

        #region ITask Interface

        /// <summary>
        /// Gets the state of the task.
        /// </summary>
        /// <value>The state of the task.</value>
        public TaskState taskState
        {
            get { return m_taskState; }
        }

        #endregion ITask Interface

        /// <summary>
        /// The coroutine function to be executed.
        /// </summary>
        protected IEnumerator m_coroutine;

        /// <summary>
        /// Initializes a new instance of the <see cref="CoroutineTask"/> class.
        /// </summary>
        /// <param name="coroutine">The coroutine.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        public CoroutineTask(IEnumerator coroutine, bool autoStart = true)
        {
            m_taskState = TaskState.Stop;
            m_coroutine = coroutine;
            CoroutineTaskManager.instance.AddTask(this, autoStart);
        }

        #region ICroutineTask Interface

        /// <summary>
        /// The wrapper of coroutine.
        /// </summary>
        /// <returns>The enumerator object.</returns>
        public IEnumerator CoroutineWrapper()
        {
            yield return null;

            while (m_taskState != TaskState.Stop)
            {
                if (m_taskState == TaskState.Pause)
                {
                    yield return null;
                }
                else
                {
                    // Running.
                    if (m_coroutine != null && m_coroutine.MoveNext())
                        yield return m_coroutine.Current;
                    else
                        Stop();
                }
            }
        }

        #endregion ICroutineTask Interface

        #region ITask Interface

        /// <summary>
        /// Starts this task.
        /// </summary>
        public void Start()
        {
            m_taskState = TaskState.Running;
            DispatchEvent(new CoroutineTaskEvent(CoroutineTaskEvent.CoroutineTaskStart, this));
        }

        /// <summary>
        /// Pauses this task.
        /// </summary>
        public void Pause()
        {
            m_taskState = TaskState.Pause;
            DispatchEvent(new CoroutineTaskEvent(CoroutineTaskEvent.CoroutineTaskPause, this));
        }

        /// <summary>
        /// Resumes this task.
        /// </summary>
        public void Resume()
        {
            m_taskState = TaskState.Running;
            DispatchEvent(new CoroutineTaskEvent(CoroutineTaskEvent.CoroutineTaskResume, this));
        }

        /// <summary>
        /// Stops this task.
        /// </summary>
        public void Stop()
        {
            m_taskState = TaskState.Stop;
            DispatchEvent(new CoroutineTaskEvent(CoroutineTaskEvent.CoroutineTaskStop, this));
        }

        /// <summary>
        /// Destroys this task instance.
        /// </summary>
        public void Destroy()
        {
            CoroutineTaskManager.instance.RemoveTask(this);
        }

        #endregion ITask Interface
    }
}