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

using System;
using System.Collections.Generic;

namespace QuickUnity.Timers
{
    /// <summary>
    /// The interface definition for the timer list object.
    /// </summary>
    public interface ITimerList
    {
        /// <summary>
        /// Gets all the timers.
        /// </summary>
        /// <value>All the timers.</value>
        List<ITimer> timers { get; }

        /// <summary>
        /// Performs the specified action on each element of the timer list.
        /// </summary>
        /// <param name="action">The action.</param>
        void ForEach(Action<ITimer> action);

        /// <summary>
        /// Determines whether contains the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <returns><c>true</c> if contains the timer object; otherwise, <c>false</c>.</returns>
        bool Contains(ITimer timer);

        /// <summary>
        /// Adds the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        void Add(ITimer timer, bool autoStart = false);

        /// <summary>
        /// Adds the timers.
        /// </summary>
        /// <param name="collection">The timer collection.</param>
        void AddRange(IEnumerable<ITimer> collection);

        /// <summary>
        /// Removes the timer object.
        /// </summary>
        /// <param name="timer">The timer object.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] timer object.</param>
        /// <returns>
        /// <c>true</c> if timer is successfully removed; otherwise, <c>false</c>. This method also
        /// returns false if timer was not found.
        /// </returns>
        bool Remove(ITimer timer, bool autoStop = true);

        /// <summary>
        /// Removes a range of elements from the timer list.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        void RemoveRange(ICollection<ITimer> collection, bool autoStop = true);

        /// <summary>
        /// Removes all timers.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop] all timer objects.</param>
        void RemoveAll(bool autoStop = true);

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        void Destroy();

        /// <summary>
        /// Sets all timers enabled.
        /// </summary>
        /// <param name="enabled">The enabled value.</param>
        void SetAllEnabled(bool enabled = true);

        /// <summary>
        /// Starts all timers.
        /// </summary>
        void StartAll();

        /// <summary>
        /// Pauses all timers.
        /// </summary>
        void PauseAll();

        /// <summary>
        /// Resumes all timers.
        /// </summary>
        void ResumeAll();

        /// <summary>
        /// Stops all timers.
        /// </summary>
        void StopAll();

        /// <summary>
        /// Resets all timers.
        /// </summary>
        void ResetAll();
    }
}