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

using UnityEngine;

namespace QuickUnity
{
    /// <summary>
    /// The base MonoBehaivour class for QuickUnity.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour"/>
    public abstract class MonoBehaviourBase : MonoBehaviour
    {
        #region Messages

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            OnAwake();
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before any of the Update
        /// methods is called the first time.
        /// </summary>
        private void Start()
        {
            OnStart();
        }

        /// <summary>
        /// Reset to default values.
        /// </summary>
        private void Reset()
        {
            OnReset();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            OnUpdate();
        }

        /// <summary>
        /// FixedUpdate is called every fixed framerate frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void FixedUpdate()
        {
            OnFixedUpdate();
        }

        /// <summary>
        /// LateUpdate is called after all Update functions have been called, if the MonoBehaviour is enabled.
        /// </summary>
        private void LateUpdate()
        {
            OnLateUpdate();
        }

        #endregion Messages

        #region Protected Functions

        /// <summary>
        /// Called when script receive message Awake.
        /// </summary>
        protected virtual void OnAwake()
        {
        }

        /// <summary>
        /// Called when script receive message Start.
        /// </summary>
        protected virtual void OnStart()
        {
        }

        /// <summary>
        /// Called when script receive message Reset.
        /// </summary>
        protected virtual void OnReset()
        {
        }

        /// <summary>
        /// Called when script receive message Update.
        /// </summary>
        protected virtual void OnUpdate()
        {
        }

        /// <summary>
        /// Called when script receive message FixedUpdate.
        /// </summary>
        protected virtual void OnFixedUpdate()
        {
        }

        /// <summary>
        /// Called when script receive message LateUpdate.
        /// </summary>
        protected virtual void OnLateUpdate()
        {
        }

        #endregion Protected Functions
    }
}