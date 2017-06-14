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
using System.Collections.Generic;

namespace QuickUnity.Extensions
{
    /// <summary>
    /// Extension methods to the <see cref="System.Collections.Generic.List{T}"/>.
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// Adds the unique item.
        /// </summary>
        /// <typeparam name="T">The type of element in List.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        /// <param name="item">The object to be added to the end of the <see cref="System.Collections.Generic.List{T}"/>. </param>
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
        /// Swaps the elements of two index.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        /// <param name="i">The first index of element in the <see cref="System.Collections.Generic.List{T}"/> to swap.</param>
        /// <param name="j">The second index of element in the <see cref="System.Collections.Generic.List{T}"/> to swap.</param>
        public static void Swap<T>(this List<T> source, int i, int j)
        {
            if(i >= 0 && i < source.Count
                && j >= 0 && j < source.Count)
            {
                T temp = source[j];
                source[j] = source[i];
                source[i] = temp;
            }
        }

        /// <summary>
        /// Converts the value of the current <see cref="System.Collections.Generic.List{T}"/> object to its equivalent array string representation.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        /// <returns>The array string representation of the value of <see cref="System.Collections.Generic.List{T}"/> object.</returns>
        public static string ToArrayString<T>(this List<T> source)
        {
            string[] strArr = new string[source.Count];

            for(int i = 0, length = source.Count; i < length; ++i)
            {
                strArr[i] = source[i].ToString();
            }

            return string.Format("{{ {0} }}", string.Join(", ", strArr));
        }

        /// <summary>
        /// Implements the bubble sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void BubbleSort<T>(this List<T> source) where T : IComparable
        {
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            for(int i = 0; i < length; ++i)
            {
                for(int j = 0; j < length - i - 1; j++)
                {
                    if(source[j].CompareTo(source[j + 1]) > 0)
                    {
                        source.Swap(j, j + 1);
                    }
                }
            }
        }

        /// <summary>
        /// Implements the cocktail sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void CocktailSort<T>(this List<T> source) where T : IComparable
        {
            bool found = true;
            int i = 0;
            int left = 0;
            int right = source.Count - 1;

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
                    if(source[i].CompareTo(source[i + 1]) > 0)
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
                    if(source[i - 1].CompareTo(source[i]) > 0)
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
        /// Implements the selection sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void SelectionSort<T>(this List<T> source) where T : IComparable
        {
            int min = 0;
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            for(int i = 0; i < length; ++i)
            {
                min = i;

                for(int j = i + 1; j < length; ++j)
                {
                    if(source[j].CompareTo(source[min]) < 0)
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
        /// Implements the insertion sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void InsertionSort<T>(this List<T> source) where T : IComparable
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
                item = source[i];

                for(j = i -1; j >= 0 && item.CompareTo(source[j]) < 0; j--)
                {
                    source[j + 1] = source[j];
                }

                source[j + 1] = item;
            }
        }

        /// <summary>
        /// Implements the binary insertion sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void BinaryInsertionSort<T>(this List<T> source) where T : IComparable
        {
            int left = 0, middle = 0, right = 0, j = 0;
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            for(int i = 1; i < length; ++i)
            {
                T item = source[i];
                left = 0;
                right = i - 1;

                while(left <= right)
                {
                    middle = (left + right) / 2;

                    if(source[middle].CompareTo(item) > 0)
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
        /// Implements the shell sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void ShellSort<T>(this List<T> source) where T : IComparable
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
                    item = source[i];

                    if(item.CompareTo(source[i - h]) < 0)
                    {
                        for(int j = 0; j < i; j += h)
                        {
                            if(item.CompareTo(source[j]) < 0)
                            {
                                item = source[j];
                                source[j] = source[i];
                                source[i] = item;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Implements the merge sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void MergeSort<T>(this List<T> source) where T : IComparable
        {
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            int middle = length / 2;
            List<T> left = new List<T>(4);
            List<T> right = new List<T>(length - middle);

            for(int i = 0; i < length; ++i)
            {
                if(i < middle)
                {
                    left.Add(source[i]);
                }
                else
                {
                    right.Add(source[i]);
                }
            }

            left.MergeSort();
            right.MergeSort();
            Merge(source, left, right);
        }

        /// <summary>
        /// Implements the heap sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void HeapSort<T>(this List<T> source) where T : IComparable
        {
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            int index = GetMaxIndex(source, length);

            for(int i = length - 1, j = 1; i >= 0; --i, ++j)
            {
                source.Swap(index, i);
                index = GetMaxIndex(source, length - j);
            }
        }

        /// <summary>
        /// Implements the quick sorting algorithm.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The source <see cref="System.Collections.Generic.List{T}"/> object.</param>
        public static void QuickSort<T>(this List<T> source) where T : IComparable
        {
            int length = source.Count;

            if(length <= 1)
            {
                return;
            }

            QuickSort(source, 0, length - 1);
        }

        /// <summary>
        /// Merges two <see cref="System.Collections.Generic.List{T}"/> objects by order.
        /// </summary>
        /// <param name="output">The final output <see cerf="System.Collections.Generic.List{T}"/> object.</param>
        /// <param name="a">The first <see cerf="System.Collections.Generic.List{T}"/> object.</param>
        /// <param name="b">The second <see cref="System.Collections.Generic.List{T}"/> object.</param>
        private static void Merge<T>(List<T> output, List<T> a, List<T> b) where T : IComparable
        {
            if(output == null || a == null || b == null)
            {
                return;
            }

            T[] resultArr = new T[a.Count + b.Count];
            int i = 0, j = 0, k = 0;
            int lengthA = a.Count;
            int lengthB = b.Count;

            while(i < lengthA && j < lengthB)
            {
                if(a[i].CompareTo(b[j]) < 0)
                {
                    resultArr[k++] = a[i++];
                }
                else
                {
                    resultArr[k++] = b[j++];
                }
            }

            while(i < lengthA)
            {
                resultArr[k++] = a[i++];
            }

            while(j < lengthB)
            {
                resultArr[k++] = b[j++];
            }

            output.Clear();
            output.AddRange(resultArr);
        }

        /// <summary>
        /// Gets the index of maximum in the <see cref="System.Collections.Generic.List{T}"/>.
        /// </summary>
        /// <param name="source">The <see cref="System.Collections.Generic.List{T}"/> object to search.</param>
        /// <param name="length">The length to limit searching.</param>
        /// <returns>The index of maximum in the <see cref="System.Collections.Generic.List{T}"/>.</returns>
        private static int GetMaxIndex<T>(List<T> source, int length) where T : IComparable
        {
            int index = 0;

            if(source.Count > 1)
            {
                for(int i = 0; i < length; ++i)
                {
                    if(source[i].CompareTo(source[index]) > 0)
                    {
                        index = i;
                    }
                }
            }

            return index;
        }

        /// <summary>
        /// Implement quick sorting algorithm.
        /// </summary>
        /// <param name="list">The <see cref="System.Collections.Generic.List{T}"/> object.</param>
        /// <param name="low">The start index for sorting.</param>
        /// <param name="high">The end index for sorting.</param>
        private static void QuickSort<T>(List<T> list, int low, int high) where T : IComparable
        {
            if(low >= high)
            {
                return;
            }

            int i = low + 1, j = high;

            while(true)
            {
                while(list[j].CompareTo(list[low]) > 0)
                {
                    j--;
                }

                while(list[i].CompareTo(list[low]) < 0 && i < j)
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