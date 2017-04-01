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

using QuickUnity.Core.Miscs;
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
    /// <seealso cref="QuickUnity.Timers.ITimerGroupManager"/>
    /// <seealso cref="QuickUnity.Timers.ITimerManager"/>
    /// <seealso cref="QuickUnity.Patterns.SingletonBehaviour{QuickUnity.Timers.TimerManager}"/>
    public class TimerManager : SingletonBehaviour<TimerManager>, ITimerManager, ITimerGroupManager
    {
        /// <summary>
        /// The timer list.
        /// </summary>
        private ITimerList m_timerList;

        /// <summary>
        /// Gets the timers.
        /// </summary>
        /// <value>The timers.</value>
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
        /// The dictionary of timer groups.
        /// </summary>
        private Dictionary<string, ITimerGroup> m_timerGroups;

        #region Messages

        /// <summary>
        /// Called when script receive message Awake.
        /// </summary>
        protected override void OnAwake()
        {
            base.OnAwake();

            // Initialize all timers list.
            m_timerList = new TimerList();

            // Initialize the dictionary of timer groups.
            m_timerGroups = new Dictionary<string, ITimerGroup>();
        }

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        private void Update()
        {
            if (m_timerList != null)
            {
                m_timerList.ForEach(timer =>
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
                    catch (Exception exception)
                    {
                        DebugLogger.LogException(exception, this);
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
            Destroy();
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

        #endregion Messages

        #region Public Functions

        #region Interface ITimerManager Implement Functions

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
        /// Determines whether contains the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <returns><c>true</c> if contains the timer object; otherwise, <c>false</c>.</returns>
        public bool ContainsTimer(ITimer timer)
        {
            if (m_timerList != null)
            {
                return m_timerList.ContainsTimer(timer);
            }

            return false;
        }

        /// <summary>
        /// Adds the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        public void AddTimer(ITimer timer, bool autoStart = false)
        {
            if (m_timerList != null)
            {
                m_timerList.AddTimer(timer, autoStart);
            }
        }

        /// <summary>
        /// Adds timers.
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
            if (m_timerList != null)
            {
                bool success = m_timerList.RemoveTimer(timer, autoStop);

                if (success)
                {
                    RemoveTimerInGroups(timer);
                }

                return success;
            }

            return false;
        }

        /// <summary>
        /// Removes the range.
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
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] all timer objects.</param>
        public void RemoveAllTimers(bool autoStop = true)
        {
            if (m_timerList != null)
            {
                m_timerList.RemoveAllTimers(autoStop);
            }

            RemoveAllTimerGroups(false);
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
        /// Starts all timers.
        /// </summary>
        public void StartAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.StartAllTimers();
            }
        }

        /// <summary>
        /// Pauses all timers.
        /// </summary>
        public void PauseAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.PauseAllTimers();
            }
        }

        /// <summary>
        /// Resumes all timers.
        /// </summary>
        public void ResumeAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.ResumeAllTimers();
            }
        }

        /// <summary>
        /// Stops all timers.
        /// </summary>
        public void StopAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.StopAllTimers();
            }
        }

        /// <summary>
        /// Resets all timers.
        /// </summary>
        public void ResetAllTimers()
        {
            if (m_timerList != null)
            {
                m_timerList.ResetAllTimers();
            }
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            RemoveAllTimers();
        }

        #endregion Interface ITimerManager Implement Functions

        #region Interface ITimerGroup Implement Functions

        /// <summary>
        /// Gets the timer group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>The TimerGroup object.</returns>
        public ITimerGroup GetTimerGroup(string groupName)
        {
            if (m_timerGroups != null && m_timerGroups.ContainsKey(groupName))
            {
                return m_timerGroups[groupName];
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
                m_timerGroups.AddUnique(timerGroup.groupName, timerGroup);

                if (timerGroup.timers != null)
                {
                    AddTimers(timerGroup.timers);
                }
            }
        }

        /// <summary>
        /// Removes the timer group.
        /// </summary>
        /// <param name="groupName">Name of the timer group.</param>
        /// <param name="removeTimersInGroup">if set to <c>true</c> [remove timers in group].</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        /// <returns>
        /// <c>true</c> if timer group is successfully removed; otherwise, <c>false</c>. This method
        /// also returns false if timer group was not found.
        /// </returns>
        public bool RemoveTimerGroup(string groupName, bool removeTimersInGroup = true, bool autoStop = true)
        {
            if (m_timerGroups != null && !string.IsNullOrEmpty(groupName))
            {
                ITimerGroup timerGroup = GetTimerGroup(groupName);
                bool success = m_timerGroups.Remove(groupName);

                if (success && timerGroup != null)
                {
                    // Remove timers in timer group.
                    if (timerGroup.timers != null)
                    {
                        RemoveTimers(timerGroup.timers);
                    }

                    // Auto Stop.
                    if (autoStop)
                    {
                        timerGroup.StopAllTimers();
                    }
                }

                return success;
            }

            return false;
        }

        /// <summary>
        /// Removes the timer group.
        /// </summary>
        /// <param name="timerGroup">The timer group.</param>
        /// <param name="removeTimersInGroup">if set to <c>true</c> [remove timers in group].</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        /// <returns>
        /// <c>true</c> if timer group is successfully removed; otherwise, <c>false</c>. This method
        /// also returns false if timer group was not found.
        /// </returns>
        public bool RemoveTimerGroup(ITimerGroup timerGroup, bool removeTimersInGroup = true, bool autoStop = true)
        {
            if (timerGroup != null)
            {
                return RemoveTimerGroup(timerGroup.groupName, removeTimersInGroup, autoStop);
            }

            return false;
        }

        /// <summary>
        /// Removes all timer groups.
        /// </summary>
        /// <param name="removeTimersInGroup">if set to <c>true</c> [remove timers in group].</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        public void RemoveAllTimerGroups(bool removeTimersInGroup = true, bool autoStop = true)
        {
            if (m_timerGroups != null)
            {
                foreach (KeyValuePair<string, ITimerGroup> kvp in m_timerGroups)
                {
                    ITimerGroup timerGroup = kvp.Value;

                    if (timerGroup != null)
                    {
                        if (removeTimersInGroup && timerGroup.timers != null)
                        {
                            RemoveTimers(timerGroup.timers);
                        }

                        if (autoStop)
                        {
                            timerGroup.StopAllTimers();
                        }
                    }
                }

                m_timerGroups.Clear();
            }
        }

        #endregion Interface ITimerGroup Implement Functions

        #endregion Public Functions

        #region Private Functions

        /// <summary>
        /// Removes the timer in timer groups.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        private void RemoveTimerInGroups(ITimer timer)
        {
            if (timer != null)
            {
                foreach (KeyValuePair<string, ITimerGroup> kvp in m_timerGroups)
                {
                    ITimerGroup timerGroup = kvp.Value;

                    if (timerGroup.ContainsTimer(timer))
                    {
                        timerGroup.RemoveTimer(timer);
                    }
                }
            }
        }

        #endregion Private Functions
    }
}