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
using System.IO;
using System.Text.RegularExpressions;

namespace QuickUnity.Config
{
    /// <summary>
    /// INIConfigFile struct define an INI configuration section structure.
    /// </summary>
    public struct INIConfigSection
    {
        /// <summary>
        /// The name of section.
        /// </summary>
        private string m_sectionName;

        /// <summary>
        /// Gets the name of the section.
        /// </summary>
        /// <value>The name of the section.</value>
        public string sectionName
        {
            get
            {
                return m_sectionName;
            }
        }

        /// <summary>
        /// The value map.
        /// </summary>
        private Dictionary<string, string> m_valueMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="INIConfigSection"/> struct.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        public INIConfigSection(string sectionName)
        {
            m_sectionName = sectionName;
            m_valueMap = new Dictionary<string, string>();
        }

        /// <summary>
        /// Adds the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddValue(string key, string value)
        {
            key = key.Trim();
            value = value.Trim();

            if (!m_valueMap.ContainsKey(key))
            {
                m_valueMap.Add(key, value);
            }
        }
    }

    /// <summary>
    /// INIConfigFile class define an INI configuration file structure.
    /// </summary>
    public class INIConfigFile
    {
        /// <summary>
        /// The INI configuration section map.
        /// </summary>
        protected Dictionary<string, INIConfigSection> m_sectionMap = new Dictionary<string, INIConfigSection>();

        /// <summary>
        /// The current section name.
        /// </summary>
        private string m_currentSectionName;

        /// <summary>
        /// Initializes a new instance of the <see cref="INIConfigFile"/> class.
        /// </summary>
        /// <param name="fileContent">Content of the file.</param>
        public INIConfigFile(string fileContent)
        {
            if (!string.IsNullOrEmpty(fileContent))
            {
                string[] lines = fileContent.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

                for (int i = 0, length = lines.Length; i < length; ++i)
                {
                    string lineText = lines[i].Trim();
                    ParseLineText(lineText);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="INIConfigFile"/> class.
        /// </summary>
        /// <param name="reader">The file stream reader.</param>
        public INIConfigFile(StreamReader reader)
        {
            if (reader != null)
            {
                while (!reader.EndOfStream)
                {
                    string lineText = reader.ReadLine().Trim();
                    ParseLineText(lineText);
                }
            }
        }

        /// <summary>
        /// Parses the line text.
        /// </summary>
        /// <param name="lineText">The line text.</param>
        private void ParseLineText(string lineText)
        {
            string sectionName = null;
            bool isSection = ParseSection(lineText, out sectionName);

            if (isSection)
            {
                // It is section.
                if (!m_sectionMap.ContainsKey(sectionName))
                {
                    m_sectionMap.Add(sectionName, new INIConfigSection(sectionName));
                    m_currentSectionName = sectionName;
                }
            }
            else
            {
                // if it is a key/value pair.
                string key = null;
                string value = ParseValue(lineText, out key);

                if (!string.IsNullOrEmpty(value))
                {
                    if (m_sectionMap.ContainsKey(m_currentSectionName))
                    {
                        INIConfigSection section = m_sectionMap[m_currentSectionName];
                        section.AddValue(key, value);
                    }
                }
            }
        }

        /// <summary>
        /// Determines whether ths line text [is comment line].
        /// </summary>
        /// <param name="lineText">The line text.</param>
        /// <param name="commentCharIndex">Index of the comment character.</param>
        /// <returns><c>true</c> if the line text [is comment line]; otherwise, <c>false</c>.</returns>
        private bool IsCommentLine(string lineText, out int commentCharIndex)
        {
            const string commentStr = ";";
            int index = lineText.IndexOf(commentStr);
            commentCharIndex = -1;

            if (index > 0)
            {
                commentCharIndex = index;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Parses a section data.
        /// </summary>
        /// <param name="lineText">The line text.</param>
        /// <param name="sectionName">Name of the section.</param>
        /// <returns><c>true</c> if this line text define a section, <c>false</c> otherwise.</returns>
        private bool ParseSection(string lineText, out string sectionName)
        {
            int commentCharIndex = -1;
            bool isCommentLine = IsCommentLine(lineText, out commentCharIndex);
            sectionName = string.Empty;

            if (!isCommentLine)
            {
                const string pattern = @"\[([^\[^\]]*)\]";
                bool isMatch = Regex.IsMatch(lineText, pattern);

                if (isMatch)
                {
                    Match match = Regex.Match(lineText, pattern);

                    if (match.Index < commentCharIndex)
                    {
                        sectionName = match.Captures[0].Value;
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Parses the value.
        /// </summary>
        /// <param name="lineText">The line text.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value.</returns>
        private string ParseValue(string lineText, out string key)
        {
            int commentCharIndex = -1;
            bool isCommentLine = IsCommentLine(lineText, out commentCharIndex);
            key = string.Empty;

            if (!isCommentLine)
            {
                const string pattern = @"([^=\s]+)=([^=\s]+)";
                bool isMatch = Regex.IsMatch(lineText, pattern);

                if (isMatch)
                {
                    Match match = Regex.Match(lineText, pattern);

                    if (match.Index < commentCharIndex)
                    {
                        string result = match.Value;
                        string[] resultArr = result.Split('=');

                        if (resultArr != null && resultArr.Length > 1)
                        {
                            key = resultArr[0];
                            return resultArr[1];
                        }
                    }
                }
            }

            return null;
        }
    }
}