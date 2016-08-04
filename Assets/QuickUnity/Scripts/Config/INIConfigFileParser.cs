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
using System.IO;
using System.Text;
using UnityEngine;

namespace QuickUnity.Config
{
    /// <summary>
    /// INIConfigFileParser is a class to parse INI configuration file.
    /// </summary>
    public class INIConfigFileParser
    {
        /// <summary>
        /// Parses the INI configuration file.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>INIConfigFile object.</returns>
        public static INIConfigFile ParseINIConfigFile(string filePath)
        {
            INIConfigFile configFile = null;
            string text = LoadINIFromResource(filePath);

            if (!string.IsNullOrEmpty(text))
            {
                configFile = new INIConfigFile(text);
            }
            else
            {
                StreamReader sr = LoadINIFromFileStream(filePath);
                configFile = new INIConfigFile(sr);
            }

            return configFile;
        }

        /// <summary>
        /// Loads the INI file content from project Resources folder.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The INI file text content.</returns>
        private static string LoadINIFromResource(string filePath)
        {
            TextAsset asset = Resources.Load<TextAsset>(filePath);

            if (asset != null)
            {
                return asset.text;
            }

            return null;
        }

        /// <summary>
        /// Loads the INI file content from FileStream.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The INI file StreamReader object.</returns>
        private static StreamReader LoadINIFromFileStream(string filePath)
        {
            StreamReader sr = null;

            if (File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.None);
                sr = new StreamReader(fs, Encoding.UTF8);
            }

            return sr;
        }
    }
}