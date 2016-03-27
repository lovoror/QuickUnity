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

namespace QuickUnity.FSM
{
    /// <summary>
    /// A FiniteStateMachine event.
    /// </summary>
    public class FSMEvent : Event
    {
        /// <summary>
        /// When the finite state machine make a state transition, it will dispatch this event.
        /// </summary>
        public const string StateTransition = "stateTransition";

        /// <summary>
        /// The previous state.
        /// </summary>
        private IFSMState m_previousState;

        /// <summary>
        /// Gets or sets the state of the previous.
        /// </summary>
        /// <value>
        /// The state of the previous.
        /// </value>
        public IFSMState previousState
        {
            get { return m_previousState; }
            set { m_previousState = value; }
        }

        /// <summary>
        /// The current state.
        /// </summary>
        private IFSMState m_currentState;

        /// <summary>
        /// Gets or sets the state of the current.
        /// </summary>
        /// <value>
        /// The state of the current.
        /// </value>
        public IFSMState currentState
        {
            get { return m_currentState; }
            set { m_currentState = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMEvent"/> class.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="target">The target object of event.</param>
        public FSMEvent(string type, object target = null)
            : base(type, target)
        {
        }
    }
}