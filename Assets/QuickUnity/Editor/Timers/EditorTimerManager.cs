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

using QuickUnity.Patterns;
using QuickUnity.Timers;
using QuickUnityEditor.Attributes;
using System;
using UnityEditor;
using UnityEngine;

namespace QuickUnityEditor
{
    /// <summary>
    /// The EditorTimerManager is a convenience class for managing editor timer systems. This class
    /// cannot be inherited.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.Singleton{QuickUnityEditor.EditorTimerManager}"/>
    /// <seealso cref="QuickUnity.Timers.ITimerCollection"/>
    [InitializeOnEditorStartup(int.MaxValue)]
    internal sealed class EditorTimerManager : Singleton<EditorTimerManager>, ITimerCollection
    {
        /// <summary>
        /// The last time.
        /// </summary>
        private double m_lastTime = 0.0d;

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
        /// Gets the number of <see cref="T:QuickUnity.Timers.ITimer"/> elements contained in the
        /// <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        /// <value>
        /// The number of <see cref="T:QuickUnity.Timers.ITimer"/> elements contained in the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </value>
        public int count
        {
            get
            {
                if (m_timerList != null)
                {
                    return m_timerList.count;
                }

                return 0;
            }
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

        #region ITimerCollection Interface

        /// <summary>
        /// Adds an <see cref="T:QuickUnity.Timers.ITimer"/> item to the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        /// <param name="item">
        /// The <see cref="T:QuickUnity.Timers.ITimer"/> object to add to the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </param>
        public void Add(ITimer item)
        {
            if (m_timerList != null)
            {
                m_timerList.Add(item);
            }
        }

        /// <summary>
        /// Removes all <see cref="T:QuickUnity.Timers.ITimer"/> items from the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        public void Clear()
        {
            if (m_timerList != null)
            {
                m_timerList.Clear();
            }
        }

        /// <summary>
        /// Determines whether the <see cref="T:QuickUnity.Timers.ITimerCollection"/>. contains a
        /// specific <see cref="T:QuickUnity.Timers.ITimer"/> object.
        /// </summary>
        /// <param name="item">
        /// The <see cref="T:QuickUnity.Timers.ITimer"/> object to locate in the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if <see cref="T:QuickUnity.Timers.ITimer"/> item is found in the <see
        /// cref="T:QuickUnity.Timers.ITimerCollection"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(ITimer item)
        {
            if (m_timerList != null)
            {
                return m_timerList.Contains(item);
            }

            return false;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.</param>
        /// <returns>
        /// <c>true</c> if item was successfully removed from the <see
        /// cref="T:QuickUnity.Timers.ITimerCollection"/>; otherwise, <c>false</c>. This method also
        /// returns <c>false</c> if item is not found in the original <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </returns>
        public bool Remove(ITimer item)
        {
            if (m_timerList != null)
            {
                return m_timerList.Remove(item);
            }

            return false;
        }

        /// <summary>
        /// Pauses all timers in the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        public void PauseAll()
        {
        }

        /// <summary>
        /// Resets all timers in the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        public void ResetAll()
        {
            if (m_timerList != null)
            {
                m_timerList.ResetAll();
            }
        }

        /// <summary>
        /// Resumes all timers in <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        public void ResumeAll()
        {
            if (m_timerList != null)
            {
                m_timerList.ResumeAll();
            }
        }

        /// <summary>
        /// Sets all timers in the <see cref="T:QuickUnity.Timers.ITimerCollection"/> to be enabled
        /// or not.
        /// </summary>
        /// <param name="value">
        /// Set to <c>true</c> to enable all timers in the <see
        /// cref="T:QuickUnity.Timers.ITimerCollection"/> control to trigger their timer event;
        /// otherwise, set to <c>false</c>.
        /// </param>
        public void SetAllEnabled(bool value = true)
        {
            if (m_timerList != null)
            {
                m_timerList.SetAllEnabled(value);
            }
        }

        /// <summary>
        /// Starts all timers in the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        public void StartAll()
        {
            if (m_timerList != null)
            {
                m_timerList.StartAll();
            }
        }

        /// <summary>
        /// Stops all timers in the <see cref="T:QuickUnity.Timers.ITimerCollection"/>.
        /// </summary>
        public void StopAll()
        {
            if (m_timerList != null)
            {
                m_timerList.StopAll();
            }
        }

        #endregion ITimerCollection Interface

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
                m_timerList.ForEach((timer) =>
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
                        Debug.LogException(exception);
                    }
                });
            }
        }

        #endregion Private Functions
    }
}