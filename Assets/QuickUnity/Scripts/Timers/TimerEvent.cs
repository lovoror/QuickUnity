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
    /// When you use Timer component, Timer component will dispatch TimerEvent.
    /// </summary>
    public class TimerEvent : Events.Event
    {
        /// <summary>
        /// When timer action start, this event will be dispatched.
        /// </summary>
        public const string TimerStart = "timerStart";

        /// <summary>
        /// When timer reach the time by delay set, this event will be dispatched.
        /// </summary>
        public const string Timer = "timer";

        /// <summary>
        /// When timer complete the repeat count, this event will be dispatched.
        /// </summary>
        public const string TimerComplelte = "timerComplete";

        /// <summary>
        /// The timer object.
        /// </summary>
        private ITimer m_timer;

        /// <summary>
        /// Gets the timer object.
        /// </summary>
        /// <value>The timer.</value>
        public ITimer timer
        {
            get { return (ITimer)m_target; }
        }

        /// <summary>
        /// The delta time.
        /// </summary>
        private float m_deltaTime;

        /// <summary>
        /// Gets the delta time.
        /// </summary>
        /// <value>The delta time.</value>
        public float deltaTime
        {
            get { return m_deltaTime; }
            set { m_deltaTime = value; }
        }

        /// <summary>
        /// Initializes a new sInstance of the <see cref="TimerEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target object of event.</param>
        /// <param name="deltaTime">The delta time.</param>
        public TimerEvent(string type, object target = null, float deltaTime = 0.0f)
            : base(type, target)
        {
            m_deltaTime = deltaTime;
        }
    }
}