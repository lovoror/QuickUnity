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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Timers
{
    /// <summary>
    /// Class to globally manage timers.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.SingletonBehaviour{QuickUnity.Timers.TimerManager}"/>
    public class TimerManager : SingletonBehaviour<TimerManager>
    {
        /// <summary>
        /// The list of timers.
        /// </summary>
        private List<ITimer> m_timers;

        /// <summary>
        /// Called when script receive message Awake.
        /// </summary>
        protected override void OnAwake()
        {
            base.OnAwake();

            // Initialize timers list.
            m_timers = new List<ITimer>();
        }

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        private void Update()
        {
            if (m_timers != null)
            {
                for (int i = 0, length = m_timers.Count; i < length; ++i)
                {
                    float deltaTime = Time.deltaTime;
                    ITimer timer = m_timers[i];

                    try
                    {
                        if (timer.ignoreTimeScale)
                        {
                            deltaTime = Time.unscaledDeltaTime;
                        }

                        timer.Tick(deltaTime);
                    }
                    catch (Exception error)
                    {
                        Debug.LogWarning(error.StackTrace);
                    }
                }
            }
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            RemoveAllTimers();
            m_timers = null;
        }

        #region Public Functions

        /// <summary>
        /// Determines whether contains the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <returns><c>true</c> if contains the timer object; otherwise, <c>false</c>.</returns>
        public bool ContainsTimer(ITimer timer)
        {
            if (m_timers != null)
            {
                return m_timers.Contains(timer);
            }

            return false;
        }

        /// <summary>
        /// Adds the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start] timer object.</param>
        public void AddTimer(ITimer timer, bool autoStart = true)
        {
        }

        /// <summary>
        /// Removes the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] timer object.</param>
        public void RemoveTimer(ITimer timer, bool autoStop = true)
        {
            if (m_timers != null && ContainsTimer(timer))
            {
                if (autoStop)
                {
                    timer.Stop();
                }

                m_timers.Remove(timer);
            }
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] all timer objects.</param>
        public void RemoveAllTimers(bool autoStop = true)
        {
            if (autoStop)
            {
                for (int i = 0, length = m_timers.Count; i < length; ++i)
                {
                    ITimer timer = m_timers[i];
                    timer.Stop();
                }
            }

            if (m_timers != null)
            {
                m_timers.Clear();
            }
        }

        #endregion Public Functions
    }
}