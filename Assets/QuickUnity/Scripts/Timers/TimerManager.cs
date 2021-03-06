﻿/*
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
    /// Hold and manage all timer objects.
    /// </summary>
    public class TimerManager : MonoBehaviourSingleton<TimerManager>
    {
        /// <summary>
        /// The timer dictionary.
        /// </summary>
        private Dictionary<string, ITimer> m_timers;

        /// <summary>
        /// Whether the application paused.
        /// </summary>
        private bool m_applicationPaused = false;

        /// <summary>
        /// Whether paused manually.
        /// </summary>
        private bool m_paused = false;

        #region Messages

        /// <summary>
        /// Awake this script.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            // Initialize timer list.
            m_timers = new Dictionary<string, ITimer>();
        }

        /// <summary>
        /// Update in fixed time.
        /// </summary>
        private void FixedUpdate()
        {
            if (enabled && !m_applicationPaused && !m_paused)
            {
                float deltaTime = Time.fixedDeltaTime;

                if (m_timers != null && m_timers.Count > 0)
                {
                    try
                    {
                        foreach (KeyValuePair<string, ITimer> kvp in m_timers)
                        {
                            ITimer timer = kvp.Value;
                            timer.Tick(deltaTime);
                        }
                    }
                    catch (InvalidOperationException exception)
                    {
                        Debug.Log(exception.StackTrace);
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

            // Remove all timers.
            RemoveAllTimers();
            m_timers = null;
        }

        /// <summary>
        /// Sent to all game objects when the player pauses.
        /// </summary>
        /// <param name="pauseStatus">if set to <c>true</c> [pause status].</param>
        private void OnApplicationPause(bool pauseStatus)
        {
            m_applicationPaused = pauseStatus;
        }

        #endregion Messages

        #region API

        /// <summary>
        /// Pause all timers.
        /// </summary>
        public void Pause()
        {
            m_paused = true;
        }

        /// <summary>
        /// Resume all timers.
        /// </summary>
        public void Resume()
        {
            m_paused = false;
        }

        /// <summary>
        /// Gets the timer.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Timer.</returns>
        public ITimer GetTimer(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            if (m_timers.ContainsKey(name))
                return m_timers[name];

            return null;
        }

        /// <summary>
        /// If owns the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <returns>System.Boolean.</returns>
        public bool OwnTimer(ITimer timer)
        {
            return m_timers.ContainsValue(timer);
        }

        /// <summary>
        /// Adds the timer object.
        /// </summary>
        /// <param name="name">The name of timer.</param>
        /// <param name="timer">The timer.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start timer].</param>
        public void AddTimer(string name, ITimer timer, bool autoStart = true)
        {
            if (GetTimer(name) != null)
            {
                Debug.LogWarning("Already got a timer with the same name, please change the name or remove the timer of TimerManager already own!");
                return;
            }

            if (!string.IsNullOrEmpty(name))
            {
                m_timers.Add(name, timer);

                if (autoStart)
                    timer.Start();
            }
        }

        /// <summary>
        /// Removes the timer by timer name.
        /// </summary>
        /// <param name="name">The name of timer.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic stop timer].</param>
        public void RemoveTimer(string name, bool autoStop = true)
        {
            ITimer timer = GetTimer(name);

            if (timer != null)
            {
                if (autoStop)
                    timer.Stop();

                RemoveTimer(timer);
            }
        }

        /// <summary>
        /// Removes the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        public void RemoveTimer(ITimer timer)
        {
            if (OwnTimer(timer) && m_timers != null && m_timers.Count > 0)
            {
                foreach (KeyValuePair<string, ITimer> kvp in m_timers)
                {
                    if (kvp.Value.Equals(timer))
                    {
                        m_timers.Remove(kvp.Key);
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Removes all timers.
        /// </summary>
        /// <param name="autoStart">if set to <c>true</c> [automatic stop all timers].</param>
        public void RemoveAllTimers(bool autoStop = true)
        {
            if (autoStop)
                StopAllTimers();

            if (m_timers != null)
                m_timers.Clear();
        }

        /// <summary>
        /// Starts all timers.
        /// </summary>
        public void StartAllTimers()
        {
            if (m_timers != null && m_timers.Count > 0)
            {
                foreach (KeyValuePair<string, ITimer> kvp in m_timers)
                {
                    ITimer timer = kvp.Value;
                    timer.Start();
                }
            }
        }

        /// <summary>
        /// Resets all timers.
        /// </summary>
        public void ResetAllTimers()
        {
            if (m_timers != null && m_timers.Count > 0)
            {
                foreach (KeyValuePair<string, ITimer> kvp in m_timers)
                {
                    ITimer timer = kvp.Value;
                    timer.Reset();
                }
            }
        }

        /// <summary>
        /// Stops all timers.
        /// </summary>
        public void StopAllTimers()
        {
            if (m_timers != null && m_timers.Count > 0)
            {
                foreach (KeyValuePair<string, ITimer> kvp in m_timers)
                {
                    ITimer timer = kvp.Value;
                    timer.Stop();
                }
            }
        }

        #endregion API
    }
}