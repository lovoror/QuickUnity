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
using System.Collections;

namespace QuickUnity.Tasks
{
    /// <summary>
    /// The interface definition for the Task component.
    /// </summary>
    public interface ITask : IEventDispatcher
    {
        /// <summary>
        /// Gets the global unique identifier.
        /// </summary>
        /// <value>
        /// The global unique identifier.
        /// </value>
        Guid guid
        {
            get;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [removed from TaskManager when stop].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [removed from TaskManager when stop]; otherwise, <c>false</c>.
        /// </value>
        bool removedWhenStop
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITask"/> is running.
        /// </summary>
        /// <value><c>true</c> if running; otherwise, <c>false</c>.</value>
        bool running
        {
            get;
        }

        /// <summary>
        /// Gets the state of the task.
        /// </summary>
        /// <value>The state of the task.</value>
        TaskState taskState
        {
            get;
        }

        /// <summary>
        /// Gets the function need to be executed.
        /// </summary>
        /// <value>The routine.</value>
        IEnumerator routine
        {
            get;
        }

        /// <summary>
        /// The wrapper of routine.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        IEnumerator RoutineWrapper();

        /// <summary>
        /// Starts this task.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops this task.
        /// </summary>
        void Stop();

        /// <summary>
        /// Pauses this task.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes this task.
        /// </summary>
        void Resume();
    }
}