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
using System.Collections.Generic;

namespace QuickUnity.Commands
{
    /// <summary>
    /// A command queue implement.
    /// </summary>
    public class CommandQueue : EventDispatcher
    {
        /// <summary>
        /// The queue object.
        /// </summary>
        protected Queue<ICommand> m_queue;

        /// <summary>
        /// The total commands count.
        /// </summary>
        protected int m_commandsCount = 0;

        /// <summary>
        /// The executed commands count.
        /// </summary>
        protected int m_executedCommandsCount = 0;

        /// <summary>
        /// The progress of commands executing.
        /// </summary>
        protected float m_progress;

        /// <summary>
        /// Gets the progress of commands executing.
        /// </summary>
        /// <value>
        /// The progress.
        /// </value>
        public float progress
        {
            get { return m_progress; }
        }

        /// <summary>
        /// if set to <c>true</c> [stop when error].
        /// </summary>
        protected bool m_stopWhenError = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandQueue" /> class.
        /// </summary>
        /// <param name="stopWhenError">if set to <c>true</c> [stop when error].</param>
        public CommandQueue(bool stopWhenError = false)
        {
            m_queue = new Queue<ICommand>();
            m_stopWhenError = stopWhenError;
        }

        #region API

        /// <summary>
        /// Start to executes the commands.
        /// </summary>
        public void Start()
        {
            DispatchEvent(new CommandQueueEvent(CommandQueueEvent.Start));
            ExecuteCommand();
        }

        /// <summary>
        /// Adds the command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void AddCommand(ICommand command)
        {
            if (command != null && m_queue != null)
            {
                m_queue.Enqueue(command);
                m_commandsCount++;
            }
        }

        #endregion API

        #region Protected Functions

        /// <summary>
        /// Executes the command that is dequeued from queue.
        /// </summary>
        protected void ExecuteCommand()
        {
            if (m_queue == null)
                return;

            if (m_queue.Count > 0)
            {
                ICommand command = m_queue.Dequeue();

                if (command != null)
                {
                    command.AddEventListener(CommandEvent.Error, OnCommandError);
                    command.AddEventListener(CommandEvent.Executed, OnCommandExecuted);
                    command.Execute();
                }
            }
            else
            {
                DispatchEvent(new CommandQueueEvent(CommandQueueEvent.Complete));
                Reset();
            }
        }

        /// <summary>
        /// Execute next command.
        /// </summary>
        protected void Next()
        {
            m_executedCommandsCount++;

            // Dispatch progress event.
            CommandQueueEvent evt = new CommandQueueEvent(CommandQueueEvent.Progress);
            evt.progress = (float)m_executedCommandsCount / m_commandsCount;
            DispatchEvent(evt);

            // Execute the next command.
            ExecuteCommand();
        }

        /// <summary>
        /// Resets.
        /// </summary>
        protected void Reset()
        {
            m_commandsCount = 0;
            m_executedCommandsCount = 0;
        }

        #endregion Protected Functions

        #region Private Functions

        /// <summary>
        /// Called when [command got error].
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void OnCommandError(QuickUnity.Events.Event evt)
        {
            RemoveCommandEventListeners(evt);

            if (!m_stopWhenError)
                Next();
            else
                DispatchEvent(new CommandQueueEvent(CommandQueueEvent.Interrupt));
        }

        /// <summary>
        /// Called when [command has executed].
        /// </summary>
        /// <param name="evt">The evt.</param>
        private void OnCommandExecuted(QuickUnity.Events.Event evt)
        {
            RemoveCommandEventListeners(evt);

            Next();
        }

        /// <summary>
        /// Removes the command event listeners.
        /// </summary>
        /// <param name="evt">The event object.</param>
        private void RemoveCommandEventListeners(QuickUnity.Events.Event evt)
        {
            CommandEvent commandEvent = (CommandEvent)evt;
            ICommand command = (ICommand)commandEvent.target;

            if (command != null)
                command.RemoveEventListenersByTarget(this);
        }

        #endregion Private Functions
    }
}