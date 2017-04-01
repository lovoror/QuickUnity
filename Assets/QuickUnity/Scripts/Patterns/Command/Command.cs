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

namespace QuickUnity.Patterns
{
    /// <summary>
    /// A base <c>ICommand</c> implementation.
    /// </summary>
    public abstract class Command : EventDispatcher, ICommand
    {
        /// <summary>
        /// The data for this command.
        /// </summary>
        protected object m_data;

        /// <summary>
        /// Sets the data for this command.
        /// </summary>
        /// <value>The data for this command.</value>
        public object data
        {
            set
            {
                m_data = value;
            }
        }

        /// <summary>
        /// The context object of this command.
        /// </summary>
        protected object m_context;

        /// <summary>
        /// Sets the context object of this command.
        /// </summary>
        /// <value>The context object of this command.</value>
        public object context
        {
            set
            {
                m_context = value;
            }
        }

        #region Constrcutors

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        public Command()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="context">The context.</param>
        public Command(object data, object context = null)
        {
            m_data = data;
            m_context = context;
        }

        #endregion Constrcutors

        #region Public Functions

        /// <summary>
        /// Executes this command.
        /// </summary>
        public virtual void Execute()
        {
        }

        #endregion Public Functions
    }
}