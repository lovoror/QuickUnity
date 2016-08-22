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
        /// The module reorderable list.
        /// </summary>
        private ReorderableList m_moduleReorderableList;

        /// <summary>
        /// The module list.
        /// </summary>
        private List<LocalizationModule> m_modules;

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

        /// <summary>
        /// The language reorderable list.
        /// </summary>
        private ReorderableList m_languageReorderableList;

        #region Messages

        /// <summary>
        /// This function is called when the object is loaded.
        /// </summary>
        private void OnEnable()
        {
            LoadConfig();

            if (m_modules == null)
            {
                m_modules = new List<LocalizationModule>();
            }

            if (m_moduleReorderableList == null)
            {
                m_moduleReorderableList = new ReorderableList(m_modules, typeof(LocalizationModule), true, true, true, true);

                // Draw header callback.
                m_moduleReorderableList.drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, "Modules");
                };

                // Draw element callback.
                m_moduleReorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    string moduleName = m_modules[index].name;
                    EditorGUI.LabelField(rect, moduleName);
                };

                // List change callback.
                m_moduleReorderableList.onChangedCallback = (list) =>
                {
                    SaveConfig();
                };

                // On item selected callback.
                m_moduleReorderableList.onSelectCallback = (list) =>
                {
                    m_selectedModule = m_modules[list.index];
                };
            }

            if (m_languageReorderableList == null)
            {
                m_languageReorderableList = new ReorderableList(null, typeof(ModuleLanguage), true, true, true, true);

                // Draw header callback.
                m_languageReorderableList.drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, "Languages");
                };

                // Draw element callback.
                m_languageReorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    ModuleLanguage element = m_selectedModule.languages[index];
                    rect.y += 2;
                    EditorGUI.Popup(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), 0, new string[2] { "English", "Chinese" });
                };

                // List change callback.
                m_languageReorderableList.onChangedCallback = (list) =>
                {
                    m_selectedModule.languages = (List<ModuleLanguage>)list.list;
                    SaveConfig();
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

            if (m_moduleReorderableList != null)
            {
                m_moduleReorderableList.DoLayoutList();
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

                // Module Name.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Module Name:");
                m_selectedModule.name = EditorGUILayout.TextField(m_selectedModule.name, GUILayout.Width(200));
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(10);

                // Languages
                if (m_languageReorderableList != null)
                {
                    m_languageReorderableList.list = m_selectedModule.languages;
                    m_languageReorderableList.DoLayoutList();
                }

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
            m_modules = iniFile.GetListValue<LocalizationModule>(INIConfigFileSectionName, "modules");
        }

        /// <summary>
        /// Saves the configuration.
        /// </summary>
        private void SaveConfig()
        {
            if (m_modules != null)
            {
                INIConfigFile iniFile = new INIConfigFile("Editor");
                iniFile.AddOrUpdateListValue(INIConfigFileSectionName, "modules", m_modules);
                iniFile.Save(Path.Combine(Application.dataPath, "Config"));
            }
        }

        #endregion Private Functions
    }
}