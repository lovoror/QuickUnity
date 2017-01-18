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

using QuickUnity.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Events
{
    /// <summary>
    /// The EventDispatcher class is the base class for all classes that dispatch events.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.IEventDispatcher"/>
    public class EventDispatcher : IEventDispatcher
    {
        /// <summary>
        /// The listeners dictionary.
        /// </summary>
        private Dictionary<string, ArrayList> m_listeners = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDispatcher"/> class.
        /// </summary>
        public EventDispatcher()
        {
            m_listeners = new Dictionary<string, ArrayList>();
        }

        /// <summary>
        /// Performs an implicit conversion from <see cref="EventDispatcher"/> to <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="exists">The exists.</param>
        /// <returns>The result of the conversion.</returns>
        public static implicit operator bool(EventDispatcher exists)
        {
            return exists != null;
        }

        #region IEventDispatcher Implementations

        /// <summary>
        /// Registers an event listener object with an EventDispatcher object so that the listener
        /// receives notification of an event.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parameter of the method that this delegate encapsulates.
        /// </typeparam>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The listener function that processes the event.</param>
        public void AddEventListener<T>(string eventType, Action<T> listener) where T : Event
        {
            if (string.IsNullOrEmpty(eventType) || listener == null)
                return;

            if (m_listeners != null && !m_listeners.ContainsKey(eventType))
            {
                ArrayList list = new ArrayList();
                m_listeners.Add(eventType, list);
            }

            if (m_listeners != null && !m_listeners[eventType].Contains(listener))
                m_listeners[eventType].Add(listener);
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parameter of the method that this delegate encapsulates.
        /// </typeparam>
        /// <param name="eventObject">The event object.</param>
        public void DispatchEvent<T>(T eventObject) where T : Event
        {
            if (eventObject == null)
                return;

            string eventType = eventObject.eventType;
            eventObject.target = this;

            if (m_listeners != null && m_listeners.ContainsKey(eventType))
            {
                ArrayList listeners = m_listeners[eventType];

                for (int i = 0, length = listeners.Count; i < length; ++i)
                {
                    Action<T> listener = listeners[i] as Action<T>;

                    if (listener != null && listener.GetInvocationList().Length > 0)
                        listener.Invoke(eventObject);
                }
            }
        }

        /// <summary>
        /// Checks whether the EventDispatcher object has listener registered for a specific type of event.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parameter of the method that this delegate encapsulates.
        /// </typeparam>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The listener function that processes the event.</param>
        /// <returns>
        /// A value of <c>true</c> if a listener of the specified type is registered; <c>false</c> otherwise.
        /// </returns>
        public bool HasEventListener<T>(string eventType, Action<T> listener) where T : Event
        {
            if (string.IsNullOrEmpty(eventType) || listener == null)
                return false;

            return m_listeners != null && m_listeners.ContainsKey(eventType) && m_listeners[eventType].Contains(listener);
        }

        /// <summary>
        /// Checks whether the EventDispatcher object has listeners registered for a specific type of event.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <returns>
        /// <c>true</c> if [has event listener] [the specified event type]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasEventListeners(string eventType)
        {
            if (m_listeners != null && m_listeners.Count > 0)
            {
                ArrayList list = m_listeners[eventType];

                if (list != null && list.Count > 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether the EventDispatcher object has listeners registered for a target object.
        /// </summary>
        /// <param name="target">The target object.</param>
        /// <returns>
        /// <c>true</c> if [has event listeners] for [the specified target]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasEventListeners(object target)
        {
            if (m_listeners != null && m_listeners.Count > 0)
            {
                foreach (KeyValuePair<string, ArrayList> kvp in m_listeners)
                {
                    ArrayList list = kvp.Value;

                    if (list != null && list.Count > 0)
                    {
                        for (int i = 0, length = list.Count; i < length; ++i)
                        {
                            object listenerTarget = ReflectionUtility.GetObjectPropertyValue(list[i], "Target");

                            if (listenerTarget == target)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks whether the EventDispatcher object has any listener registered.
        /// </summary>
        /// <returns><c>true</c> if [has event listeners]; otherwise, <c>false</c>.</returns>
        public bool HasAnyEventListener()
        {
            if (m_listeners != null && m_listeners.Count > 0)
            {
                foreach (KeyValuePair<string, ArrayList> kvp in m_listeners)
                {
                    ArrayList list = kvp.Value;

                    if (list.Count > 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Removes a listener from the EventDispatcher object.
        /// </summary>
        /// <typeparam name="T">
        /// The type of the parameter of the method that this delegate encapsulates.
        /// </typeparam>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The listener object to remove.</param>
        public void RemoveEventListener<T>(string eventType, Action<T> listener) where T : Event
        {
            if (string.IsNullOrEmpty(eventType) || listener == null)
                return;

            if (m_listeners != null && m_listeners.ContainsKey(eventType))
            {
                ArrayList listeners = m_listeners[eventType];

                if (listeners.Contains(listener))
                    listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Removes the event listener by event type.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        public void RemoveEventListener(string eventType)
        {
            if (string.IsNullOrEmpty(eventType))
            {
                return;
            }

            if (m_listeners != null && m_listeners.ContainsKey(eventType))
            {
                m_listeners.Remove(eventType);
            }
        }

        /// <summary>
        /// Removes listeners from the EventDispatcher object by matching target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public void RemoveEventListeners(object target)
        {
            if (target == null)
            {
                return;
            }

            Dictionary<string, ArrayList> listenersMap = new Dictionary<string, ArrayList>();

            // Mark listeners which need to be removed.
            foreach (KeyValuePair<string, ArrayList> kvp in m_listeners)
            {
                string eventType = kvp.Key;
                ArrayList list = kvp.Value;

                for (int i = 0, length = list.Count; i < length; ++i)
                {
                    object listenerTarget = ReflectionUtility.GetObjectPropertyValue(list[i], "Target");

                    if (listenerTarget == target)
                    {
                        if (!listenersMap.ContainsKey(eventType))
                        {
                            listenersMap[eventType] = new ArrayList();
                        }

                        listenersMap[eventType].Add(list[i]);
                    }
                }
            }

            // Remove listeners.
            if (listenersMap.Count > 0)
            {
                foreach (KeyValuePair<string, ArrayList> kvp in listenersMap)
                {
                    string eventType = kvp.Key;
                    ArrayList removedListeners = kvp.Value;

                    for (int i = 0, length = removedListeners.Count; i < length; ++i)
                    {
                        ArrayList listeners = m_listeners[eventType];

                        if (listeners != null && listeners.Contains(removedListeners[i]))
                        {
                            listeners.Remove(removedListeners[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public void RemoveAllEventListeners()
        {
            if (m_listeners != null)
            {
                m_listeners.Clear();
            }
        }

        #endregion IEventDispatcher Implementations
    }
}