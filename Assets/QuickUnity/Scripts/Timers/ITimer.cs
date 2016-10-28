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
    /// The interface definition for the Timer component.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.IEventDispatcher"/>
    public interface ITimer : IEventDispatcher
    {
        /// <summary>
        /// Gets the current count of timer.
        /// </summary>
        /// <value>The current count.</value>
        uint currentCount { get; }

        /// <summary>
        /// Gets or sets the delay time of timer.
        /// </summary>
        /// <value>The delay.</value>
        float delay { get; set; }

        /// <summary>
        /// Gets or sets the repeat count of timer.
        /// </summary>
        /// <value>The repeat count.</value>
        uint repeatCount { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITimer"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        bool running { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [ignore time scale].
        /// </summary>
        /// <value><c>true</c> if [ignore time scale]; otherwise, <c>false</c>.</value>
        bool ignoreTimeScale { get; set; }

        /// <summary>
        /// This timer start timing.
        /// </summary>
        void Start();

        /// <summary>
        /// This timer pause timing.
        /// </summary>
        void Pause();

        /// <summary>
        /// This timer resume timing.
        /// </summary>
        void Resume();

        /// <summary>
        /// This timer resets timing. Set currentCount to 0.
        /// </summary>
        void Reset();

        /// <summary>
        /// This timer stop timing.
        /// </summary>
        void Stop();

        /// <summary>
        /// Tick.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        void Tick(float deltaTime);
    }
}