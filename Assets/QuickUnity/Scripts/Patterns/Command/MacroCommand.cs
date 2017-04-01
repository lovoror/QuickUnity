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
using System.Collections.Generic;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// A base <c>IMacroCommand</c> implementation that executes other <c>ICommand</c> s.
    /// </summary>
    public abstract class MacroCommand : EventDispatcher, ICommand, IMacroCommand
    {
        /// <summary>
        /// The command queue.
        /// </summary>
        protected Queue<ICommand> m_commandQueue;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MacroCommand"/> class.
        /// </summary>
        public MacroCommand()
        {
            m_commandQueue = new Queue<ICommand>();
            Initialize();
        }

        #endregion Constructors

        #region Public Functions

        /// <summary>
        /// Adds sub command.
        /// </summary>
        /// <param name="subCommand">The sub command.</param>
        public void AddSubCommand(ICommand subCommand)
        {
            if (m_commandQueue != null)
            {
                m_commandQueue.Enqueue(subCommand);
            }
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        public void Execute()
        {
            if (m_commandQueue != null)
            {
                while (m_commandQueue.Count > 0)
                {
                    ICommand subCommand = m_commandQueue.Dequeue();
                    subCommand.Execute();
                }
            }
        }

        #endregion Public Functions

        #region Protected Functions

        /// <summary>
        /// Initializes this command.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        #endregion Protected Functions
    }
}