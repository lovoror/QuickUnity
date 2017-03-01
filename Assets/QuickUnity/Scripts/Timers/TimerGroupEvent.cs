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

namespace QuickUnity.Timers
{
    /// <summary>
    /// Class TimerGroupEvent.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.Event"/>
    public class TimerGroupEvent : Events.Event
    {
        /// <summary>
        /// Dispatched whenever a TimerGroup start to run.
        /// </summary>
        public const string TimerGroupStart = "TimerGroupStart";

        /// <summary>
        /// Dispatched whenever a TimerGroup pause.
        /// </summary>
        public const string TimerGroupPause = "TimerGroupPause";

        /// <summary>
        /// Dispatched whenever a TimerGroup resume to run.
        /// </summary>
        public const string TimerGroupResume = "TimerGroupResume";

        /// <summary>
        /// Dispatched whenever a TimerGroup stop.
        /// </summary>
        public const string TimerGroupStop = "TimerGroupStop";

        /// <summary>
        /// Dispatched whenever a TimerGroup reset.
        /// </summary>
        public const string TimerGroupReset = "TimerGroupReset";

        /// <summary>
        /// Gets the timer group.
        /// </summary>
        /// <value>The timer group.</value>
        public ITimerGroup timerGroup
        {
            get { return (ITimerGroup)m_context; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerGroupEvent"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="group">The timer group.</param>
        public TimerGroupEvent(string eventType, ITimerGroup group)
            : base(eventType, group)
        {
        }
    }
}