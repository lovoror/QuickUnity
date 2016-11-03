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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Timers
{
    /// <summary>
    /// Class to globally manage Timer objects.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.SingletonBehaviour{QuickUnity.Timers.TimerManager}"/>
    public class TimerManager : SingletonBehaviour<TimerManager>
    {
        /// <summary>
        /// The list of all timers.
        /// </summary>
        private List<ITimer> m_timers;

        /// <summary>
        /// The list of all timer groups.
        /// </summary>
        private List<ITimerGroup> m_timerGroups;

        /// <summary>
        /// Called when script receive message Awake.
        /// </summary>
        protected override void OnAwake()
        {
            base.OnAwake();

            // Initialize all timers list.
            m_timers = new List<ITimer>();

            // Initialize all timer groups list.
            m_timerGroups = new List<ITimerGroup>();
        }

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        private void Update()
        {
            if (m_timers != null)
            {
                m_timers.ForEach(timer =>
                {
                    float deltaTime = Time.deltaTime;

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
                });
            }
        }

        /// <summary>
        /// This function is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            SetAllTimersEnabled(true);
        }

        /// <summary>
        /// This function is called when the behaviour becomes disabled () or inactive.
        /// </summary>
        private void OnDisable()
        {
            SetAllTimersEnabled(false);
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

        /// <summary>
        /// This function is called when the application pauses.
        /// </summary>
        /// <param name="pauseStatus">if set to <c>true</c> [pause status].</param>
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                // Pause all timers.
                PauseAllTimers();
            }
            else
            {
                // Resume all timers.
                ResumeAllTimers();
            }
        }

        #region Public Functions

        #region Timer Functions

        /// <summary>
        /// Determines whether contains the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <returns><c>true</c> if contains the timer object; otherwise, <c>false</c>.</returns>
        public bool ContainsTimer(ITimer timer)
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
        /// <param name="autoStart">if set to <c>true</c> [automatic start] timer object.</param>
        public void AddTimer(ITimer timer, bool autoStart = true)
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
        /// Sets all timers enabled.
        /// </summary>
        /// <param name="enabled">The enabled value.</param>
        public void SetAllTimersEnabled(bool enabled = true)
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
        /// Starts all timers.
        /// </summary>
        public void StartAllTimers()
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
        /// Pauses all timers.
        /// </summary>
        public void PauseAllTimers()
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
        /// Resumes all timers.
        /// </summary>
        public void ResumeAllTimers()
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
        /// Stops all timers.
        /// </summary>
        public void StopAllTimers()
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
        /// Resets all timers.
        /// </summary>
        public void ResetAllTimers()
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
        /// Removes the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] timer object.</param>
        /// <returns>
        /// <c>true</c> if timer is successfully removed; otherwise, <c>false</c>. This method also
        /// returns false if timer was not found.
        /// </returns>
        public bool RemoveTimer(ITimer timer, bool autoStop = true)
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

                    // Remove timer in timer groups.
                    RemoveTimerInGroups(timer);
                }
            }

            return false;
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] all timer objects.</param>
        public void RemoveAllTimers(bool autoStop = true)
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
                m_timerGroups.Clear();
            }
        }

        #endregion Timer Functions

        #region TimerGroup Functions

        /// <summary>
        /// Gets the timer group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>The TimerGroup object.</returns>
        public ITimerGroup GetTimerGroup(string groupName)
        {
            if (m_timerGroups != null && !string.IsNullOrEmpty(groupName))
            {
                return m_timerGroups.Find(timerGroup =>
                {
                    if (timerGroup.groupName == groupName)
                    {
                        return true;
                    }

                    return false;
                });
            }

            return null;
        }

        /// <summary>
        /// Adds the timer group.
        /// </summary>
        /// <param name="timerGroup">The timer group.</param>
        public void AddTimerGroup(ITimerGroup timerGroup)
        {
            if (m_timerGroups != null && timerGroup != null)
            {
                m_timerGroups.AddUnique(timerGroup);
            }
        }

        /// <summary>
        /// Removes the timer group.
        /// </summary>
        /// <param name="timerGroup">The timer group.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        /// <returns>
        /// <c>true</c> if timer group is successfully removed; otherwise, <c>false</c>. This method
        /// also returns false if timer group was not found.
        /// </returns>
        public bool RemoveTimerGroup(ITimerGroup timerGroup, bool autoStop = true)
        {
            if (m_timerGroups != null && timerGroup != null)
            {
                bool success = m_timerGroups.Remove(timerGroup);

                if (success)
                {
                    if (autoStop)
                    {
                        timerGroup.Stop();
                    }
                }

                return success;
            }

            return false;
        }

        /// <summary>
        /// Removes all timer groups.
        /// </summary>
        public void RemoveAllTimerGroups()
        {
            if (m_timerGroups != null)
            {
                m_timerGroups.ForEach(timerGroup =>
                {
                    if (timerGroup != null)
                    {
                        timerGroup.Stop();
                    }
                });

                m_timerGroups.Clear();
            }
        }

        /// <summary>
        /// Removes the timer in timer groups.
        /// </summary>
        /// <param name="timer">The timer.</param>
        public void RemoveTimerInGroups(ITimer timer)
        {
            if (m_timerGroups != null && timer != null)
            {
                m_timerGroups.ForEach(timerGroup =>
                {
                    if (timerGroup != null && timerGroup.ContainsTimer(timer))
                    {
                        timerGroup.RemoveTimer(timer);
                    }
                });
            }
        }

        #endregion TimerGroup Functions

        #endregion Public Functions
    }
}