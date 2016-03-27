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
    /// A base <c>ITimer</c> implementation.
    /// </summary>
    public class Timer : EventDispatcher, ITimer
    {
        /// <summary>
        /// The current count of timer.
        /// </summary>
        protected int m_currentCount = 0;

        /// <summary>
        /// Gets the current count of timer.
        /// </summary>
        /// <value>The current count.</value>
        public int currentCount
        {
            get { return m_currentCount; }
        }

        /// <summary>
        /// The delay time of timer.
        /// </summary>
        protected float m_delay;

        /// <summary>
        /// Gets or sets the delay time of timer.
        /// </summary>
        /// <value>The delay.</value>
        public float delay
        {
            get { return m_delay; }
            set { m_delay = value; }
        }

        /// <summary>
        /// The repeat count of timer.
        /// </summary>
        protected int m_repeatCount;

        /// <summary>
        /// Gets or sets the repeat count of timer.
        /// </summary>
        /// <value>The repeat count.</value>
        public int repeatCount
        {
            get { return m_repeatCount; }
            set { m_repeatCount = value; }
        }

        /// <summary>
        /// The state of timer. If the timer is running, it is true, or false.
        /// </summary>
        protected bool m_running;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Timer"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        public bool running
        {
            get { return m_running; }
        }

        /// <summary>
        /// The time of timer timing.
        /// </summary>
        protected float m_time = 0.0f;

        /// <summary>
        /// Gets the time of timer.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public float time
        {
            get { return m_time; }
        }

        /// <summary>
        /// Initializes a new sInstance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="delay">The delay time. Unit is second.</param>
        /// <param name="repeatCount">The repeat count.</param>
        public Timer(float delay, int repeatCount = 0)
            : base()
        {
            m_delay = delay;
            m_repeatCount = repeatCount;
        }

        /// <summary>
        /// Tick.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public virtual void Tick(float deltaTime)
        {
            if (m_running)
            {
                m_time += deltaTime;

                if (m_time >= m_delay)
                {
                    // Dispatch timer event.
                    m_currentCount++;

                    if (m_currentCount == int.MaxValue)
                        m_currentCount = 0;

                    DispatchEvent(new TimerEvent(TimerEvent.Timer, this, m_time));

                    // If reach the repeat count number, stop timing.
                    if (m_repeatCount != 0 && m_currentCount >= m_repeatCount)
                    {
                        Stop();
                        DispatchEvent(new TimerEvent(TimerEvent.TimerComplelte, this, m_time));
                    }

                    m_time = 0.0f;
                }
            }
        }

        /// <summary>
        /// This timer start timing.
        /// </summary>
        public void Start()
        {
            m_running = true;
            DispatchEvent(new TimerEvent(TimerEvent.TimerStart, this, m_time));
        }

        /// <summary>
        /// This timer pause timing.
        /// </summary>
        public void Pause()
        {
            m_running = false;
        }

        /// <summary>
        /// This timer resume timing.
        /// </summary>
        public void Resume()
        {
            m_running = true;
        }

        /// <summary>
        /// This timer resets timing. Set currentCount to 0.
        /// </summary>
        public void Reset()
        {
            if (m_running)
                Stop();

            m_currentCount = 0;
            m_time = 0.0f;
        }

        public void Reset(float delay, int repeatCount = 0)
        {
            m_delay = delay;
            m_repeatCount = repeatCount;
        }

        /// <summary>
        /// This timer stop timing.
        /// </summary>
        public void Stop()
        {
            m_running = false;
        }
    }
}