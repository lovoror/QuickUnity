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

namespace QuickUnity.Utilities
{
	/// <summary>
	/// 
	/// </summary>
	public static class SortUtility
	{
		/// <summary>
		/// Implements the bubble sorting algorithm to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Collections.IList"/>. </typeparam>
		/// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
		public static void BubbleSort<T>(IList source) where T : IComparable
		{
			int length = source.Count;
			T item = default(T);

            if(length <= 1)
            {
                return;
            }

            for(int i = 0; i < length; ++i)
            {
                for(int j = 0; j < length - i - 1; j++)
                {
					item = (T)source[j];

                    if(item.CompareTo(source[j + 1]) > 0)
                    {
                        source.Swap(j, j + 1);
                    }
                }
            }
		}

		/// <summary>
		/// Implements the cocktail sorting algorithm to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Collections.IList"/>. </typeparam>
		/// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
		public static void CocktailSort<T>(IList source) where T : IComparable
		{
            bool found = true;
            int i = 0;
            int left = 0;
            int right = source.Count - 1;
			T item = default(T);

            if(right == 0)
            {
                return;
            }

            while(left < right)
            {
                found = false;

                // Find to right.
                for(i = left; i < right; ++i)
                {
					item = (T)source[i];

                    if(item.CompareTo(source[i + 1]) > 0)
                    {
                        source.Swap(i, i + 1);
                        found = true;
                    }
                }

                if(!found)
                {
                    break;
                }

                right--;

                // Find to left.
                for(i = right; i > left; --i)
                {
					item = (T)source[i - 1];

                    if(item.CompareTo(source[i]) > 0)
                    {
                        source.Swap(i, i - 1);
                        found = true;
                    }
                }

                if(!found)
                {
                    break;
                }

                left++;
            }
        }

		/// <summary>
		/// Implements the selection sorting algorithm to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Collections.IList"/>. </typeparam>
		/// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
		public static void SelectionSort<T>(IList source) where T : IComparable
		{
            int min = 0;
            int length = source.Count;
			T item = default(T);

            if(length <= 1)
            {
                return;
            }

            for(int i = 0; i < length; ++i)
            {
                min = i;

                for(int j = i + 1; j < length; ++j)
                {
					item = (T)source[j];

                    if(item.CompareTo(source[min]) < 0)
                    {
                        min = j;
                    }
                }

                if(min != i)
                {
                    source.Swap(min, i);
                }
            }
        }

		/// <summary>
		/// Implements the insertion sorting algorithm to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Collections.IList"/>. </typeparam>
		/// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
		public static void InsertionSort<T>(IList source) where T : IComparable
		{
            T item = default(T);
            int j = 0;
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            for(int i = 1; i < length; ++i)
            {
                item = (T)source[i];

                for(j = i -1; j >= 0 && (item as IComparable).CompareTo(source[j]) < 0; j--)
                {
                    source[j + 1] = source[j];
                }

                source[j + 1] = item;
            }
        }

		/// <summary>
		/// Implements the binary insertion sorting algorithm to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Collections.IList"/>. </typeparam>
		/// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
		public static void BinaryInsertionSort<T>(IList source) where T : IComparable
		{
            int left = 0, middle = 0, right = 0, j = 0;
            int length = source.Count;
			T item = default(T);
			T middleItem = default(T);

            if(length <= 1)
            {
                return;
            }

            for(int i = 1; i < length; ++i)
            {
                item = (T)source[i];
                left = 0;
                right = i - 1;

                while(left <= right)
                {
                    middle = (left + right) / 2;
					middleItem = (T)source[middle];

                    if(middleItem.CompareTo(item) > 0)
                    {
                        right = middle - 1;
                    }
                    else
                    {
                        left = middle + 1;
                    }
                }

                for(j = i - 1; j >= left; --j)
                {
                    source[j + 1] = source[j];
                }

                source[left] = item;
            }
        }

		/// <summary>
		/// Implements the shell sorting algorithm to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Collections.IList"/>. </typeparam>
		/// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
		public static void ShellSort<T>(IList source) where T : IComparable
		{
            T item = default(T);
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            for(int h = length / 2; h > 0; h /= 2)
            {
                for(int i = h; i < length; ++i)
                {
                    item = (T)source[i];

                    if(item.CompareTo(source[i - h]) < 0)
                    {
                        for(int j = 0; j < i; j += h)
                        {
                            if(item.CompareTo(source[j]) < 0)
                            {
                                item = (T)source[j];
                                source[j] = source[i];
                                source[i] = item;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
		/// Implements the merge sorting algorithm to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <typeparam name="T">The type of elements in the <see cref="System.Collections.IList"/>. </typeparam>
		/// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
        public static void MergeSort<T>(IList source) where T : IComparable
        {
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            int middle = length / 2;
            IList left = new T[middle];
            IList right = new T[length - middle];

            for(int i = 0; i < length; ++i)
            {
                if(i < middle)
                {
                    left[i] = (T)source[i];
                }
                else
                {
                    right[i - middle] = (T)source[i];
                }
            }

            MergeSort<T>(left);
            MergeSort<T>(right);
            IList result = MergeList<T>(left, right);
            result.CopyTo(source);
        }

        /// <summary>
		/// Implements the heap sorting algorithm to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
        public static void HeapSort(IList source)
        {
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            int index = source.IndexOfMax(length);

            for(int i = length - 1, j = 1; i >= 0; --i, ++j)
            {
                source.Swap(index, i);
                index = source.IndexOfMax(length - j);
            }
        }

        /// <summary>
        /// Implements the quick sorting algorithm.
        /// </summary>
        /// <param name="source">The <see cref="System.Collections.IList"/> to be sorted. </param>
        public static void QuickSort(IList source)
        {
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            QuickSort(source, 0, length - 1);
        }

        /// <summary>
        /// Merges two <see cref="System.Collections.IList"/> by order.
        /// </summary>
        /// <param name="a">The first <see cerf="System.Collections.IList"/>. </param>
        /// <param name="b">The second <see cref="System.Collections.IList"/> object. </param>
        /// <returns>The sorted <see cref="System.Collections.IList"/>. </returns>
        private static IList MergeList<T>(IList a, IList b) where T : IComparable
        {
            if(a == null || b == null)
            {
                return null;
            }

            int lengthA = a.Count;
            int lengthB = b.Count;
            int i = 0, j = 0, k = 0;
            T item = default(T);
            T[] resultArr = new T[lengthA + lengthB];

            while(i < lengthA && j < lengthB)
            {
                item = (T)a[i];

                if(item.CompareTo(b[j]) < 0)
                {
                    resultArr[k++] = (T)a[i++];
                }
                else
                {
                    resultArr[k++] = (T)b[j++];
                }
            }

            while(i < lengthA)
            {
                resultArr[k++] = (T)a[i++];
            }

            while(j < lengthB)
            {
                resultArr[k++] = (T)b[j++];
            }

            return resultArr;
        }

        /// <summary>
        /// Implement quick sorting algorithm. 
        /// </summary>
        /// <param name="list">The <see cref="System.Collections.IList"/> to be sorted. </param>
        /// <param name="low">The start index for sorting. </param>
        /// <param name="high">The end index for sorting. </param>
        private static void QuickSort(IList list, int low, int high)
        {
            if(low >= high)
            {
                return;
            }

            int i = low + 1, j = high;

            while(true)
            {
                while((list[j] as IComparable).CompareTo(list[low]) > 0)
                {
                    j--;
                }

                while((list[i] as IComparable).CompareTo(list[low]) < 0 && i < j)
                {
                    i++;
                }

                if(i >= j)
                {
                    break;
                }

                list.Swap(i, j);
                i++;
                j--;
            }

            if(j != low)
            {
                list.Swap(low, j);
            }

            QuickSort(list, j + 1, high);
            QuickSort(list, low, j - 1);
        }
	}
}