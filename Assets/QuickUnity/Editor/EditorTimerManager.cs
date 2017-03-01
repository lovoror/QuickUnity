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

using QuickUnity.Core.Miscs;
using QuickUnity.Patterns;
using QuickUnity.Timers;
using QuickUnityEditor.Attributes;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuickUnityEditor
{
    /// <summary>
    /// TimerManager for using in Editor.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.Singleton{QuickUnityEditor.EditorTimerManager}"/>
    [InitializeOnEditorStartup(int.MaxValue)]
    internal sealed class EditorTimerManager : Singleton<EditorTimerManager>, ITimerManager
    {
        /// <summary>
        /// The last time.
        /// </summary>
        public double m_lastTime = 0.0d;

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
        /// The timer list.
        /// </summary>
        private ITimerList m_timerList;

        /// <summary>
        /// Initializes static members of the <see cref="EditorTimerManager"/> class.
        /// </summary>
        static EditorTimerManager()
        {
            instance.Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            m_lastTime = EditorApplication.timeSinceStartup;
            m_timerList = new TimerList();
            EditorApplication.update += OnEditorUpdate;
        }

        #region Interface ITimerManager Implement Functions

        /// <summary>
        /// Fors the each.
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
        public void RemoveAllTimers(bool autoStop = true)
        {
            if (m_timerList != null)
            {
                m_timerList.RemoveAllTimers(autoStop);
            }
        }

        /// <summary>
        /// Destroys this instance.
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
        /// <param name="enabled">The enabled value.</param>
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
            }
        }

        #endregion Interface ITimerManager Implement Functions

        #region Private Functions

        /// <summary>
        /// Called when [editor update].
        /// </summary>
        private void OnEditorUpdate()
        {
            float deltaTime = (float)(EditorApplication.timeSinceStartup - m_lastTime);
            m_lastTime = EditorApplication.timeSinceStartup;

            if (m_timerList != null)
            {
                m_timerList.ForEach(timer =>
                {
                    try
                    {
                        if (!timer.ignoreTimeScale)
                        {
                            deltaTime = deltaTime * Time.timeScale;
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

        #endregion Private Functions
    }
}