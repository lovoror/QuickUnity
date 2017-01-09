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

using System.Collections.Generic;

namespace QuickUnity.Extensions
{
    /// <summary>
    /// Extension methods collection for System.Collections.Generic.List.
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Adds the unique item.
        /// </summary>
        /// <typeparam name="T">The type of element in List.</typeparam>
        /// <param name="source">The source List object.</param>
        /// <param name="item">The item.</param>
        public static void AddUnique<T>(this List<T> source, T item)
        {
            if (!source.Contains(item))
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Adds the range unique collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="collection">The collection.</param>
        public static void AddRangeUnique<T>(this List<T> source, IEnumerable<T> collection)
        {
            List<T> newCollection = new List<T>();

            foreach (T item in collection)
            {
                if (!source.Contains(item) && !newCollection.Contains(item))
                {
                    newCollection.Add(item);
                }
            }

            source.AddRange(newCollection);
        }

        /// <summary>
        /// Removes a range of elements from the List.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="collection">The collection.</param>
        public static void RemoveRange<T>(this List<T> source, ICollection<T> collection)
        {
            foreach (T item in collection)
            {
                source.Remove(item);
            }
        }
    }
}