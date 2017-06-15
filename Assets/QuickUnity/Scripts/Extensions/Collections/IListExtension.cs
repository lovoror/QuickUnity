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

namespace QuickUnity.Extensions.Collections
{
	/// <summary>
	/// Extension methods to the <see cref="System.Collections.IList"/>.
	/// </summary>
	public static class IListExtension
	{
		/// <summary>
		/// Adds a unique item to the <see cref="System.Collections.IList"/>.
		/// </summary>
		/// <param name="source">A <see cref="System.Collections.IList"/> to add unique item.</param>
		/// <param name="value">The object to add to the <see cref="System.Collections.IList"/>.</param>
		/// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection.</returns>
		public static int AddUnique(this IList source, object value)
		{
			int position = -1;

			if (!source.Contains(value))
            {
                position = source.Add(value);
            }

			return position;
		}

		/// <summary>
		/// Swaps a element in one index with another element in another index.
		/// </summary>
		/// <param name="source">The <see cref="System.Collections.IList"/> to swap elements. </param>
		/// <param name="a">The first index of element in the <see cref="System.Collections.IList"/> to swap. </param>
		/// <param name="b">The second index of element in the <see cref="System.Collections.IList"/> to swap. </param>
		public static void Swap(this IList source, int a, int b)
        {
            if(a >= 0 && a < source.Count
                && b >= 0 && b < source.Count)
            {
                object temp = source[b];
                source[b] = source[a];
                source[a] = temp;
            }
        }

		/// <summary>
		/// Copies all the elements of the <see cref="System.Collections.IList"/> to the specified <see cref="System.Collections.IList"/>. 
		/// </summary>
		/// <param name="source">The source object of the <see cref="System.Collections.IList"/>. </param>
		/// <param name="target">The target object of the <see cref="System.Collections.IList"/>. </param>
		///  <param name="index">A 32-bit integer that represents the index in <see cref="System.Collections.IList"/> at which copying begins. </param>
		public static void CopyTo(this IList source, IList target, int index = 0)
		{
			int sourceLength = source.Count;
			int targetLength = target.Count;

			index = Math.Max(Math.Min(sourceLength - 1, index), 0);

			for(int i = index; i < sourceLength; ++i)
			{
				int j = i - index;

				if(j < targetLength)
				{
					target[j] = source[i];
				}
			}
		}

		/// <summary>
        /// Searches for the maximum object and returns the index of object in the <see cref="System.Collections.IList"/>.  
        /// </summary>
        /// <param name="source">The <see cref="System.Collections.IList"/> to search. </param>
        /// <param name="count">The number of objects in the section to search. </param>
        /// <returns>The index of maximum object in the <see cref="System.Collections.IList"/>. </returns>
		public static int IndexOfMax(this IList source, int count)
		{
            int index = -1;
			int length = source.Count;

			count = Math.Max(Math.Min(count, source.Count), 0);

            if(length > 1)
            {
				index = 0;

                for(int i = 0; i < count; ++i)
                {
                    if((source[i] as IComparable).CompareTo(source[index]) > 0)
                    {
                        index = i;
                    }
                }
            }

            return index;
        }

		/// <summary>
        /// Converts the value of the current <see cref="System.Collections.IList"/> to its equivalent array string representation.
        /// </summary>
        /// <param name="source">The source <see cref="System.Collections.IList"/> object.</param>
        /// <returns>The array string representation of the value of <see cref="System.Collections.IList"/>.</returns>
        public static string ToArrayString(this IList source)
        {
            string[] strArr = new string[source.Count];

            for(int i = 0, length = source.Count; i < length; ++i)
            {
                strArr[i] = source[i].ToString();
            }

            return string.Format("{{ {0} }}", string.Join(", ", strArr));
        }
	}
}