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
    /// A base <c>IThreadEventDispatcher</c> implementation.
    /// </summary>
    public class ThreadEventDispatcher : EventDispatcher, IThreadEventDispatcher
    {
        /// <summary>
        /// The pending listeners.
        /// </summary>
        private Dictionary<string, List<Action<Event>>> m_pendingListeners;

        /// <summary>
        /// The events list.
        /// </summary>
        private List<Event> m_events;

        /// <summary>
        /// The pending events.
        /// </summary>
        private List<Event> m_pendingEvents;

        /// <summary>
        /// If the listener is in pending.
        /// </summary>
        protected bool m_pending = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="ThreadEventDispatcher"/> is pending.
        /// </summary>
        /// <value>
        ///   <c>true</c> if pending; otherwise, <c>false</c>.
        /// </value>
        public bool pending
        {
            get { return m_pending; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadEventDispatcher"/> class.
        /// </summary>
        public ThreadEventDispatcher()
            : base()
        {
            m_pendingListeners = new Dictionary<string, List<Action<Event>>>();
            m_events = new List<Event>();
            m_pendingEvents = new List<Event>();
        }

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public override void AddEventListener(string type, Action<Event> listener)
        {
            lock (this)
            {
                if (m_pending)
                {
                    if (!m_pendingListeners.ContainsKey(type))
                        m_pendingListeners.Add(type, new List<Action<Event>>());

                    if (!m_pendingListeners[type].Contains(listener))
                        m_pendingListeners[type].Add(listener);

                    return;
                }

                base.AddEventListener(type, listener);
            }
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="evt">The evt.</param>
        public override void DispatchEvent(Event evt)
        {
            lock (this)
            {
                if (!m_listeners.ContainsKey(evt.eventType))
                    return;

                bool running = true;

                do
                {
                    if (m_pending)
                    {
                        if (!m_pendingEvents.Contains(evt))
                            m_pendingEvents.Add(evt);
                    }
                    else
                    {
                        foreach (Event pendingEvent in m_pendingEvents)
                            m_events.Add(pendingEvent);

                        m_pendingEvents.Clear();
                        m_events.Add(evt);
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type of event.</param>
        public override void RemoveEventListenerByName(string type)
        {
            lock (this)
            {
                if (!m_listeners.ContainsKey(type))
                    return;

                bool running = true;

                do
                {
                    if (!m_pending)
                    {
                        base.RemoveEventListenerByName(type);
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public override void RemoveEventListenersByTarget(object target)
        {
            lock (this)
            {
                bool running = true;

                do
                {
                    if (!m_pending)
                    {
                        base.RemoveEventListenersByTarget(target);
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public override void RemoveEventListener(string type, Action<Event> listener)
        {
            lock (this)
            {
                if (!m_listeners.ContainsKey(type))
                    return;

                bool running = true;

                do
                {
                    if (!m_pending)
                    {
                        base.RemoveEventListener(type, listener);
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public override void RemoveAllEventListeners()
        {
            lock (this)
            {
                bool running = true;

                do
                {
                    if (!m_pending)
                    {
                        base.RemoveAllEventListeners();
                        running = false;
                    }
                } while (running);
            }
        }

        /// <summary>
        /// Updates.
        /// </summary>
        public void Update()
        {
            lock (this)
            {
                if (m_events.Count == 0)
                {
                    foreach (string eventType in m_pendingListeners.Keys)
                    {
                        foreach (Action<Event> listener in m_pendingListeners[eventType])
                            AddEventListener(eventType, listener);
                    }

                    m_pendingListeners.Clear();
                    ShiftPendingEvents();
                    return;
                }

                m_pending = true;

                foreach (Event evt in m_events)
                {
                    if (m_listeners.ContainsKey(evt.eventType))
                    {
                        List<Action<Event>> listeners = m_listeners[evt.eventType];

                        foreach (Action<Event> listener in listeners)
                        {
                            if (listener != null)
                                listener(evt);
                        }
                    }
                }

                m_events.Clear();
            }

            m_pending = false;
        }

        /// <summary>
        /// Determines whether [has pending event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        /// <returns><c>true</c> if [has pending event listener] [the specified type]; otherwise, <c>false</c>.</returns>
        public bool HasPendingEventListener(string type, Action<Event> listener)
        {
            return m_pendingListeners.ContainsKey(type) && m_pendingListeners[type].Contains(listener);
        }

        /// <summary>
        /// Shifts the pending events.
        /// </summary>
        private void ShiftPendingEvents()
        {
            foreach (Event evt in m_pendingEvents)
                m_events.Add(evt);

            m_pendingEvents.Clear();
        }
    }
}