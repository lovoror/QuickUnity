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
using System.IO;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace QuickUnity.Editor.Localization
{
    /// <summary>
    /// The Localization Dashboard Window.
    /// </summary>
    /// <seealso cref="UnityEditor.EditorWindow"/>
    public class LocalizationDashboardWindow : EditorWindow
    {
        /// <summary>
        /// The ini configuration file section name.
        /// </summary>
        private const string INIConfigFileSectionName = "Localization";

        /// <summary>
        /// The reorderable list.
        /// </summary>
        private ReorderableList m_reorderableList;

        /// <summary>
        /// The module list.
        /// </summary>
        private List<LocalizationModule> m_moduleList;

        /// <summary>
        /// The modules view scroll position.
        /// </summary>
        private Vector2 m_modulesViewScrollPosition;

        /// <summary>
        /// The module detail view scroll position.
        /// </summary>
        private Vector2 m_moduleDetailViewScrollPosition;

        /// <summary>
        /// If can show module detail view.
        /// </summary>
        private float m_showModuleDetailView = 0;

        /// <summary>
        /// The selected module.
        /// </summary>
        private LocalizationModule m_selectedModule;

        #region Messages

        /// <summary>
        /// This function is called when the object is loaded.
        /// </summary>
        private void OnEnable()
        {
            LoadConfig();

            if (m_moduleList == null)
            {
                m_moduleList = new List<LocalizationModule>();
            }

            if (m_reorderableList == null)
            {
                m_reorderableList = new ReorderableList(m_moduleList, typeof(LocalizationModule), true, true, true, true);

                // Draw header callback.
                m_reorderableList.drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, "Modules");
                };

                // Draw element callback.
                m_reorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    string moduleName = m_moduleList[index].name;
                    EditorGUI.LabelField(rect, moduleName);
                };

                // List change callback.
                m_reorderableList.onChangedCallback = (list) =>
                {
                    SaveConfig();
                };

                // On item selected callback.
                m_reorderableList.onSelectCallback = (list) =>
                {
                    m_selectedModule = m_moduleList[list.index];
                };
            }
        }

        private Vector2 scrollPos;

        /// <summary>
        /// GUI Implementation.
        /// </summary>
        private void OnGUI()
        {
            if (m_selectedModule != null)
            {
                m_showModuleDetailView = 1;
            }

            EditorGUILayout.BeginVertical();
            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);

            EditorGUILayout.BeginVertical();

            // Draw module list.
            m_modulesViewScrollPosition = EditorGUILayout.BeginScrollView(m_modulesViewScrollPosition);

            if (m_reorderableList != null)
            {
                m_reorderableList.DoLayoutList();
            }

            EditorGUILayout.EndScrollView();

            // Draw space between module list and module detail view.
            if (m_showModuleDetailView == 1)
            {
                GUILayout.Space(10);
            }

            // Draw module detail view.
            if (EditorGUILayout.BeginFadeGroup(m_showModuleDetailView))
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Toolbar(0, new string[1] { m_selectedModule.name }, GUILayout.Width(200));
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(5);

                m_moduleDetailViewScrollPosition = EditorGUILayout.BeginScrollView(m_moduleDetailViewScrollPosition, false, false);
                EditorGUILayout.LabelField("TEST");
                EditorGUILayout.LabelField("TEST");
                EditorGUILayout.LabelField("TEST");
                EditorGUILayout.LabelField("TEST");
                EditorGUILayout.LabelField("TEST");
                EditorGUILayout.EndScrollView();
            }

            EditorGUILayout.EndFadeGroup();

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
            INIConfigFile iniFile = INIConfigFile.ParseINIConfigFile(Path.Combine(Application.dataPath, "Config/Editor.ini"));
            m_moduleList = iniFile.GetListValue<LocalizationModule>(INIConfigFileSectionName, "modules");
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        private void SaveConfig()
        {
            if (m_moduleList != null)
            {
                INIConfigFile iniFile = new INIConfigFile("Editor");
                iniFile.AddOrUpdateListValue(INIConfigFileSectionName, "modules", m_moduleList);
                iniFile.Save(Path.Combine(Application.dataPath, "Config"));
            }
        }

        #endregion Private Functions
    }
}