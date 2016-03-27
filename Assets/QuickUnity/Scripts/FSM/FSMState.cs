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

using System.Collections.Generic;

namespace QuickUnity.FSM
{
    /// <summary>
    /// A base <c>IFSMState</c> implementation.
    /// </summary>
    public abstract class FSMState : IFSMState
    {
        /// <summary>
        /// The name of state.
        /// </summary>
        protected string m_stateName;

        /// <summary>
        /// Gets the name of the state.
        /// </summary>
        /// <value>
        /// The name of the state.
        /// </value>
        public string stateName
        {
            get { return m_stateName; }
        }

        /// <summary>
        /// The action list of state.
        /// </summary>
        protected List<IFSMStateAction> m_actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="FSMState"/> class.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        public FSMState(string stateName)
        {
            m_stateName = stateName;
            m_actions = new List<IFSMStateAction>();
        }

        #region API

        /// <summary>
        /// Called when [enter].
        /// </summary>
        public void OnEnter()
        {
            foreach (IFSMStateAction action in m_actions)
            {
                action.OnEnter();
            }
        }

        /// <summary>
        /// Called when [fixed update].
        /// </summary>
        public void OnFixedUpdate()
        {
            foreach (IFSMStateAction action in m_actions)
            {
                action.OnFixedUpdate();
            }
        }

        /// <summary>
        /// Called when [update].
        /// </summary>
        public void OnUpdate()
        {
            foreach (IFSMStateAction action in m_actions)
            {
                action.OnUpdate();
            }
        }

        /// <summary>
        /// Called when [late update].
        /// </summary>
        public void OnLateUpdate()
        {
            foreach (IFSMStateAction action in m_actions)
            {
                action.OnLateUpdate();
            }
        }

        /// <summary>
        /// Called when [exit].
        /// </summary>
        public void OnExit()
        {
            foreach (IFSMStateAction action in m_actions)
            {
                action.OnExit();
            }
        }

        #endregion API
    }
}