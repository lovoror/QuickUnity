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

namespace QuickUnity.Commands
{
    /// <summary>
    /// The event object for CommandQueue.
    /// </summary>
    public class CommandQueueEvent : Event
    {
        /// <summary>
        /// When CommandQueue start to execute commands, it will dispatch this event.
        /// </summary>
        public const string Start = "start";

        /// <summary>
        /// When CommandQueue in the progress of executing commands, it will dispatch this event.
        /// </summary>
        public const string Progress = "progress";

        /// <summary>
        /// When CommandQueue interrupt to execute commands, it will dispatch this event.
        /// </summary>
        public const string Interrupt = "interrupt";

        /// <summary>
        /// When CommandQueue complete executing commands, it will dispatch this event.
        /// </summary>
        public const string Complete = "complete";

        /// <summary>
        /// Gets or sets the progress of command queue executing.
        /// </summary>
        /// <value>
        /// The progress of command queue executing.
        /// </value>
        public float progress;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandQueueEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target object of event.</param>
        public CommandQueueEvent(string type, object target = null)
            : base(type, target)
        {
        }
    }
}