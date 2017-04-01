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

namespace QuickUnity.Events
{
    /// <summary>
    /// The Event class is used as the base class for the creation of Event objects, which are passed
    /// as parameters to event listeners when an event occurs.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// The type of event.
        /// </summary>
        protected string m_eventType;

        /// <summary>
        /// The type of event.
        /// </summary>
        /// <value>The type of event.</value>
        public string eventType
        {
            get { return m_eventType; }
        }

        /// <summary>
        /// The context object.
        /// </summary>
        protected object m_context;

        /// <summary>
        /// The context object.
        /// </summary>
        /// <value>The context object.</value>
        public object context
        {
            get { return m_context; }
        }

        /// <summary>
        /// The event target.
        /// </summary>
        protected object m_target;

        /// <summary>
        /// Gets or sets the event target.
        /// </summary>
        /// <value>The event target.</value>
        public object target
        {
            get { return m_target; }
            set { m_target = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="context">The context object.</param>
        public Event(string eventType, object context = null)
        {
            m_eventType = eventType;
            m_context = context;
        }
    }
}