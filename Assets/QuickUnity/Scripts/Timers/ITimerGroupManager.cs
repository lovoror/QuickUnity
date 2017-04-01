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

namespace QuickUnity.Timers
{
    /// <summary>
    /// The interface definition for the TimerGroup object.
    /// </summary>
    public interface ITimerGroupManager
    {
        /// <summary>
        /// Gets the timer group.
        /// </summary>
        /// <param name="groupName">Name of the group.</param>
        /// <returns>The TimerGroup object.</returns>
        ITimerGroup GetTimerGroup(string groupName);

        /// <summary>
        /// Adds the timer group.
        /// </summary>
        /// <param name="timerGroup">The timer group.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        void AddTimerGroup(ITimerGroup timerGroup);

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
        bool RemoveTimerGroup(string groupName, bool removeTimersInGroup = true, bool autoStop = true);

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
        bool RemoveTimerGroup(ITimerGroup timerGroup, bool removeTimersInGroup = true, bool autoStop = true);

        /// <summary>
        /// Removes all timer groups.
        /// </summary>
        /// <param name="removeTimersInGroup">if set to <c>true</c> [remove timers in group].</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        void RemoveAllTimerGroups(bool removeTimersInGroup = true, bool autoStop = true);
    }
}