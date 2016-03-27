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

using System;
using System.Collections.Generic;

namespace QuickUnity.MVCS
{
    /// <summary>
    /// A map class to save data with Dictionary.
    /// </summary>
    /// <typeparam name="K">The type of key in Dictionary.</typeparam>
    /// <typeparam name="V">The type of value in Dictionary.</typeparam>
    public class DataMap<K, V> : IDisposable
    {
        /// <summary>
        /// The map dictionary.
        /// </summary>
        protected Dictionary<K, V> m_map;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataMap{K, V}"/> class.
        /// </summary>
        public DataMap()
        {
            m_map = new Dictionary<K, V>();
        }

        #region Pubic Functions

        #region IDispsable Implementations

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            if (m_map != null)
            {
                m_map.Clear();
                m_map = null;
            }
        }

        #endregion IDispsable Implementations

        #endregion Pubic Functions

        #region Protected Functions

        /// <summary>
        /// Registers the specified data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="data">The data.</param>
        protected void Register(K key, V data)
        {
            if (data == null)
                return;

            if (!m_map.ContainsKey(key))
                m_map.Add(key, data);
        }

        /// <summary>
        /// Retrieves the specified data.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The data.</returns>
        protected V Retrieve(K key)
        {
            if (key != null && m_map.ContainsKey(key))
                return m_map[key];

            return default(V);
        }

        /// <summary>
        /// Removes the specified data by key.
        /// </summary>
        /// <param name="key">The key.</param>
        protected void Remove(K key)
        {
            if (key == null)
                return;

            if (m_map.ContainsKey(key))
                m_map.Remove(key);
        }

        /// <summary>
        /// Removes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        protected void Remove(V data)
        {
            if (data == null)
                return;

            foreach (KeyValuePair<K, V> kvp in m_map)
            {
                if (kvp.Value.Equals(data))
                {
                    Remove(kvp.Key);
                    return;
                }
            }
        }

        #endregion Protected Functions
    }
}