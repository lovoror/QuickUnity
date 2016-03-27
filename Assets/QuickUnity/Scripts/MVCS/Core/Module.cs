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
    /// Abstract MVCS <c>IModule</c> implementation.
    /// </summary>
    public class Module : IModule, IModelMap, IMediatorMap
    {
        /// <summary>
        /// The event dispatcher.
        /// </summary>
        protected IEventDispatcher m_eventDispatcher;

        /// <summary>
        /// Gets the event dispatcher.
        /// </summary>
        /// <value>
        /// The event dispatcher.
        /// </value>
        public IEventDispatcher eventDispatcher
        {
            get { return m_eventDispatcher; }
        }

        /// <summary>
        /// The model map.
        /// </summary>
        protected IModelMap m_modelMap;

        /// <summary>
        /// The mediator map.
        /// </summary>
        protected IMediatorMap m_mediatorMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Module()
        {
            m_eventDispatcher = new EventDispatcher();
            m_modelMap = new ModelMap();
            m_mediatorMap = new MediatorMap();
        }

        #region Public Functions

        #region IEventDispatcher Implementations

        /// <summary>
        /// Adds the event listener.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="listener">The listener.</param>
        public void AddEventListener(string type, Action<Event> listener)
        {
            m_eventDispatcher.AddEventListener(type, listener);
        }

        /// <summary>
        /// Dispatches the event.
        /// </summary>
        /// <param name="evt"></param>
        public void DispatchEvent(Event evt)
        {
            m_eventDispatcher.DispatchEvent(evt);
        }

        /// <summary>
        /// Determines whether [has event listener] [the specified type].
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        /// <returns>
        ///   <c>true</c> if [has event listener] [the specified type]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasEventListener(string type, Action<Event> listener)
        {
            return m_eventDispatcher.HasEventListener(type, listener);
        }

        /// <summary>
        /// Removes the event listener by event name.
        /// </summary>
        /// <param name="type">The type of event.</param>
        public void RemoveEventListenerByName(string type)
        {
            m_eventDispatcher.RemoveEventListenerByName(type);
        }

        /// <summary>
        /// Removes the event listeners by target.
        /// </summary>
        /// <param name="target">The target object.</param>
        public void RemoveEventListenersByTarget(object target)
        {
            m_eventDispatcher.RemoveEventListenersByTarget(target);
        }

        /// <summary>
        /// Removes the event listener.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="listener">The listener.</param>
        public void RemoveEventListener(string type, Action<Event> listener)
        {
            m_eventDispatcher.RemoveEventListener(type, listener);
        }

        /// <summary>
        /// Removes all event listeners.
        /// </summary>
        public void RemoveAllEventListeners()
        {
            m_eventDispatcher.RemoveAllEventListeners();
        }

        #endregion IEventDispatcher Implementations

        #region IModelMap Implementations

        /// <summary>
        /// Registers the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public virtual void RegisterModel(IModel model)
        {
            m_modelMap.RegisterModel(model);
            model.ModuleEventDispatcher = this;
        }

        /// <summary>
        /// Retrieves the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>
        /// The model object.
        /// </returns>
        public IModel RetrieveModel(Type modelType)
        {
            return m_modelMap.RetrieveModel(modelType);
        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="modelType">Type of the model object.</param>
        public void RemoveModel(Type modelType)
        {
            m_modelMap.RemoveModel(modelType);
        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public void RemoveModel(IModel model)
        {
            m_modelMap.RemoveModel(model);
        }

        #endregion IModelMap Implementations

        #region IMediatorMap Implementations

        /// <summary>
        /// Registers the mediator.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        /// <param name="mediator">The mediator.</param>
        public void RegisterMediator(IMediator mediator)
        {
            m_mediatorMap.RegisterMediator(mediator);
        }

        /// <summary>
        /// Retrieves the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        /// <returns>The mediator object.</returns>
        public IMediator RetrieveMediator(string mediatorName)
        {
            return m_mediatorMap.RetrieveMediator(mediatorName);
        }

        /// <summary>
        /// Removes the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        public void RemoveMediator(string mediatorName)
        {
            m_mediatorMap.RemoveMediator(mediatorName);
        }

        #endregion IMediatorMap Implementations

        #endregion Public Functions
    }
}