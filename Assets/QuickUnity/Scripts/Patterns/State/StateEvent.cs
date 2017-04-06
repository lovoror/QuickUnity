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

using QuickUnity.Events;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// The StateEvent class represents event objects that are specific to the State object.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.Event"/>
    public class StateEvent : Event
    {
        /// <summary>
        /// When a state become active, it will dispatch this event.
        /// </summary>
        public const string Enter = "Enter";

        /// <summary>
        /// When a state is executing, it will dispatch this event.
        /// </summary>
        public const string Execute = "Execute";

        /// <summary>
        /// When a state become inactive, it will dispatch this event.
        /// </summary>
        public const string Exit = "Exit";

        /// <summary>
        /// When the state machine make a state transition, it will dispatch this event.
        /// </summary>
        public const string Transition = "Transition";

        /// <summary>
        /// Gets or sets the previous state.
        /// </summary>
        /// <value>The previous state.</value>
        public IState prevState
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the current state.
        /// </summary>
        /// <value>The current state.</value>
        public IState currentState
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the next state.
        /// </summary>
        /// <value>The next state.</value>
        public IState nextState
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateEvent"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="context">The context object.</param>
        public StateEvent(string eventType, object context = null)
                    : base(eventType, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateEvent"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="prevState">The previous state.</param>
        /// <param name="currentState">The current state.</param>
        public StateEvent(string eventType, IState prevState, IState currentState)
            : base(eventType)
        {
            this.prevState = prevState;
            this.currentState = currentState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StateEvent"/> class.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="nextState">The next state.</param>
        public StateEvent(string eventType, IState nextState)
            : base(eventType)
        {
            this.nextState = nextState;
        }
    }
}