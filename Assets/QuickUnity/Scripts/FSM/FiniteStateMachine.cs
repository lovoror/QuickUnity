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
using System.Collections.Generic;

namespace QuickUnity.FSM
{
    /// <summary>
    /// Finite State Machine hold all states, and control them.
    /// </summary>
    public class FiniteStateMachine : EventDispatcher, IFiniteStateMachine
    {
        /// <summary>
        /// The state dictionary.
        /// </summary>
        private Dictionary<string, IFSMState> m_stateDic;

        /// <summary>
        /// The name of finite state machine.
        /// </summary>
        private string m_name;

        /// <summary>
        /// The current state.
        /// </summary>
        private IFSMState m_currentState;

        /// <summary>
        /// Gets the name of finite state machine.
        /// </summary>
        /// <value>
        /// The name of finite state machine.
        /// </value>
        public string name
        {
            get { return m_name; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FiniteStateMachine" /> class.
        /// </summary>
        /// <param name="name">The name of finite state machine.</param>
        public FiniteStateMachine(string name = null)
            : base()
        {
            m_name = name;
            m_stateDic = new Dictionary<string, IFSMState>();

            if (FSMManager.instance != null)
                FSMManager.instance.AddFSM(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="FiniteStateMachine"/> class.
        /// </summary>
        ~FiniteStateMachine()
        {
            if (m_stateDic != null)
            {
                m_stateDic.Clear();
                m_stateDic = null;
            }
        }

        #region API

        /// <summary>
        /// Enters the state.
        /// </summary>
        /// <param name="state">The state object.</param>
        public void EnterState(IFSMState state)
        {
            if (state == null)
                return;

            IFSMState previousState = m_currentState;
            m_currentState = state;

            if (previousState != null)
                previousState.OnExit();

            if (m_currentState != null)
                m_currentState.OnEnter();

            FSMEvent fsmEvent = new FSMEvent(FSMEvent.StateTransition);
            fsmEvent.previousState = previousState;
            fsmEvent.currentState = m_currentState;
            DispatchEvent(fsmEvent);
        }

        /// <summary>
        /// This function is called every fixed framerate frame.
        /// </summary>
        public void FixedUpdate()
        {
            if (m_currentState != null)
                m_currentState.OnFixedUpdate();
        }

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        public void Update()
        {
            if (m_currentState != null)
                m_currentState.OnUpdate();
        }

        /// <summary>
        /// LateUpdate is called every frame.
        /// </summary>
        public void LateUpdate()
        {
            if (m_currentState != null)
                m_currentState.OnLateUpdate();
        }

        /// <summary>
        /// Adds the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void AddState(IFSMState state)
        {
            if (state == null)
                return;

            if (!m_stateDic.ContainsKey(state.stateName))
                m_stateDic.Add(state.stateName, state);
        }

        /// <summary>
        /// Removes the state.
        /// </summary>
        /// <param name="stateName">Name of the state.</param>
        public void RemoveState(string stateName)
        {
            if (string.IsNullOrEmpty(stateName))
                return;

            if (m_stateDic.ContainsKey(stateName))
                m_stateDic.Remove(stateName);
        }

        /// <summary>
        /// Removes the state.
        /// </summary>
        /// <param name="state">The state.</param>
        public void RemoveState(IFSMState state)
        {
            if (state == null)
                return;

            string stateName = state.stateName;
            RemoveState(stateName);
        }

        #endregion API
    }
}