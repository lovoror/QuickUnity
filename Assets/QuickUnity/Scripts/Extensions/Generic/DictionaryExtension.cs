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

using System.Collections.Generic;
using System.Linq;

namespace QuickUnity.Extensions.Generic
{
    /// <summary>
    /// Extension methods to the <see cref="System.Collections.Generic.Dictionary{T, V}"/>.
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// Adds the value with unique key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source Dictionary object.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddUnique<TKey, TValue>(this Dictionary<TKey, TValue> source, TKey key, TValue value)
        {
            if (!source.ContainsKey(key))
            {
                source.Add(key, value);
            }
        }

        /// <summary>
        /// Gets the key by value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="source">The source Dictionary object.</param>
        /// <param name="value">The value.</param>
        /// <returns>The value object.</returns>
        public static TKey GetKey<TKey, TValue>(this Dictionary<TKey, TValue> source, TValue value)
        {
            return source.FirstOrDefault(q => q.Value.Equals(value)).Key;
        }
    }
}