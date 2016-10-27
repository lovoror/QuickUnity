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
using System.Collections;
using System.Collections.Generic;

namespace QuickUnity.Events
{
    /// <summary>
    /// The ThreadEventDispatcher class is the class for all classes that are working in child thread
    /// and dispatch events to Unity main thread.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.IThreadEventDispatcher"/>
    public class ThreadEventDispatcher : IThreadEventDispatcher
    {
        /// <summary>
        /// The listeners dictionary.
        /// </summary>
        private Dictionary<string, List<Action<Event>>> m_listeners = null;

        /// <summary>
        /// The pending listeners dictionary.
        /// </summary>
        private Dictionary<string, List<Action<Event>>> m_pendingListeners = null;

        /// <summary>
        /// The events list.
        /// </summary>
        private List<Event> m_events = null;

        /// <summary>
        /// The pending events list.
        /// </summary>
        private List<Event> m_pendingEvents = null;

        /// <summary>
        /// The pending state identify.
        /// </summary>
        private bool m_pending = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadEventDispatcher"/> class.
        /// </summary>
        public ThreadEventDispatcher()
        {
            m_listeners = new Dictionary<string, List<Action<Event>>>();
            m_pendingListeners = new Dictionary<string, List<Action<Event>>>();
            m_events = new List<Event>();
            m_pendingEvents = new List<Event>();
        }

        #region IThreadEventDispatcher Implements

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        public virtual void Update()
        {
            lock (this)
            {
                if (m_events != null && m_events.Count == 0)
                {
                    foreach (string eventType in m_pendingListeners.Keys)
                    {
                        foreach (Action<Event> listener in m_pendingListeners[eventType])
                            AddEventListener(eventType, listener);
                    }

                    m_pendingListeners.Clear();

                    m_events.AddRange(m_pendingEvents);
                    m_pendingEvents.Clear();
                    return;
                }

                m_pending = true;

                foreach (Event eventObject in m_events)
                {
                    if (m_listeners.ContainsKey(eventObject.eventType))
                    {
                        List<Action<Event>> listeners = m_listeners[eventObject.eventType];

                        foreach (Action<Event> listener in listeners)
                        {
                            if (listener != null)
                                listener.Invoke(eventObject);
                        }
                    }
                }

                m_events.Clear();
            }

            m_pending = false;
        }

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener
        /// receives notification of an event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The listener function that processes the event.</param>
        public void AddEventListener(string eventType, Action<Event> listener)
        {
            lock (this)
            {
                // Add to pending listeners dictionary.
                if (m_pending)
                {
                    if (!m_pendingListeners.ContainsKey(eventType))
                        m_pendingListeners.Add(eventType, new List<Action<Event>>());

                    if (!m_pendingListeners[eventType].Contains(listener))
                        m_pendingListeners[eventType].Add(listener);

                    return;
                }

                // Add to listeners dictionary.
                if (!m_listeners.ContainsKey(eventType))
                    m_listeners.Add(eventType, new List<Action<Event>>());

                if (!m_listeners[eventType].Contains(listener))
                    m_listeners[eventType].Add(listener);
            }
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="eventObject">The event object.</param>
        public void DispatchEvent(Event eventObject)
        {
            lock (this)
            {
                if (!m_listeners.ContainsKey(eventObject.eventType))
                    return;

                // Add to pending events list.
                if (m_pending)
                {
                    if (!m_pendingEvents.Contains(eventObject))
                        m_pendingEvents.Add(eventObject);

                    return;
                }

                m_events = new List<Event>(m_pendingEvents.ToArray());
                m_events.Add(eventObject);
                m_pendingEvents.Clear();
            }
        }

        /// <summary>
        /// Checks whether the EventDispatcher object has any listeners registered for a specific
        /// type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The listener function that processes the event.</param>
        /// <returns>
        /// A value of <c>true</c> if a listener of the specified type is registered; <c>false</c> otherwise.
        /// </returns>
        public bool HasEventListener(string eventType, Action<Event> listener)
        {
            return m_listeners.ContainsKey(eventType) && m_listeners[eventType].Contains(listener);
        }

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The listener object to remove.</param>
        public void RemoveEventListener(string eventType, Action<Event> listener)
        {
            lock (this)
            {
                // Can not remove event listener when this is pending.
                if (m_pending)
                    return;

                // Remove listener from listeners dictionary.
                if (!m_listeners.ContainsKey(eventType))
                    m_listeners.Add(eventType, new List<Action<Event>>());

                if (m_listeners[eventType].Contains(listener))
                    m_listeners[eventType].Remove(listener);
            }
        }

        #endregion IThreadEventDispatcher Implements
    }
}