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
using System.Collections.Generic;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// State machine to control state transitions.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.IStateMachine"/>
    /// <seealso cref="QuickUnity.Events.EventDispatcher"/>
    public class StateMachine : EventDispatcher, IStateMachine
    {
        /// <summary>
        /// The transition event bindings.
        /// </summary>
        private Dictionary<string, IState> m_transitionEventBindings;

        /// <summary>
        /// The current state.
        /// </summary>
        protected IState m_currentState;

        /// <summary>
        /// Initializes a new instance of the <see cref="StateMachine"/> class.
        /// </summary>
        public StateMachine()
            : base()
        {
            m_transitionEventBindings = new Dictionary<string, IState>();
        }

        #region IStateMachine Interface

        /// <summary>
        /// Directly set the active state by state object.
        /// </summary>
        /// <param name="state">The state object.</param>
        public void SetState(IState state)
        {
            if (state == null)
            {
                return;
            }

            IState prevState = m_currentState;

            if (prevState != null)
            {
                prevState.Exit(state);
            }

            state.Enter(prevState);

            // Dispatch event.
            StateEvent stateEvent = new StateEvent(StateEvent.Transition, prevState, state);
            DispatchEvent(stateEvent);
        }

        /// <summary>
        /// Registers the transition event.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="state">The state.</param>
        public void RegisterTransitionEvent(string eventType, IState state)
        {
            if (!m_transitionEventBindings.ContainsKey(eventType))
            {
                m_transitionEventBindings.Add(eventType, state);
            }
        }

        /// <summary>
        /// Changes the state by event type.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        public void ChangeState(string eventType)
        {
            IState targetState = null;

            if (m_transitionEventBindings.ContainsKey(eventType))
            {
                targetState = m_transitionEventBindings[eventType];
            }

            if (targetState != null)
            {
                SetState(targetState);
            }
        }

        /// <summary>
        /// Runs this state machine.
        /// </summary>
        public void Run()
        {
            if (m_currentState != null)
            {
                m_currentState.Execute();
            }
        }

        #endregion IStateMachine Interface
    }
}