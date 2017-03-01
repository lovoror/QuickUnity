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
using System.Reflection;

namespace QuickUnity.Patterns
{
    /// <summary>
    /// Template class for singleton pattern.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="QuickUnity.Events.EventDispatcher"/>
    public abstract class Singleton<T> : EventDispatcher
    {
        /// <summary>
        /// Used for locking the instance calls.
        /// </summary>
        private static readonly object s_syncRoot = new object();

        /// <summary>
        /// The singleton instance.
        /// </summary>
        private static T s_instance = default(T);

        /// <summary>
        /// A value indicating whether this <see cref="Singleton{T}"/> instance is instantiated.
        /// </summary>
        private static bool s_instantiated = false;

        /// <summary>
        /// Gets the static instance.
        /// </summary>
        /// <value>The static instance.</value>
        public static T instance
        {
            get
            {
                if (!s_instantiated)
                {
                    lock (s_syncRoot)
                    {
                        if (!s_instantiated)
                        {
                            Type type = typeof(T);
                            ConstructorInfo ctor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                                null, new Type[0], new ParameterModifier[0]);
                            s_instance = (T)ctor.Invoke(new object[0]);
                            s_instantiated = true;
                        }
                    }
                }

                return s_instance;
            }
        }
    }
}