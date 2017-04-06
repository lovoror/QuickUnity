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
    /// A base <c>IState</c> implementation.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.EventDispatcher"/>
    /// <seealso cref="QuickUnity.Patterns.IState"/>
    public abstract class State : EventDispatcher, IState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="State"/> class.
        /// </summary>
        public State()
            : base()
        {
        }

        #region Public Functions

        /// <summary>
        /// Called when the state is entered.
        /// </summary>
        /// <param name="prevState">The previous state.</param>
        public virtual void Enter(IState prevState)
        {
            DispatchEvent(new StateEvent(StateEvent.Enter, prevState, null));
        }

        /// <summary>
        /// Called when the state is executing.
        /// </summary>
        public virtual void Execute()
        {
            StateEvent stateEvent = new StateEvent(StateEvent.Execute);
            stateEvent.currentState = this;
            DispatchEvent(stateEvent);
        }

        /// <summary>
        /// Called when the active state is exited.
        /// </summary>
        /// <param name="nextState">The next state.</param>
        public virtual void Exit(IState nextState)
        {
            DispatchEvent(new StateEvent(StateEvent.Exit, nextState));
        }

        #endregion Public Functions
    }
}