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

using System.Collections.Generic;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// A base <c>IMacroCommand</c> implementation that executes other <c>ICommand</c> s asynchronously.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.ICommand"/>
    /// <seealso cref="QuickUnity.Patterns.IAsyncMacroCommand"/>
    public class AsyncMacroCommand : ICommand, IAsyncMacroCommand
    {
        /// <summary>
        /// The command queue.
        /// </summary>
        protected Queue<IAsyncCommand> m_commandQueue;

        /// <summary>
        /// Initializes a new instance of the <see cref="MacroCommand"/> class.
        /// </summary>
        public AsyncMacroCommand()
        {
            m_commandQueue = new Queue<IAsyncCommand>();
            Initialize();
        }

        #region ICommand Interface

        /// <summary>
        /// Executes this command.
        /// </summary>
        public void Execute()
        {
            ExecuteNextSubCommand();
        }

        #endregion ICommand Interface

        #region IAsyncMacroCommand Interface

        /// <summary>
        /// Adds sub command.
        /// </summary>
        /// <param name="subCommand">The sub command.</param>
        public void AddSubCommand(IAsyncCommand subCommand)
        {
            if (m_commandQueue != null)
            {
                m_commandQueue.Enqueue(subCommand);
            }
        }

        #endregion IAsyncMacroCommand Interface

        #region Protected Functions

        /// <summary>
        /// Initializes this command.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        #endregion Protected Functions

        #region Private Functions

        /// <summary>
        /// Executes the next sub command.
        /// </summary>
        private void ExecuteNextSubCommand()
        {
            if (m_commandQueue != null && m_commandQueue.Count > 0)
            {
                IAsyncCommand subCommand = m_commandQueue.Dequeue();
                subCommand.SetExecutedCallback(ExecuteNextSubCommand);
                subCommand.Execute();
            }
        }

        #endregion Private Functions
    }
}