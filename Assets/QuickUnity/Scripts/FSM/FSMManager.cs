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

using QuickUnity.Patterns;
using System.Collections.Generic;

namespace QuickUnity.FSM
{
    /// <summary>
    /// The FSMManager hold all finite state machines.
    /// </summary>
    public class FSMManager : MonoBehaviourSingleton<FSMManager>
    {
        /// <summary>
        /// The dictionary of finite state machine.
        /// </summary>
        private Dictionary<string, IFiniteStateMachine> m_finiteStateMachineDic;

        #region Messages

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            if (m_finiteStateMachineDic == null)
                m_finiteStateMachineDic = new Dictionary<string, IFiniteStateMachine>();
        }

        /// <summary>
        /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void FixedUpdate()
        {
            foreach (IFiniteStateMachine fsm in m_finiteStateMachineDic.Values)
            {
                fsm.FixedUpdate();
            }
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            foreach (IFiniteStateMachine fsm in m_finiteStateMachineDic.Values)
            {
                fsm.Update();
            }
        }

        /// <summary>
        /// LateUpdate is called every frame, if the Behaviour is enabled.
        /// </summary>
        private void LateUpdate()
        {
            foreach (IFiniteStateMachine fsm in m_finiteStateMachineDic.Values)
            {
                fsm.LateUpdate();
            }
        }

        #endregion Messages

        #region API

        /// <summary>
        /// Adds the FSM.
        /// </summary>
        /// <param name="finiteStateMachine">The finite state machine.</param>
        public void AddFSM(IFiniteStateMachine finiteStateMachine)
        {
            if (finiteStateMachine == null)
                return;

            string fsmName = finiteStateMachine.name;

            if (!m_finiteStateMachineDic.ContainsKey(fsmName))
                m_finiteStateMachineDic.Add(fsmName, finiteStateMachine);
        }

        /// <summary>
        /// Removes the FSM.
        /// </summary>
        /// <param name="name">The name of finite state machine.</param>
        public void RemoveFSM(string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            if (m_finiteStateMachineDic.ContainsKey(name))
                m_finiteStateMachineDic.Remove(name);
        }

        /// <summary>
        /// Removes the FSM.
        /// </summary>
        /// <param name="fsm">The finite state machine.</param>
        public void RemoveFSM(IFiniteStateMachine fsm)
        {
            if (fsm == null)
                return;

            string fsmName = fsm.name;
            RemoveFSM(fsmName);
        }

        #endregion API
    }
}