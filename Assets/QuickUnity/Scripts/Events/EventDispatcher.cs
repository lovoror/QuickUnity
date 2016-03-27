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
using System.Collections.Generic;

namespace QuickUnity.Events
{
    /// <summary>
    /// Class EventDispatcher.
    /// </summary>
    public class EventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// The listeners dictionary.
        /// </summary>
        protected Dictionary<string, List<Action<Event>>> m_listeners;

        /// <summary>
        /// Initializes a new sInstance of the <see cref="EventDispatcher"/> class.
        /// </summary>
        public EventDispatcher()
        {
            m_listeners = new Dictionary<string, List<Action<Event>>>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="EventDispatcher"/> class.
        /// </summary>
        ~EventDispatcher()
        {
            RemoveAllEventListeners();
            m_listeners = null;
        }

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public virtual void AddEventListener(string type, Action<Event> listener)
        {
            if (!m_listeners.ContainsKey(type))
                m_listeners.Add(type, new List<Action<Event>>());

            if (!m_listeners[type].Contains(listener))
                m_listeners[type].Add(listener);
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="event">The event.</param>
        public virtual void DispatchEvent(Event evt)
        {
            string type = evt.eventType;

            if (m_listeners.ContainsKey(type))
            {
                List<Action<Event>> listeners = m_listeners[type];
                int count = listeners.Count;
                for (int i = 0; i < count; i++)
                {
                    if (listeners[i] != null)
                    {
                        listeners[i](evt);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public virtual bool HasEventListener(string type, Action<Event> listener)
        {
            return m_listeners.ContainsKey(type) && m_listeners[type].Contains(listener);
        }

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type of event.</param>
        public virtual void RemoveEventListenerByName(string type)
        {
            if (m_listeners.ContainsKey(type))
                m_listeners.Remove(type);
        }

        /// <summary>
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public virtual void RemoveEventListenersByTarget(object target)
        {
            Dictionary<string, List<Action<Event>>> listeners = new Dictionary<string, List<Action<Event>>>();

            // 记录需要删除的Listener
            foreach (KeyValuePair<string, List<Action<Event>>> kvp in m_listeners)
            {
                string eventType = kvp.Key;
                List<Action<Event>> list = kvp.Value;

                foreach (Action<Event> listner in list)
                {
                    if (listner.Target == target)
                    {
                        if (!listeners.ContainsKey(eventType))
                            listeners[eventType] = new List<Action<Event>>();

                        listeners[eventType].Add(listner);
                    }
                }
            }

            // 实际删除Listener
            if (listeners.Count > 0)
            {
                foreach (KeyValuePair<string, List<Action<Event>>> kvp in listeners)
                {
                    string eventType = kvp.Key;
                    List<Action<Event>> list = kvp.Value;

                    foreach (Action<Event> listener in list)
                        RemoveEventListener(eventType, listener);
                }
            }
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public virtual void RemoveEventListener(string type, Action<Event> listener)
        {
            if (m_listeners.ContainsKey(type))
            {
                List<Action<Event>> listeners = m_listeners[type];

                if (listeners.Contains(listener))
                    listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public virtual void RemoveAllEventListeners()
        {
            if (m_listeners != null)
                m_listeners.Clear();
        }
    }
}