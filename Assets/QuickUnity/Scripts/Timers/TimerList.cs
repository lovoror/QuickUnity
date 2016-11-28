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
using System;
using System.Collections.Generic;

namespace QuickUnity.Timers
{
    /// <summary>
    /// Represents timer object list.
    /// </summary>
    /// <seealso cref="QuickUnity.Timers.ITimerList"/>
    public class TimerList : ITimerList
    {
        /// <summary>
        /// The timer list.
        /// </summary>
        private List<ITimer> m_timers;

        /// <summary>
        /// Gets all the timers.
        /// </summary>
        /// <value>All the timers.</value>
        public List<ITimer> timers
        {
            get { return m_timers; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerList"/> class.
        /// </summary>
        public TimerList()
        {
            m_timers = new List<ITimer>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerList"/> class.
        /// </summary>
        /// <param name="collection">The timer object collection.</param>
        public TimerList(IEnumerable<ITimer> collection)
        {
            m_timers = new List<ITimer>(collection);
        }

        /// <summary>
        /// Performs the specified action on each element of the timer list.
        /// </summary>
        /// <param name="action">The action.</param>
        public void ForEach(Action<ITimer> action)
        {
            if (m_timers != null)
            {
                m_timers.ForEach(action);
            }
        }

        /// <summary>
        /// Determines whether contains the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <returns><c>true</c> if contains the timer object; otherwise, <c>false</c>.</returns>
        public bool Contains(ITimer timer)
        {
            if (m_timers != null && timer != null)
            {
                return m_timers.Contains(timer);
            }

            return false;
        }

        /// <summary>
        /// Adds the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        public void Add(ITimer timer, bool autoStart = false)
        {
            if (m_timers != null && timer != null)
            {
                m_timers.AddUnique(timer);

                if (autoStart)
                {
                    timer.Start();
                }
            }
        }

        /// <summary>
        /// Adds the timers.
        /// </summary>
        /// <param name="collection">The timer collection.</param>
        public void AddRange(IEnumerable<ITimer> collection)
        {
            if (m_timers != null && collection != null)
            {
                m_timers.AddRangeUnique(collection);
            }
        }

        /// <summary>
        /// Removes the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] timer object.</param>
        /// <returns>
        /// <c>true</c> if timer is successfully removed; otherwise, <c>false</c>. This method also
        /// returns false if timer was not found.
        /// </returns>
        public bool Remove(ITimer timer, bool autoStop = true)
        {
            if (m_timers != null && timer != null)
            {
                bool success = m_timers.Remove(timer);

                if (success)
                {
                    if (autoStop)
                    {
                        timer.Stop();
                    }
                }

                return success;
            }

            return false;
        }

        /// <summary>
        /// Removes a range of elements from the timer list.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        public void RemoveRange(ICollection<ITimer> collection, bool autoStop = true)
        {
            if (m_timers != null)
            {
                m_timers.RemoveRange(collection);

                if (autoStop)
                {
                    foreach (ITimer timer in collection)
                    {
                        timer.Stop();
                    }
                }
            }
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] all timer objects.</param>
        public void RemoveAll(bool autoStop = true)
        {
            if (m_timers != null)
            {
                if (autoStop)
                {
                    m_timers.ForEach(timer =>
                    {
                        if (timer != null)
                        {
                            timer.Stop();
                        }
                    });
                }

                m_timers.Clear();
            }
        }

        /// <summary>
        /// Sets all timers enabled.
        /// </summary>
        /// <param name="enabled">The enabled value.</param>
        public void SetAllEnabled(bool enabled = true)
        {
            if (m_timers != null)
            {
                m_timers.ForEach(timer =>
                {
                    if (timer != null)
                    {
                        timer.enabled = enabled;
                    }
                });
            }
        }

        /// <summary>
        /// Pauses all timers.
        /// </summary>
        public void PauseAll()
        {
            if (m_timers != null)
            {
                m_timers.ForEach(timer =>
                {
                    if (timer != null)
                    {
                        timer.Pause();
                    }
                });
            }
        }

        /// <summary>
        /// Resets all timers.
        /// </summary>
        public void ResetAll()
        {
            if (m_timers != null)
            {
                m_timers.ForEach(timer =>
                {
                    if (timer != null)
                    {
                        timer.Reset();
                    }
                });
            }
        }

        /// <summary>
        /// Resumes all timers.
        /// </summary>
        public void ResumeAll()
        {
            if (m_timers != null)
            {
                m_timers.ForEach(timer =>
                {
                    if (timer != null)
                    {
                        timer.Resume();
                    }
                });
            }
        }

        /// <summary>
        /// Starts all timers.
        /// </summary>
        public void StartAll()
        {
            if (m_timers != null)
            {
                m_timers.ForEach(timer =>
                {
                    if (timer != null)
                    {
                        timer.Start();
                    }
                });
            }
        }

        /// <summary>
        /// Stops all timers.
        /// </summary>
        public void StopAll()
        {
            if (m_timers != null)
            {
                m_timers.ForEach(timer =>
                {
                    if (timer != null)
                    {
                        timer.Stop();
                    }
                });
            }
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            StopAll();

            if (m_timers != null)
            {
                m_timers.Clear();
                m_timers = null;
            }
        }
    }
}