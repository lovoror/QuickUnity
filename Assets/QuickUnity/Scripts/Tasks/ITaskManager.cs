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

namespace QuickUnity.Tasks
{
    /// <summary>
    /// The interface definition for the TaskManager object.
    /// </summary>
    /// <seealso cref="QuickUnity.Events.IEventDispatcher"/>
    public interface ITaskManager : IEventDispatcher
    {
        /// <summary>
        /// Contains the task.
        /// </summary>
        /// <param name="task">The task object.</param>
        /// <returns><c>true</c> if task is found, <c>false</c> otherwise.</returns>
        bool ContainsTask(ITask task);

        /// <summary>
        /// Adds the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="autoStart">if set to <c>true</c> [automatic start].</param>
        void AddTask(ITask task, bool autoStart = true);

        /// <summary>
        /// Removes the task.
        /// </summary>
        /// <param name="task">The task.</param>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        /// <returns><c>true</c> if task is successfully removed; <c>false</c> otherwise.</returns>
        bool RemoveTask(ITask task, bool autoStop = true);

        /// <summary>
        /// Removes all tasks.
        /// </summary>
        /// <param name="autoStop">if set to <c>true</c> [automatic stop].</param>
        void RemoveAllTasks(bool autoStop = true);

        /// <summary>
        /// Starts all tasks.
        /// </summary>
        void StartAllTasks();

        /// <summary>
        /// Pauses all tasks.
        /// </summary>
        void PauseAllTasks();

        /// <summary>
        /// Resumes all tasks.
        /// </summary>
        void ResumeAllTasks();

        /// <summary>
        /// Stops all tasks.
        /// </summary>
        void StopAllTasks();
    }
}