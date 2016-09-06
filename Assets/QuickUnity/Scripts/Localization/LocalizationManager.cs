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

using QuickUnity.Patterns;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuickUnity.Localization
{
    /// <summary>
    /// LocalizationManager is class for handling all methods for feature localization.
    /// </summary>
    /// <seealso cref="QuickUnity.Patterns.Singleton{QuickUnity.Localization.LocalizationManager}"/>
    public class LocalizationManager : Singleton<LocalizationManager>
    {
        /// <summary>
        /// The default empty text.
        /// </summary>
        private const string DefaultEmptyText = "EmptyText";

        /// <summary>
        /// The localization resources path.
        /// </summary>
        private string m_locresFilesPath;

        /// <summary>
        /// Gets the localization resources path.
        /// </summary>
        /// <value>The localization resources path.</value>
        public string locresFilesPath
        {
            get
            {
                return m_locresFilesPath;
            }
        }

        /// <summary>
        /// The current language.
        /// </summary>
        private string m_currentLanguage;

        #region Public Functions

        /// <summary>
        /// Initializes.
        /// </summary>
        /// <param name="path">The localization resources path.</param>
        public void Initialize(string path)
        {
            m_locresFilesPath = path;

            // Initialize languages.
            InitializeLanguages();

            // Initialize archives.
            InitializeArchives();
        }

        /// <summary>
        /// Gets the current language.
        /// </summary>
        /// <returns>The current language.</returns>
        public string GetCurrentLanguage()
        {
            return m_currentLanguage;
        }

        /// <summary>
        /// Sets the current language.
        /// </summary>
        /// <param name="language">The language.</param>
        public void SetCurrentLanguage(string language)
        {
            m_currentLanguage = language;
        }

        /// <summary>
        /// Gets the localization text.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="key">The key.</param>
        /// <returns>The localization text.</returns>
        public string GetLocalizationText(string moduleName, string key)
        {
            return null;
        }

        /// <summary>
        /// Gets the localization text.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="key">The key.</param>
        /// <returns>The localization text.</returns>
        public string GetLocalizationText(string language, string moduleName, string key)
        {
            return null;
        }

        #endregion Public Functions

        #region Private Functions

        /// <summary>
        /// Initializes languages.
        /// </summary>
        private void InitializeLanguages()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(m_locresFilesPath);
            DirectoryInfo[] subDirInfos = dirInfo.GetDirectories();

            if (subDirInfos.Length > 0)
            {
                for (int i = 0, length = subDirInfos.Length; i < length; ++i)
                {
                    DirectoryInfo subDirInfo = subDirInfos[i];
                    string dirName = subDirInfo.Name;
                }

                SetCurrentLanguage(subDirInfos[0].Name);
            }
        }

        /// <summary>
        /// Initializes archives.
        /// </summary>
        private void InitializeArchives()
        {
        }

        #endregion Private Functions
    }
}