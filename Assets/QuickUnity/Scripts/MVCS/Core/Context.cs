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

namespace QuickUnity.MVCS
{
    /// <summary>
    /// Abstract MVCS <c>IContext</c> implementation.
    /// </summary>
    public class Context : Module, IContext, IModuleMap
    {
        /// <summary>
        /// The module map.
        /// </summary>
        protected IModuleMap m_moduleMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="Context"/> class.
        /// </summary>
        public Context()
            : base()
        {
            m_moduleMap = new ModuleMap();
        }

        #region Public Functions

        #region IModuleMap Implementations

        /// <summary>
        /// Registers the module.
        /// </summary>
        /// <param name="module">The module.</param>
        public void RegisterModule(IModule module)
        {
            m_moduleMap.RegisterModule(module);
        }

        /// <summary>
        /// Retrieves the module.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        /// <returns>
        /// The module object.
        /// </returns>
        public IModule RetrieveModule(Type moduleType)
        {
            return m_moduleMap.RetrieveModule(moduleType);
        }

        /// <summary>
        /// Removes the module.
        /// </summary>
        /// <param name="moduleType">Type of the module.</param>
        public void RemoveModule(Type moduleType)
        {
            m_moduleMap.RemoveModule(moduleType);
        }

        #endregion IModuleMap Implementations

        #region IModelMap Implementations

        /// <summary>
        /// Registers the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public override void RegisterModel(IModel model)
        {
            base.RegisterModel(model);

            model.ContextEventDispatcher = this;
        }

        #endregion IModelMap Implementations

        #endregion Public Functions
    }
}