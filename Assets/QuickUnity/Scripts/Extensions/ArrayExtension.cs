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
using QuickUnity.Extensions.Collections;
using QuickUnity.Utilities;

namespace QuickUnity.Extensions
{
	/// <summary>
	/// Extension methods to the <see cref="System.Array"/>.
	/// </summary>
	public static class ArrayExtension
	{
		/// <summary>
		/// Swaps a element in one index with another element in another index.
		/// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Array"/>.</typeparam>
		/// <param name="source">The <see cref="System.Array"/> to swap elements. </param>
		/// <param name="a">The first index of element in the <see cref="System.Array"/> to swap. </param>
		/// <param name="b">The second index of element in the <see cref="System.Array"/> to swap. </param>
		public static void Swap<T>(this T[] source, int a, int b)
		{
			(source as IList).Swap(a, b);
		}

		/// <summary>
        /// Implements the bubble sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The <see cref="System.Array"/> to be sorted. </param>
        public static void BubbleSort<T>(this T[] source) where T : IComparable
		{
			SortUtility.BubbleSort<T>(source);
		}

		/// <summary>
        /// Implements the cocktail sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The source <see cref="System.Array"/> object. </param>
        public static void CocktailSort<T>(this T[] source) where T : IComparable
        {
            SortUtility.CocktailSort<T>(source);
        }

		/// <summary>
        /// Implements the selection sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The source <see cref="System.Array"/> object. </param>
		public static void SelectionSort<T>(this T[] source) where T : IComparable
        {
            SortUtility.SelectionSort<T>(source);
        }

		/// <summary>
        /// Implements the insertion sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The source <see cref="System.Array"/> object. </param>
		public static void InsertionSort<T>(this T[] source) where T : IComparable
		{
			SortUtility.InsertionSort<T>(source);
		}

		/// <summary>
        /// Implements the binary insertion sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The source <see cref="System.Array"/> object. </param>
		public static void BinaryInsertionSort<T>(this T[] source) where T : IComparable
        {
            SortUtility.BinaryInsertionSort<T>(source);
        }

		/// <summary>
        /// Implements the shell sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The source <see cref="System.Array"/> object. </param>
		public static void ShellSort<T>(this T[] source) where T : IComparable
		{
			SortUtility.ShellSort<T>(source);
		}

		/// <summary>
        /// Implements the shell sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The source <see cref="System.Array"/> object. </param>
		public static void MergeSort<T>(this T[] source) where T : IComparable
		{
			SortUtility.MergeSort<T>(source);
		}

		/// <summary>
        /// Implements the heap sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The source <see cref="System.Array"/> object. </param>
        public static void HeapSort<T>(this T[] source) where T : IComparable
        {
            SortUtility.HeapSort(source);
        }

		/// <summary>
        /// Implements the quick sorting algorithm to the <see cref="System.Array"/>. 
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="System.Array"/>. </typeparam>
        /// <param name="source">The source <see cref="System.Array"/> object. </param>
        public static void QuickSort<T>(this T[] source) where T : IComparable
        {
            SortUtility.QuickSort(source);
        }
	}
}