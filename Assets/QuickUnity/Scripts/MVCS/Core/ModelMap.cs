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
    /// An abstract <c>IModelMap</c> implementation.
    /// </summary>
    public class ModelMap : DataMap<Type, IModel>, IModelMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelMap"/> class.
        /// </summary>
        public ModelMap()
            : base()
        {
        }

        #region Public Functions

        #region IModelMap Implementations

        /// <summary>
        /// Registers the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public void RegisterModel(IModel model)
        {
            Register(model.GetType(), model);
        }

        /// <summary>
        /// Retrieves the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns>The model object.</returns>
        public IModel RetrieveModel(Type modelType)
        {
            return Retrieve(modelType);
        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        public void RemoveModel(Type modelType)
        {
            Remove(modelType);
        }

        /// <summary>
        /// Removes the model.
        /// </summary>
        /// <param name="model">The model object.</param>
        public void RemoveModel(IModel model)
        {
            Remove(model);
        }

        #endregion IModelMap Implementations

        #endregion Public Functions
    }
}