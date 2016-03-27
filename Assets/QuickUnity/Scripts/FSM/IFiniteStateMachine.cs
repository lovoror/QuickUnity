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
    /// The interface definition for the FiniteStateMachine component.
    /// </summary>
    public interface IFiniteStateMachine : IEventDispatcher
    {
        /// <summary>
        /// Gets the name of finite state machine.
        /// </summary>
        /// <value>
        /// The name of finite state machine.
        /// </value>
        string name
        {
            get;
        }

        /// <summary>
        /// Enters the state.
        /// </summary>
        /// <param name="state">The state object.</param>
        void EnterState(IFSMState state);

        /// <summary>
        /// This function is called every fixed framerate frame.
        /// </summary>
        void FixedUpdate();

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        void Update();

        /// <summary>
        /// LateUpdate is called every frame.
        /// </summary>
        void LateUpdate();
    }
}