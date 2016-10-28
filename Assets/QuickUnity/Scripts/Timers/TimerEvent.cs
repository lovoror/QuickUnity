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
    public class TimerEvent : Events.Event
    {
        /// <summary>
        /// When timer action start, this event will be dispatched.
        /// </summary>
        public const string TimerStart = "TimerStart";

        /// <summary>
        /// When timer reach the time by delay set, this event will be dispatched.
        /// </summary>
        public const string Timer = "Timer";

        /// <summary>
        /// When timer action pause, this event will be dispatched.
        /// </summary>
        public const string TimerPause = "TimerPause";

        /// <summary>
        /// When timer action resume, this event will be dispatched.
        /// </summary>
        public const string TimerResume = "TimerResume";

        /// <summary>
        /// When timer action stop, this event will be dispatched.
        /// </summary>
        public const string TimerStop = "TimerStop";

        /// <summary>
        /// When timer complete the repeat count, this event will be dispatched.
        /// </summary>
        public const string TimerComplete = "TimerComplete";

        /// <summary>
        /// The timer object.
        /// </summary>
        private ITimer m_timer;

        /// <summary>
        /// Gets or sets the timer object.
        /// </summary>
        /// <value>The timer object.</value>
        public ITimer timer
        {
            get { return (ITimer)m_context; }
            set { m_context = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="context">The context.</param>
        public TimerEvent(string type, object context = null)
            : base(type, context)
        {
        }
    }
}