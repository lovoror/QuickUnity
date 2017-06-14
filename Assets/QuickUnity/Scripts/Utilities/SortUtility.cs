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
	}
}