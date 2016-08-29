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

using Pathfinding.Serialization.JsonFx;
using QuickUnity.Localization;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace QuickUnity.Editor.Localization
{
    /// <summary>
    /// The LocalizationArchive window.
    /// </summary>
    /// <seealso cref="UnityEditor.EditorWindow"/>
    public class LocalizationArchiveWindow : EditorWindow
    {
        /// <summary>
        /// The current window.
        /// </summary>
        public static LocalizationArchiveWindow currentWindow = null;

        /// <summary>
        /// The localization archive list.
        /// </summary>
        private List<LocalizationArchive> m_localizationArchives;

        /// <summary>
        /// Sets the localization archives.
        /// </summary>
        /// <value>The localization archives.</value>
        public List<LocalizationArchive> localizationArchives
        {
            set
            {
                m_localizationArchives = value;

                if (m_archivesReorderableList != null && m_localizationArchives != null)
                {
                    m_archivesReorderableList.list = m_localizationArchives;
                }
            }
        }

        /// <summary>
        /// The module language.
        /// </summary>
        private ModuleLanguage m_moduleLanguage;

        /// <summary>
        /// Sets the module language.
        /// </summary>
        /// <value>The module language.</value>
        public ModuleLanguage moduleLanguage
        {
            set
            {
                m_moduleLanguage = value;
            }
        }

        /// <summary>
        /// The archive file path
        /// </summary>
        private string m_archiveFilePath;

        /// <summary>
        /// Sets the archive file path.
        /// </summary>
        /// <value>The archive file path.</value>
        public string archiveFilePath
        {
            set
            {
                m_archiveFilePath = value;
            }
        }

        /// <summary>
        /// The archives scroll position.
        /// </summary>
        private Vector2 m_archivesScrollPosition = default(Vector2);

        /// <summary>
        /// The archives reorderable list.
        /// </summary>
        private ReorderableList m_archivesReorderableList = null;

        #region Messages

        /// <summary>
        /// This function is called when the object is loaded.
        /// </summary>
        private void OnEnable()
        {
            if (m_localizationArchives == null)
            {
                m_localizationArchives = new List<LocalizationArchive>();
            }

            m_archivesReorderableList = new ReorderableList(m_localizationArchives, typeof(LocalizationArchive), false, false, true, true);

            // Draw element callback.
            m_archivesReorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                LocalizationArchive element = m_localizationArchives[index];

                if (element != null)
                {
                    rect.height -= 6;
                    rect.width = 250;

                    // Key field.
                    rect.y += 2;
                    EditorGUI.PrefixLabel(rect, new GUIContent("Key: "));

                    rect.x += 90;
                    element.key = EditorGUI.TextField(rect, element.key);

                    // Translation field.
                    rect.x += 300;
                    EditorGUI.PrefixLabel(rect, new GUIContent("Translation: "));
                    rect.x += 70;
                    element.translation = EditorGUI.TextField(rect, element.translation);
                }
            };
        }

        /// <summary>
        /// OnDestroy is called when the EditorWindow is closed.
        /// </summary>
        private void OnDestroy()
        {
            SaveArchives();
        }

        /// <summary>
        /// GUI Implementation.
        /// </summary>
        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);

            m_archivesScrollPosition = EditorGUILayout.BeginScrollView(m_archivesScrollPosition, false, false);

            EditorGUI.BeginChangeCheck();

            if (m_archivesReorderableList != null)
            {
                m_archivesReorderableList.DoLayoutList();
            }

            if (EditorGUI.EndChangeCheck())
            {
                SaveArchives();
            }

            EditorGUILayout.EndScrollView();

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
        }

        #endregion Messages

        #region Private Functions

        /// <summary>
        /// Saves the archives.
        /// </summary>
        private void SaveArchives()
        {
            if (!string.IsNullOrEmpty(m_archiveFilePath))
            {
                string text = JsonWriter.Serialize(m_localizationArchives);
                File.WriteAllText(m_archiveFilePath, text, Encoding.UTF8);

                if (m_localizationArchives != null && m_moduleLanguage != null)
                {
                    m_moduleLanguage.wordCount = m_localizationArchives.Count;
                }
            }
        }

        #endregion Private Functions
    }
}