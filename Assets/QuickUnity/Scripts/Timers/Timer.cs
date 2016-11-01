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
using System;

namespace QuickUnity.Timers
{
    /// <summary>
    /// A base <c>ITimer</c> implementation.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.EventDispatcher"/>
    /// <seealso cref="QuickUnity.Timers.ITimer"/>
    public class Timer : EventDispatcher, ITimer
    {
        /// <summary>
        /// The minimum delay time.
        /// </summary>
        private const float MinDelayTime = 0.02f;

        /// <summary>
        /// The time of timing.
        /// </summary>
        protected float m_time;

        /// <summary>
        /// The current count of timer.
        /// </summary>
        protected uint m_currentCount = 0;

        /// <summary>
        /// Gets the current count of timer.
        /// </summary>
        /// <value>The current count.</value>
        public uint currentCount
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
        protected uint m_repeatCount;

        /// <summary>
        /// Gets or sets the repeat count of timer.
        /// </summary>
        /// <value>The repeat count.</value>
        public uint repeatCount
        {
            get { return m_repeatCount; }
            set { m_repeatCount = value; }
        }

        /// <summary>
        /// Whether this timer is enabled.
        /// </summary>
        protected bool m_enabled = true;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ITimer"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled Tick function will be invoked; otherwise, <c>false</c>.</value>
        public bool enabled
        {
            get { return m_enabled; }
            set
            {
                if (!value)
                {
                    // Disable timer object.
                    if (stopOnDisable)
                    {
                        Reset();
                    }
                    else
                    {
                        Pause();
                    }
                }
                else
                {
                    // Enable timer object.
                    if (!stopOnDisable && m_paused)
                    {
                        Resume();
                    }
                }

                m_enabled = value;
            }
        }

        /// <summary>
        /// The state of timer. If the timer is running, it is true, or false.
        /// </summary>
        protected bool m_running;

        /// <summary>
        /// Gets a value indicating whether this <see cref="Timer"/> is running.
        /// </summary>
        /// If the timer is running, this is
        /// <value><c>true</c>; otherwise, <c>false</c>.</value>
        public bool running
        {
            get { return m_running; }
        }

        protected bool m_paused;

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITimer"/> is paused.
        /// </summary>
        /// <value><c>true</c> if paused; otherwise, <c>false</c>.</value>
        public bool paused
        {
            get { return m_paused; }
        }

        /// <summary>
        /// The m_ignore time scale
        /// </summary>
        protected bool m_ignoreTimeScale = true;

        /// <summary>
        /// Gets or sets a value indicating whether [ignore time scale].
        /// </summary>
        /// <value><c>true</c> if [ignore time scale]; otherwise, <c>false</c>.</value>
        public bool ignoreTimeScale
        {
            get { return m_ignoreTimeScale; }
            set { m_ignoreTimeScale = value; }
        }

        /// <summary>
        /// Whether [stop on disable].
        /// </summary>
        protected bool m_stopOnDisable = true;

        /// <summary>
        /// Gets or sets a value indicating whether [stop on disable].
        /// </summary>
        /// <value><c>true</c> if [stop on disable]; otherwise, <c>false</c>.</value>
        public bool stopOnDisable
        {
            get { return m_stopOnDisable; }
            set { m_stopOnDisable = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class.
        /// </summary>
        /// <param name="delay">The delay.</param>
        /// <param name="repeatCount">The repeat count.</param>
        /// <param name="ignoreTimeScale">if set to <c>true</c> [ignore time scale].</param>
        /// <param name="stopOnDisable">if set to <c>true</c> [stop on disable].</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        public Timer(float delay, uint repeatCount = 0, bool ignoreTimeScale = true, bool stopOnDisable = true, bool autoStart = true)
        {
            if (delay > MinDelayTime)
            {
                m_delay = delay;
            }
            else
            {
                m_delay = MinDelayTime;
            }

            m_repeatCount = repeatCount;
            m_ignoreTimeScale = ignoreTimeScale;
            m_stopOnDisable = stopOnDisable;

            // Add timer instance into timer manager.
            TimerManager.instance.AddTimer(this, autoStart);
        }

        /// <summary>
        /// This timer start timing.
        /// </summary>
        public void Start()
        {
            m_running = true;
            m_paused = false;
            DispatchEvent(new TimerEvent(TimerEvent.TimerStart, this));
        }

        /// <summary>
        /// This timer pause timing.
        /// </summary>
        public void Pause()
        {
            m_running = false;
            m_paused = true;
            DispatchEvent(new TimerEvent(TimerEvent.TimerPause, this));
        }

        /// <summary>
        /// This timer resume timing.
        /// </summary>
        public void Resume()
        {
            m_running = true;
            m_paused = false;
            DispatchEvent(new TimerEvent(TimerEvent.TimerResume, this));
        }

        /// <summary>
        /// This timer stop timing.
        /// </summary>
        public void Stop()
        {
            m_running = false;
            m_paused = false;
            DispatchEvent(new TimerEvent(TimerEvent.TimerStop, this));
        }

        /// <summary>
        /// This timer resets timing. Set currentCount to 0.
        /// </summary>
        public void Reset()
        {
            if (m_running)
            {
                Stop();
            }

            m_currentCount = 0;
            m_time = 0f;

            DispatchEvent(new TimerEvent(TimerEvent.TimerReset, this));
        }

        /// <summary>
        /// Tick.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Tick(float deltaTime)
        {
            if (m_enabled && m_running && !m_paused)
            {
                m_time += deltaTime;

                if (m_time >= m_delay)
                {
                    // Dispatch timer event.
                    m_currentCount++;
                    DispatchEvent(new TimerEvent(TimerEvent.Timer, this));

                    // Dispatch timer complete event.
                    if (m_repeatCount != 0 && m_currentCount >= m_repeatCount)
                    {
                        Stop();
                        DispatchEvent(new TimerEvent(TimerEvent.TimerComplete, this));
                    }

                    // Reset delay time.
                    m_time -= m_delay;
                }
            }
        }

        /// <summary>
        /// Destroys this timer instance.
        /// </summary>
        public void Destroy()
        {
            TimerManager.instance.RemoveTimer(this);
        }
    }
}