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

using System.Collections.Generic;
using System.IO;

namespace QuickUnity.Extensions
{
    /// <summary>
    /// Extension methods collection for System.IO.DirectoryInfo.
    /// </summary>
    public static class DirectoryInfoExtension
    {
        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="source">The DirectoryInfo source.</param>
        /// <param name="searchPatterns">The search patterns.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns>FileInfo[]. The array of FileInfo objects.</returns>
        public static FileInfo[] GetFiles(this DirectoryInfo source, string[] searchPatterns, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            List<FileInfo> result = new List<FileInfo>();

            if (searchPatterns != null)
            {
                for (int i = 0, length = searchPatterns.Length; i < length; ++i)
                {
                    string searchPattern = searchPatterns[i];

                    if (!string.IsNullOrEmpty(searchPattern))
                    {
                        FileInfo[] array = source.GetFiles(searchPattern, searchOption);

                        if (array != null && array.Length > 0)
                        {
                            result.AddRangeUnique(array);
                        }
                    }
                }
            }

            return result.ToArray();
        }
    }
}