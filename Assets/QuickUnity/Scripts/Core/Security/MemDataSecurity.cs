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

namespace QuickUnity.Core.Security
{
    /// <summary>
    /// MemDataSecurity is a class that process memory data encryption and decryption.
    /// </summary>
    internal sealed class MemDataSecurity
    {
        /// <summary>
        /// The key.
        /// </summary>
        private static int s_key;

        /// <summary>
        /// The long key.
        /// </summary>
        private static long s_longKey;

        /// <summary>
        /// The check key.
        /// </summary>
        private static int s_checkKey;

        /// <summary>
        /// The check long key.
        /// </summary>
        private static long s_checkLongKey;

        /// <summary>
        /// Initializes static members of the <see cref="MemDataSecurity"/> class.
        /// </summary>
        static MemDataSecurity()
        {
            Random rnd = new Random();
            s_key = rnd.Next();
        }
    }
}