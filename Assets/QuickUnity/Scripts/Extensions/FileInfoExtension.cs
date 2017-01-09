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

using System.IO;

namespace QuickUnity.Extensions
{
    /// <summary>
    /// Extension methods collection for System.IO.FileInfo.
    /// </summary>
    public static class FileInfoExtension
    {
        /// <summary>
        /// Renames the file.
        /// </summary>
        /// <param name="source">The source object of FileInfo.</param>
        /// <param name="newFileName">The new file name.</param>
        public static void Rename(this FileInfo source, string newFileName)
        {
            string dirPath = source.DirectoryName;
            string destPath = Path.Combine(dirPath, newFileName);
            source.MoveTo(destPath);
            source = new FileInfo(destPath);
        }

        /// <summary>
        /// Gets the file name without extension.
        /// </summary>
        /// <param name="source">The source object of FileInfo.</param>
        /// <returns>System.String The file name without extension.</returns>
        public static string GetFileNameWithoutExtension(this FileInfo source)
        {
            return Path.GetFileNameWithoutExtension(source.FullName);
        }
    }
}