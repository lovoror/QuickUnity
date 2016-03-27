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

namespace QuickUnity.FSM
{
    /// <summary>
    /// The interface definition for the FSMStateAction component.
    /// </summary>
    public interface IFSMStateAction
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IFSMStateAction"/> is enabled.
        /// </summary>
        /// <value>
        ///   <c>true</c> if enabled; otherwise, <c>false</c>.
        /// </value>
        bool enabled
        {
            get;
            set;
        }

        /// <summary>
        /// Called when [enter].
        /// </summary>
        void OnEnter();

        /// <summary>
        /// Called when [fixed update].
        /// </summary>
        void OnFixedUpdate();

        /// <summary>
        /// Called when [update].
        /// </summary>
        void OnUpdate();

        /// <summary>
        /// Called when [late update].
        /// </summary>
        void OnLateUpdate();

        /// <summary>
        /// Called when [exit].
        /// </summary>
        void OnExit();
    }
}