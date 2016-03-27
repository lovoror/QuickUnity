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

using QuickUnity.Utilities;
using System;
using System.Globalization;
using System.IO;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The type parser base class.
    /// </summary>
    public abstract class TypeParser : ITypeParser
    {
        #region API

        /// <summary>
        /// Parses the type string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The parsed type string.
        /// </returns>
        public virtual string ParseType(string source)
        {
            return source;
        }

        /// <summary>
        /// Gets the stream data.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The stream data.
        /// </returns>
        public virtual MemoryStream GetStream(string value)
        {
            return null;
        }

        /// <summary>
        /// Parses the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// The parsed value.
        /// </returns>
        public abstract object ParseValue(string value);

        #endregion API

        #region Protected Functions

        /// <summary>
        /// Parses the string object.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The string value.</returns>
        protected string ParseString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value;
        }

        /// <summary>
        /// Parses the number value.
        /// </summary>
        /// <typeparam name="T">The type of object</typeparam>
        /// <param name="value">The value.</param>
        /// <returns>The converted object.</returns>
        protected T ParseNumber<T>(string value)
        {
            T result = default(T);
            Type targetType = typeof(T);
            value = value.Trim();
            object[] args = new object[4] { value, NumberStyles.Any, CultureInfo.InvariantCulture, result };

            if (targetType != null && !string.IsNullOrEmpty(value))
            {
                ReflectionUtility.InvokeStaticMethod(targetType, "TryParse", new Type[4] {
                    typeof(string), typeof(NumberStyles), typeof(CultureInfo), targetType.MakeByRefType()
                }, ref args);
            }

            return (T)args[3];
        }

        #endregion Protected Functions
    }
}