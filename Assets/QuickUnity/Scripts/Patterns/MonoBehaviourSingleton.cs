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
using System;
using UnityEngine;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// Singleton template class for MonoBehaviour.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviourEventDispatcher where T : MonoBehaviourEventDispatcher
    {
        /// <summary>
        /// The name of root GameObject.
        /// </summary>
        public const string RootGameObjectName = "MonoBehaviourSingleton GameObjects";

        /// <summary>
        /// The static instance.
        /// </summary>
        private static T s_instance = null;

        /// <summary>
        /// If the application quit.
        /// </summary>
        private static bool s_applicationQuit = false;

        /// <summary>
        /// A value indicating whether this <see cref="MonoBehaviourSingleton{T}"/> is instantiated.
        /// </summary>
        private static bool s_instantiated = false;

        /// <summary>
        /// Gets a value indicating whether this <see cref="MonoBehaviourSingleton{T}"/> is instantiated.
        /// </summary>
        /// <value><c>true</c> if instantiated; otherwise, <c>false</c>.</value>
        public static bool instantiated
        {
            get { return s_instantiated; }
        }

        /// <summary>
        /// Gets the static instance.
        /// </summary>
        /// <value>The static instance.</value>
        public static T instance
        {
            get
            {
                if (s_applicationQuit)
                    return null;

                if (s_instantiated)
                    return s_instance;

                Type type = typeof(T);
                UnityEngine.Object[] objects = FindObjectsOfType(type);

                if (objects.Length > 0)
                {
                    instance = (T)objects[0];

                    if (objects.Length > 1)
                    {
                        Debug.LogWarning("There is more than one instance of MonoBehaviourSingleton of type \"" + type + "\". Keeping the first. Destroying the others.");

                        for (int i = 1, length = objects.Length; i < length; ++i)
                        {
                            MonoBehaviour behaviour = (MonoBehaviour)objects[i];
                            Destroy(behaviour.gameObject);
                        }
                    }

                    return s_instance;
                }

                // Find BehaviourSingletons root GameObject.
                GameObject root = GameObject.Find(RootGameObjectName);

                // If can not find out this GameObject, then create one.
                if (root == null)
                {
                    root = new GameObject(RootGameObjectName);
                    DontDestroyOnLoad(root);
                }

                // Find the GameObject who hold the singleton of this component.
                Transform singletonTrans = root.transform.FindChild(type.Name);

                if (singletonTrans == null)
                {
                    // Create a GameObject to add the component of this Singleton.
                    GameObject singletonGameObject = new GameObject(type.Name);
                    singletonGameObject.transform.parent = root.transform;
                    instance = singletonGameObject.AddComponent<T>();
                }
                else
                {
                    // If already has one, get it.
                    T component = singletonTrans.GetComponent<T>();

                    if (component != null)
                        instance = component;
                }

                return s_instance;
            }

            private set
            {
                s_instance = value;
                s_instantiated = value != null;

                if (value != null)
                    DontDestroyOnLoad(s_instance);
            }
        }

        #region Messages

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            if (s_instance == null)
                instance = GetComponent<T>();

            OnAwake();
        }

        /// <summary>
        /// Called when [destroy].
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            OnDispose();
            instance = null;
        }

        /// <summary>
        /// Called when [application quit].
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            s_applicationQuit = true;
        }

        #endregion Messages

        #region Public Functions

        /// <summary>
        /// Destroy the gameObject self.
        /// </summary>
        public void Dispose()
        {
            if (gameObject != null)
                Destroy(gameObject);
        }

        #endregion Public Functions

        #region Protected Functions

        #region Virtual Functions

        /// <summary>
        /// Called when the message Awake of MonoBehaviour was invoked.
        /// </summary>
        protected virtual void OnAwake()
        {
        }

        /// <summary>
        /// Called when the message OnDestroy of MonoBehaviour was invoked.
        /// </summary>
        protected virtual void OnDispose()
        {
        }

        #endregion Virtual Functions

        #endregion Protected Functions
    }
}