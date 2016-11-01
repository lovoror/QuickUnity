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

namespace QuickUnity.Timers
{
    /// <summary>
    /// The interface definition for the TimerGroup object.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.IEventDispatcher"/>
    public interface ITimerGroup : IEventDispatcher
    {
        /// <summary>
        /// Gets or sets the name of the group.
        /// </summary>
        /// <value>The name of the group.</value>
        string groupName { get; }

        /// <summary>
        /// Determines whether this timer group contains the specified timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <returns>
        /// <c>true</c> if this timer group contains the specified timer; otherwise, <c>false</c>.
        /// </returns>
        bool ContainsTimer(ITimer timer);

        /// <summary>
        /// Adds the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        void AddTimer(ITimer timer, bool autoStart = false);

        /// <summary>
        /// Removes the timer.
        /// </summary>
        /// <param name="timer">The timer.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        /// <returns>
        /// <c>true</c> if timer is successfully removed; otherwise, <c>false</c>. This method also
        /// returns false if timer was not found.
        /// </returns>
        bool RemoveTimer(ITimer timer, bool autoStop = false);

        /// <summary>
        /// Removes all timers.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        void RemoveAllTimers(bool autoStop = false);

        /// <summary>
        /// This timer group start timing.
        /// </summary>
        void Start();

        /// <summary>
        /// This timer group pause timing.
        /// </summary>
        void Pause();

        /// <summary>
        /// This timer group resume timing.
        /// </summary>
        void Resume();

        /// <summary>
        /// This timer group resets timing.
        /// </summary>
        void Reset();

        /// <summary>
        /// This timer group stop timing.
        /// </summary>
        void Stop();

        /// <summary>
        /// Destroys this timer group instance.
        /// </summary>
        void Destroy();
    }
}