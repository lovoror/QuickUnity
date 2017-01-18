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

namespace QuickUnity.Utilities
{
    /// <summary>
    /// Utility class for common time functions. This class cannot be inherited.
    /// </summary>
    public sealed class TimeUtility
    {
        /// <summary>
        /// Converts the date time to UNIX time stamp.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>The UNIX time stamp.</returns>
        public static int ConvertToUnixTimeStamp(DateTime dateTime)
        {
            TimeSpan ts = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// Gets the UNIX time stamp.
        /// </summary>
        /// <returns>The UNIX time stamp.</returns>
        public static int GetUnixTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt32(ts.TotalSeconds);
        }

        /// <summary>
        /// Gets the time stamp.
        /// </summary>
        /// <returns>The time stamp.</returns>
        public static string GetTimeStamp()
        {
            return GetUnixTimeStamp().ToString();
        }
    }
}