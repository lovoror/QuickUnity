﻿/*
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

namespace QuickUnity.MVCS
{
    /// <summary>
    /// Abstract MVCS <c>IMediatorMap</c> implementation.
    /// </summary>
    public class MediatorMap : DataMap<string, IMediator>, IMediatorMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediatorMap"/> class.
        /// </summary>
        public MediatorMap()
            : base()
        {
        }

        #region Public Functions

        #region IMediatorMap Implementations

        /// <summary>
        /// Registers the mediator.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        /// <param name="mediator">The mediator.</param>
        public void RegisterMediator(IMediator mediator)
        {
            if (mediator != null)
                Register(mediator.mediatorName, mediator);
        }

        /// <summary>
        /// Retrieves the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        /// <returns>The mediator object.</returns>
        public IMediator RetrieveMediator(string mediatorName)
        {
            if (!string.IsNullOrEmpty(mediatorName))
                return Retrieve(mediatorName);

            return null;
        }

        /// <summary>
        /// Removes the mediator.
        /// </summary>
        /// <param name="mediatorName">Name of the mediator.</param>
        public void RemoveMediator(string mediatorName)
        {
            if (!string.IsNullOrEmpty(mediatorName))
                Remove(mediatorName);
        }

        #endregion IMediatorMap Implementations

        #endregion Public Functions
    }
}