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

using System;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// A base <c>IAsyncCommand</c> implementation.
    /// </summary>
    public abstract class AsyncCommand : Command, IAsyncCommand
    {
        /// <summary>
        /// The executed callback function.
        /// </summary>
        protected Action m_executedCallback;

        #region Public Functions

        /// <summary>
        /// Sets the executed callback.
        /// </summary>
        /// <param name="callback">The callback function.</param>
        public void SetExecutedCallback(Action callback)
        {
            m_executedCallback = callback;
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        public override void Execute()
        {
            base.Execute();

            if (m_executedCallback != null)
            {
                m_executedCallback.Invoke();
            }
        }

        #endregion Public Functions
    }
}