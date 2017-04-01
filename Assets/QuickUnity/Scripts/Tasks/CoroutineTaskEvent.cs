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

namespace QuickUnity.Tasks
{
    /// <summary>
    /// The CoroutineTaskEvent class represents event objects that are specific to the CoroutineTask object.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.Event"/>
    public class CoroutineTaskEvent : Events.Event
    {
        /// <summary>
        /// Dispatched whenever a CoroutineTask start to run.
        /// </summary>
        public const string CoroutineTaskStart = "CoroutineTaskStart";

        /// <summary>
        /// Dispatched whenever a CoroutineTask pause.
        /// </summary>
        public const string CoroutineTaskPause = "CoroutineTaskPause";

        /// <summary>
        /// Dispatched whenever a CoroutineTask resume to run.
        /// </summary>
        public const string CoroutineTaskResume = "CoroutineTaskResume";

        /// <summary>
        /// Dispatched whenever a CoroutineTask stop.
        /// </summary>
        public const string CoroutineTaskStop = "CoroutineTaskStop";

        /// <summary>
        /// Gets or sets the coroutine task.
        /// </summary>
        /// <value>The coroutine task.</value>
        public ICoroutineTask coroutineTask
        {
            get { return (ICoroutineTask)m_context; }
            set { m_context = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoroutineTaskEvent"/> class.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="context">The CoroutineTask object.</param>
        public CoroutineTaskEvent(string eventType, ICoroutineTask task = null)
            : base(eventType, task)
        {
        }
    }
}