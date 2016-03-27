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

using UnityEngine;

namespace QuickUnity
{
    /// <summary>
    /// The environment of platform.
    /// </summary>
    public static class Environment
    {
        /// <summary>
        /// The newline of Unix operation system.
        /// </summary>
        public const string UnixNewline = "\n";

        /// <summary>
        /// The newline of Windows operation system.
        /// </summary>
        public const string WindowsNewline = "\r\n";

        /// <summary>
        /// Gets the newline.
        /// </summary>
        /// <value>
        /// The newline.
        /// </value>
        public static string newline
        {
            get
            {
                string result = string.Empty;

                switch (Application.platform)
                {
                    case RuntimePlatform.WindowsEditor:
                        result = WindowsNewline;
                        break;

                    case RuntimePlatform.OSXEditor:
                    default:
                        result = UnixNewline;
                        break;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets the streaming asset path by platform.
        /// </summary>
        /// <param name="assetPath">The asset path.</param>
        /// <returns>System.String.</returns>
        public static string GetStreamingAssetPath(string assetPath)
        {
            string path = "";

            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.OSXEditor:
                default:
                    // Windows Editor or Mac OS X Editor
                    path = "file://" + Application.dataPath + "/StreamingAssets" + assetPath;
                    break;

                case RuntimePlatform.Android:
                    // Android
                    path = "jar:file://" + Application.dataPath + "!/assets" + assetPath;
                    break;

                case RuntimePlatform.IPhonePlayer:
                    // iOS
                    path = "file://" + Application.dataPath + "/Raw" + assetPath;
                    break;
            }

            return path;
        }
    }
}