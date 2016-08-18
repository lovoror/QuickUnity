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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

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

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        public string GetValue(string key)
        {
            if (m_valueMap.ContainsKey(key))
            {
                return m_valueMap[key];
            }

            return null;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T">The type of value.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The value of the type.</returns>
        public T GetValue<T>(string key)
        {
            string strValue = GetValue(key);
            T value = default(T);

            if (!string.IsNullOrEmpty(strValue))
            {
                value = ObjectUtility.TryParse<T>(strValue);
            }

            return value;
        }

        /// <summary>
        /// Gets the list value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The list value.</returns>
        public List<string> GetListValue(string key)
        {
            List<string> list = null;
            string strValue = GetValue(key);

            if (!string.IsNullOrEmpty(strValue))
            {
                string[] values = strValue.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                list = new List<string>(values);
            }

            return (list != null && list.Count > 0) ? list : null;
        }

        /// <summary>
        /// Gets the list value.
        /// </summary>
        /// <typeparam name="T">The target type of object.</typeparam>
        /// <param name="key">The key.</param>
        /// <returns>The list value.</returns>
        public List<T> GetListValue<T>(string key)
        {
            List<T> list = new List<T>();

            string strValue = GetValue(key);

            if (!string.IsNullOrEmpty(strValue))
            {
                string[] values = strValue.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0, length = values.Length; i < length; ++i)
                {
                    string value = values[i].Trim();
                    T targetValue = ObjectUtility.TryParse<T>(value);

                    if (targetValue != null)
                    {
                        list.Add(targetValue);
                    }
                }
            }

            return (list != null && list.Count > 0) ? list : null;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            List<string> representList = new List<string>();

            foreach (KeyValuePair<string, string> kvp in m_valueMap)
            {
                string represent = kvp.Key + ":" + kvp.Value;
                representList.Add(represent);
            }

            string[] represents = representList.ToArray();
            return string.Join("\n", represents);
        }
    }

    /// <summary>
    /// INIConfigFile class define an INI configuration file structure.
    /// </summary>
    public class INIConfigFile
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
        public INIConfigFile()
        {
        }

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
        /// Gets the value.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value object.</returns>
        public string GetValue(string sectionName, string key)
        {
            string value = null;

            if (m_sectionMap != null && m_sectionMap.ContainsKey(sectionName))
            {
                INIConfigSection section = m_sectionMap[sectionName];
                return section.GetValue(key);
            }

            return value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <typeparam name="T">The type of value object.</typeparam>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="key">The key.</param>
        /// <returns>The value object.</returns>
        public T GetValue<T>(string sectionName, string key)
        {
            T value = default(T);

            if (m_sectionMap != null && m_sectionMap.ContainsKey(sectionName))
            {
                INIConfigSection section = m_sectionMap[sectionName];
                return section.GetValue<T>(key);
            }

            return value;
        }

        /// <summary>
        /// Gets the list value.
        /// </summary>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="key">The key.</param>
        /// <returns>The list value.</returns>
        public List<string> GetListValue(string sectionName, string key)
        {
            if (m_sectionMap != null && m_sectionMap.ContainsKey(sectionName))
            {
                INIConfigSection section = m_sectionMap[sectionName];
                return section.GetListValue(key);
            }

            return null;
        }

        /// <summary>
        /// Gets the list value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sectionName">Name of the section.</param>
        /// <param name="key">The key.</param>
        /// <returns>The list value.</returns>
        public List<T> GetListValue<T>(string sectionName, string key)
        {
            if (m_sectionMap != null && m_sectionMap.ContainsKey(sectionName))
            {
                INIConfigSection section = m_sectionMap[sectionName];
                return section.GetListValue<T>(key);
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
        public override string ToString()
        {
            string value = string.Empty;

            foreach (KeyValuePair<string, INIConfigSection> kvp in m_sectionMap)
            {
                value += "[" + kvp.Key + "]\n";
                value += kvp.Value.ToString() + "\n\n";
            }

            return value;
        }

        #region Private Functions

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

            if (index == 0)
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

                    if (commentCharIndex == -1 || match.Index < commentCharIndex)
                    {
                        sectionName = match.Groups[1].Value;
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

                    if (commentCharIndex == -1 || match.Index < commentCharIndex)
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

        #endregion Private Functions
    }
}