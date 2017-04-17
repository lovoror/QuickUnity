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

using UnityEngine;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// Template class for singleton pattern which .
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="QuickUnity.Events.BehaviourEventDispatcher"/>
    public abstract class SingletonBehaviour<T> : BehaviourEventDispatcher where T : BehaviourEventDispatcher
    {
        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static T s_instance = null;

        /// <summary>
        /// A value indicating whether this <see cref="MonoBehaviourSingleton{T}"/> instance is instantiated.
        /// </summary>
        private static bool s_instantiated = false;

        /// <summary>
        /// Gets or sets the singleton instance.
        /// </summary>
        /// <value>The singleton instance.</value>
        public static T instance
        {
            get
            {
                if (!s_instantiated)
                {
                    T[] foundObjects = FindObjectsOfType<T>();

                    if (foundObjects.Length > 0)
                    {
                        // Found instances already have.
                        instance = foundObjects[0];

                        if (foundObjects.Length > 1)
                        {
                            Debug.LogWarningFormat("There are more than one instance of MonoBehaviourSingleton of type \"{0}\". Keeping the first. Destroying the others.", typeof(T).ToString());

                            for (int i = 1, length = foundObjects.Length; i < length; ++i)
                            {
                                T behaviour = foundObjects[i];
                                Destroy(behaviour.gameObject);
                            }
                        }
                    }
                    else
                    {
                        // Make new one.
                        GameObject go = new GameObject();
                        go.name = typeof(T).Name;
                        instance = go.AddComponent<T>();
                    }
                }

                return s_instance;
            }

            set
            {
                s_instance = value;
                s_instantiated = value != null;

                if (Application.isPlaying && value != null)
                {
                    DontDestroyOnLoad(s_instance.gameObject);
                }
            }
        }

        #region Messages

        /// <summary>
        /// Called when script receive message Awake.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            // If singleton instance got null, find the instance already have.
            if (s_instance == null)
            {
                instance = FindObjectOfType<T>();
            }
        }

        #endregion Messages
    }
}