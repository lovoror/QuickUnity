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
using System.Collections.Generic;

namespace QuickUnity.Timers
{
    /// <summary>
    /// Class TimerGroup.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.EventDispatcher"/>
    public class TimerGroup : EventDispatcher, ITimerGroup
    {
        /// <summary>
        /// The timer list object.
        /// </summary>
        protected ITimerList m_timerList;

        /// <summary>
        /// Gets all the timers.
        /// </summary>
        /// <value>All the timers.</value>
        public List<ITimer> timers
        {
            get
            {
                if (m_timerList != null)
                {
                    return m_timerList.timers;
                }

                return null;
            }
        }

        /// <summary>
        /// The name of the group.
        /// </summary>
        protected string m_groupName;

        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        public string groupName
        {
            get
            {
                return m_groupName;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerGroup"/> class.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        /// <param name="timers">The timers.</param>
        public TimerGroup(string groupName, bool autoStart = false, params ITimer[] timers)
            : base()
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return;
            }

            m_groupName = groupName;
            m_timerList = new TimerList(timers);

            if (autoStart)
            {
                StartAllTimers();
            }
        }

        /// <summary>
        /// Performs the specified action on each element of the timer list.
        /// </summary>
        /// <param name="action">The action.</param>
        public void ForEach(Action<ITimer> action)
        {
            if (m_timerList != null)
            {
                m_timerList.ForEach(action);
            }
        }

        /// <summary>
        /// Determines whether this timer group contains the specified timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <returns>
        /// <c>true</c> if this timer group contains the specified timer; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsTimer(ITimer timer)
        {
            if (m_timerList != null)
            {
                return m_timerList.ContainsTimer(timer);
            }

            return false;
        }

        /// <summary>
        /// Adds the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        public void AddTimer(ITimer timer, bool autoStart = false)
        {
            if (m_timerList != null)
            {
                m_timerList.AddTimer(timer);
            }
        }

        /// <summary>
        /// Adds the timers.
        /// </summary>
        /// <param name="collection">The timer collection.</param>
        public void AddTimers(IEnumerable<ITimer> collection)
        {
            if (m_timerList != null)
            {
                m_timerList.AddTimers(collection);
            }
        }

        /// <summary>
        /// Removes the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        /// <returns>
        /// <c>true</c> if timer is successfully removed; otherwise, <c>false</c>. This method also
        /// returns false if timer was not found.
        /// </returns>
        public bool RemoveTimer(ITimer timer, bool autoStop = false)
        {
            if (m_timerList != null)
            {
                return m_timerList.RemoveTimer(timer, autoStop);
            }

            return false;
        }

        /// <summary>
        /// Removes a range of elements from the timer list.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        public void RemoveTimers(ICollection<ITimer> collection, bool autoStop = true)
        {
            if (m_timerList != null)
            {
                m_timerList.RemoveTimers(collection, autoStop);
            }
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        public void RemoveAllTimers(bool autoStop = false)
        {
            if (m_timerList != null)
            {
                m_timerList.RemoveAllTimers(autoStop);
            }
        }

        /// <summary>
        /// Destroys this timer group instance.
        /// </summary>
        public void Destroy()
        {
            if (m_timerList != null)
            {
                m_timerList.Destroy();
            }
        }

        /// <summary>
        /// Sets all timers enabled.
        /// </summary>
        /// <param name="enabled">Timer enabled or not.</param>
        public void SetAllTimersEnabled(bool enabled = true)
        {
            if (m_timerList != null)
            {
                m_timerList.SetAllTimersEnabled(enabled);
            }
        }

        /// <summary>
        /// This timer group start timing.
        /// </summary>
        public void StartAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.StartAllTimers();
                DispatchEvent(new TimerGroupEvent(TimerGroupEvent.TimerGroupStart, this));
            }
        }

        /// <summary>
        /// This timer group pause timing.
        /// </summary>
        public void PauseAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.PauseAllTimers();
                DispatchEvent(new TimerGroupEvent(TimerGroupEvent.TimerGroupPause, this));
            }
        }

        /// <summary>
        /// This timer group resume timing.
        /// </summary>
        public void ResumeAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.ResumeAllTimers();
                DispatchEvent(new TimerGroupEvent(TimerGroupEvent.TimerGroupResume, this));
            }
        }

        /// <summary>
        /// This timer group stop timing.
        /// </summary>
        public void StopAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.StopAllTimers();
                DispatchEvent(new TimerGroupEvent(TimerGroupEvent.TimerGroupStop, this));
            }
        }

        /// <summary>
        /// This timer group resets timing.
        /// </summary>
        public void ResetAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.ResetAllTimers();
                DispatchEvent(new TimerGroupEvent(TimerGroupEvent.TimerGroupReset, this));
            }
        }
    }
}