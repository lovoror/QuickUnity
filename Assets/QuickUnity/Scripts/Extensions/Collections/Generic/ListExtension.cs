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

using System;
using System.Collections;
using System.Collections.Generic;
using QuickUnity.Utilities;

namespace QuickUnity.Extensions.Collections.Generic
{
    /// <summary>
    /// Extension methods to the <see cref="System.Collections.Generic.List{T}"/>.
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Adds an unique item to the <see cref="System.Collections.Generic.List{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of element in List.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        /// <param name="item">The object to be added to the end of the <see cref="System.Collections.Generic.List{T}"/>. </param>
        public static void AddUnique<T>(this List<T> source, T item)
        {
            (source as ICollection<T>).AddUnique(item);
        }

        /// <summary>
        /// Adds the range unique collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        /// <param name="collection">The collection whose elements should be added to the end of the <see cref="System.Collections.Generic.List{T}"/>. </param>
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
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        /// <param name="collection">The collection whose elements should be removed from the <see cref="System.Collections.Generic.List{T}"/>. </param>
        public static void RemoveRange<T>(this List<T> source, IEnumerable<T> collection)
        {
            foreach (T item in collection)
            {
                source.Remove(item);
            }
        }

        /// <summary>
		/// Swaps a element in one index with another element in another index.
		/// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The <see cref="System.Collections.Generic.List{T}"/> to swap elements. </param>
		/// <param name="a">The first index of element in the <see cref="System.Collections.Generic.List{T}"/> to swap. </param>
		/// <param name="b">The second index of element in the <see cref="System.Collections.Generic.List{T}"/> to swap. </param>
        public static void Swap<T>(this List<T> source, int a, int b) where T : IComparable
        {
            (source as IList).Swap(a, b);
        }

        /// <summary>
        /// Converts the value of the current <see cref="System.Collections.Generic.List{T}"/> object to its equivalent array string representation.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        /// <returns>The array string representation of the value of <see cref="System.Collections.Generic.List{T}"/> object.</returns>
        public static string ToArrayString<T>(this List<T> source)
        {
            return (source as IList).ToArrayString();
        }

        /// <summary>
        /// Implements the bubble sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void BubbleSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.BubbleSort<T>(source);
        }

        /// <summary>
        /// Implements the cocktail sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void CocktailSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.CocktailSort<T>(source);
        }

        /// <summary>
        /// Implements the selection sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void SelectionSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.SelectionSort<T>(source);
        }

        /// <summary>
        /// Implements the insertion sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void InsertionSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.InsertionSort<T>(source);
        }

        /// <summary>
        /// Implements the binary insertion sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void BinaryInsertionSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.BinaryInsertionSort<T>(source);
        }

        /// <summary>
        /// Implements the shell sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void ShellSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.ShellSort<T>(source);
        }

        /// <summary>
        /// Implements the merge sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void MergeSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.MergeSort<T>(source);
        }

        /// <summary>
        /// Implements the heap sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void HeapSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.HeapSort(source);
        }

        /// <summary>
        /// Implements the quick sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void QuickSort<T>(this List<T> source) where T : IComparable
        {
            SortUtility.QuickSort(source);
        }
    }
}