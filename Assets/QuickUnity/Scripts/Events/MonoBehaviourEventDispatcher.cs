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

using System;
using UnityEngine;

namespace QuickUnity.Events
{
    /// <summary>
    /// Class MonoBehaviourEventDispatcher.
    /// </summary>
    public class MonoBehaviourEventDispatcher : MonoBehaviour, IEventDispatcher
    {
        /// <summary>
        /// The event dispatcher.
        /// </summary>
        private EventDispatcher m_dispatcher;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected virtual void Awake()
        {
            m_dispatcher = new EventDispatcher();
        }

        /// <summary>
        /// Called when [destroy].
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (m_dispatcher != null)
            {
                m_dispatcher.RemoveAllEventListeners();
                m_dispatcher = null;
            }
        }

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public void AddEventListener(string type, Action<Event> listener)
        {
            if (m_dispatcher != null)
                m_dispatcher.AddEventListener(type, listener);
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="event">The event.</param>
        public void DispatchEvent(Event evt)
        {
            if (m_dispatcher != null)
                m_dispatcher.DispatchEvent(evt);
        }

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        /// <returns><c>true</c> if [has event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        public bool HasEventListener(string type, Action<Event> listener)
        {
            if (m_dispatcher != null)
                return m_dispatcher.HasEventListener(type, listener);

            return false;
        }

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type.</param>
        public void RemoveEventListenerByName(string type)
        {
            if (m_dispatcher != null)
                m_dispatcher.RemoveEventListenerByName(type);
        }

        /// <summary>
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public virtual void RemoveEventListenersByTarget(object target)
        {
            if (m_dispatcher != null)
                m_dispatcher.RemoveEventListenersByTarget(target);
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public void RemoveEventListener(string type, Action<Event> listener)
        {
            if (m_dispatcher != null)
                m_dispatcher.RemoveEventListener(type, listener);
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public void RemoveAllEventListeners()
        {
            if (m_dispatcher != null)
                m_dispatcher.RemoveAllEventListeners();
        }
    }
}