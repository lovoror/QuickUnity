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

using QuickUnity.Config;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace QuickUnity.Editor.Localization
{
    /// <summary>
    /// The LanguagePresets Window.
    /// </summary>
    /// <seealso cref="UnityEditor.EditorWindow"/>
    public class LanguagePresetsWindow : EditorWindow
    {
        /// <summary>
        /// The INI Configuration file.
        /// </summary>
        private INIConfigFile m_iniFile = null;

        /// <summary>
        /// The language reorderable list.
        /// </summary>
        private ReorderableList m_languagePresetList = null;

        /// <summary>
        /// The languge preset list.
        /// </summary>
        private List<LanguagePreset> m_langugePresets = null;

        #region Messages

        /// <summary>
        /// This function is called when the object is loaded.
        /// </summary>
        private void OnEnable()
        {
            LoadConfig();

            if (m_langugePresets == null)
            {
                m_langugePresets = new List<LanguagePreset>();
            }

            m_languagePresetList = new ReorderableList(m_langugePresets, typeof(LanguagePreset), false, false, true, true);

            // Draw element callback.
            m_languagePresetList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                LanguagePreset element = m_langugePresets[index];

                if (element != null)
                {
                    rect.height -= 6;
                    rect.width = 250;

                    // Display name field.
                    rect.y += 2;
                    EditorGUI.PrefixLabel(rect, new GUIContent("Display Name: "));

                    rect.x += 90;
                    element.displayName = EditorGUI.TextField(rect, element.displayName);

                    // Language field.
                    rect.x += 300;
                    EditorGUI.PrefixLabel(rect, new GUIContent("Language: "));
                    rect.x += 70;
                    element.language = EditorGUI.TextField(rect, element.language);
                }
            };
        }

        /// <summary>
        /// OnDestroy is called when the EditorWindow is closed.
        /// </summary>
        private void OnDestroy()
        {
            SaveConfig();
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

            EditorGUILayout.BeginVertical();

            EditorGUI.BeginChangeCheck();

            // Languages reorderable list.
            if (m_languagePresetList != null)
            {
                m_languagePresetList.DoLayoutList();
            }

            if (EditorGUI.EndChangeCheck())
            {
                SaveConfig();
            }

            EditorGUILayout.EndVertical();

            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            EditorGUILayout.EndVertical();
        }

        #endregion Messages

        #region Private Functions

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        private void LoadConfig()
        {
            if (m_iniFile == null)
            {
                m_iniFile = INIConfigFile.ParseINIConfigFile(QuickUnityEditor.EditorConfigPath);
                m_langugePresets = m_iniFile.GetListValue<LanguagePreset>(LocalizationUtility.INIConfigFileSectionName, LocalizationUtility.LanguagePresetsConfigKey);
            }
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        private void SaveConfig()
        {
            if (m_iniFile == null)
            {
                LoadConfig();
            }

            if (m_iniFile != null && m_langugePresets != null)
            {
                m_iniFile.AddOrUpdateListValue(LocalizationUtility.INIConfigFileSectionName, LocalizationUtility.LanguagePresetsConfigKey, m_langugePresets);
                m_iniFile.Save(QuickUnityEditor.EditorConfigPath);
            }
        }

        #endregion Private Functions
    }
}