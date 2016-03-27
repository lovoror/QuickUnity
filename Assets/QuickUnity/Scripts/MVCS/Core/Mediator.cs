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

namespace QuickUnity.MVCS
{
    /// <summary>
    /// Abstract MVCS <c>IMediator</c> implementation.
    /// </summary>
    public class Mediator : IMediator
    {
        /// <summary>
        /// The mediator name.
        /// </summary>
        protected string m_mediatorName;

        /// <summary>
        /// The mediator name.
        /// </summary>
        public string mediatorName
        {
            get { return m_mediatorName; }
        }

        /// <summary>
        /// The view component.
        /// </summary>
        protected object m_viewComponent;

        /// <summary>
        /// Gets the view component.
        /// </summary>
        /// <value>
        /// The view component.
        /// </value>
        public object viewComponent
        {
            get { return m_viewComponent; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator" /> class.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        /// <param name="viewComponent">The view component.</param>
        public Mediator(string mediatorName = null, object viewComponent = null)
        {
            mediatorName = string.IsNullOrEmpty(mediatorName) ? this.GetType().FullName : mediatorName;
            m_viewComponent = viewComponent;
        }

        #region Public Functions

        #region IMediator Implementations

        /// <summary>
        /// Called when [register].
        /// </summary>
        public virtual void OnRegister()
        {
        }

        /// <summary>
        /// Called when [remove].
        /// </summary>
        public virtual void OnRemove()
        {
        }

        #endregion IMediator Implementations

        #endregion Public Functions

        #region Protected Functions

        /// <summary>
        /// Adds the context event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        protected void AddContextEventListener(string type, Action<Event> listener)
        {
        }

        /// <summary>
        /// Adds the module event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        protected void AddModuleEventListener(string type, Action<Event> listener)
        {
        }

        #endregion Protected Functions
    }
}