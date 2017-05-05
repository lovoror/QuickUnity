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

using System.Collections.ObjectModel;

namespace QuickUnity.Extensions
{
    /// <summary>
    /// Extension methods to the <see cref="System.Collections.ObjectModel.Collection{T}"/>.
    /// </summary>
    public static class CollectionExtension
    {
        /// <summary>
        /// Adds an unique object for the <see cref="System.Collections.ObjectModel.Collection{T}"/>
        /// to the end of the <see cref="System.Collections.ObjectModel.Collection{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the collection.</typeparam>
        /// <param name="source">
        /// The source <see cref="System.Collections.ObjectModel.Collection{T}"/> object.
        /// </param>
        /// <param name="item">
        /// The object to be added to the end of the <see
        /// cref="System.Collections.ObjectModel.Collection{T}"/>. The value can be null for
        /// reference types.
        /// </param>
        public static void AddUnique<T>(this Collection<T> source, T item)
        {
            if (!source.Contains(item))
            {
                source.Add(item);
            }
        }
    }
}