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
using QuickUnity.Config;
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
    /// The Localization Dashboard Window.
    /// </summary>
    /// <seealso cref="UnityEditor.EditorWindow"/>
    public class LocalizationDashboardWindow : EditorWindow
    {
        /// <summary>
        /// The INI Configuration file.
        /// </summary>
        private INIConfigFile m_iniFile = null;

        /// <summary>
        /// The localization files path.
        /// </summary>
        private string m_localizationFilesPath = null;

        /// <summary>
        /// The module reorderable list.
        /// </summary>
        private ReorderableList m_moduleReorderableList = null;

        /// <summary>
        /// The module list.
        /// </summary>
        private List<LocalizationModule> m_modules = null;

        /// <summary>
        /// The language preset list.
        /// </summary>
        private List<LanguagePreset> m_languagePresets = null;

        /// <summary>
        /// The language display name list.
        /// </summary>
        private List<string> m_languageDisplayNames = new List<string>();

        /// <summary>
        /// The modules view scroll position.
        /// </summary>
        private Vector2 m_modulesViewScrollPosition = default(Vector2);

        /// <summary>
        /// The module detail view scroll position.
        /// </summary>
        private Vector2 m_moduleDetailViewScrollPosition = default(Vector2);

        /// <summary>
        /// If can show module detail view.
        /// </summary>
        private float m_showModuleDetailView = 0;

        /// <summary>
        /// The selected module.
        /// </summary>
        private LocalizationModule m_selectedModule = null;

        /// <summary>
        /// The language reorderable list.
        /// </summary>
        private ReorderableList m_languageReorderableList = null;

        #region Messages

        /// <summary>
        /// This function is called when the object is loaded.
        /// </summary>
        private void OnEnable()
        {
            LoadConfig();

            if (m_languagePresets == null)
            {
                m_languagePresets = new List<LanguagePreset>();
            }

            if (m_modules == null)
            {
                m_modules = new List<LocalizationModule>();
            }

            if (m_moduleReorderableList == null)
            {
                m_moduleReorderableList = new ReorderableList(m_modules, typeof(LocalizationModule), false, true, true, true);

                // Draw module reorderable list header callback.
                m_moduleReorderableList.drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, "Modules", EditorStyles.boldLabel);
                };

                // Draw module reorderable list element callback.
                m_moduleReorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    LocalizationModule module = m_modules[index];

                    if (module != null)
                    {
                        rect.height = EditorGUIUtility.singleLineHeight;
                        rect.y += 2;
                        string moduleName = module.moduleName;
                        EditorGUI.LabelField(rect, moduleName);
                    }
                };

                // On module reorderable list change callback.
                m_moduleReorderableList.onChangedCallback = (list) =>
                {
                    SaveConfig();
                };

                // On module reorderable list item selected callback.
                m_moduleReorderableList.onSelectCallback = (list) =>
                {
                    m_selectedModule = m_modules[list.index];
                };
            }

            if (m_languageReorderableList == null)
            {
                m_languageReorderableList = new ReorderableList(null, typeof(ModuleLanguage), false, true, true, true);

                // Draw language reorderable list header callback.
                m_languageReorderableList.drawHeaderCallback = (rect) =>
                {
                    EditorGUI.LabelField(rect, "Languages");
                };

                // Draw language reorderable list element callback.
                m_languageReorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                {
                    ModuleLanguage element = m_selectedModule.languages[index];
                    LanguagePreset preset = m_languagePresets[element.selectedItemIndex];

                    if (element != null)
                    {
                        rect.y += 2;

                        if (m_languageDisplayNames.Count == 0)
                        {
                            element.selectedItemIndex = 0;
                        }

                        // Language popup.
                        element.selectedItemIndex = EditorGUI.Popup(new Rect(rect.x, rect.y, 250, EditorGUIUtility.singleLineHeight), element.selectedItemIndex, m_languageDisplayNames.ToArray());

                        // Word count.
                        Rect wordCountRect = new Rect(rect.x + 275, rect.y, 180, rect.height - 5);
                        EditorGUI.LabelField(wordCountRect, string.Format("Word Count: {0}", element.wordCount));

                        // Edit translation texts.
                        Rect editButtonRect = new Rect(wordCountRect.x + wordCountRect.width + 20, rect.y, 75, rect.height - 5);
                        if (GUI.Button(editButtonRect, "Edit"))
                        {
                            LocalizationUtility.ShowLocalizationArchiveWindow(element, m_localizationFilesPath, preset.language, m_selectedModule.moduleName);
                        }

                        // Import file.
                        Rect importButtonRect = new Rect(editButtonRect.x + editButtonRect.width + 10, rect.y, editButtonRect.width, editButtonRect.height);
                        if (GUI.Button(importButtonRect, "Import"))
                        {
                            string translationTextFilePath = EditorUtility.OpenFilePanelWithFilters("Import", null, new string[4] { "txt", "json", "xls", "xlsx" });
                            string text = LocalizationUtility.ImportTranslationTextFile(translationTextFilePath);

                            // Write text to archive file.
                            if (!string.IsNullOrEmpty(text))
                            {
                                List<LocalizationArchive> archives = JsonReader.Deserialize<List<LocalizationArchive>>(text);

                                if (archives != null)
                                {
                                    element.wordCount = archives.Count;
                                }

                                string saveFilePath = Path.Combine(m_localizationFilesPath, preset.language);
                                saveFilePath = Path.Combine(saveFilePath, m_selectedModule.moduleName + LocalizationUtility.LocalizationArchiveFileExtension);
                                File.WriteAllText(saveFilePath, text, Encoding.UTF8);
                            }
                        }

                        // Export file.
                        Rect exportButtonRect = new Rect(importButtonRect.x + importButtonRect.width + 10, rect.y, editButtonRect.width, editButtonRect.height);
                        if (GUI.Button(exportButtonRect, "Export"))
                        {
                            string defaultName = m_selectedModule.moduleName + "-" + preset.language + ".json";
                            string targetPath = EditorUtility.SaveFilePanel("Export", null, defaultName, "json");

                            if (!string.IsNullOrEmpty(targetPath))
                            {
                                string archiveFilePath = Path.Combine(m_localizationFilesPath, preset.language);
                                archiveFilePath = Path.Combine(archiveFilePath, m_selectedModule.moduleName + LocalizationUtility.LocalizationArchiveFileExtension);
                                LocalizationUtility.ExportTranslationArchiveFile(targetPath, archiveFilePath);
                            }
                        }

                        // Compile translation archive.
                        Rect compileButtonRect = new Rect(exportButtonRect.x + exportButtonRect.width + 10, rect.y, editButtonRect.width, editButtonRect.height);
                        if (GUI.Button(compileButtonRect, "Compile"))
                        {
                            LocalizationUtility.CompileTranslationFile(m_localizationFilesPath, preset.language, m_selectedModule.moduleName);
                        }
                    }
                };

                // On language reorderable list change callback.
                m_languageReorderableList.onChangedCallback = (list) =>
                {
                    m_selectedModule.languages = (List<ModuleLanguage>)list.list;
                    SaveConfig();
                };
            }
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
            if (m_selectedModule != null)
            {
                m_showModuleDetailView = 1;
            }

            EditorGUILayout.BeginVertical();
            GUILayout.Space(10);

            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);

            EditorGUILayout.BeginVertical();

            m_modulesViewScrollPosition = EditorGUILayout.BeginScrollView(m_modulesViewScrollPosition, false, false);

            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();

            // Draw localization files path.
            EditorGUILayout.PrefixLabel("Localization Files Path: ");

            GUIStyle fileTextFieldStyle = new GUIStyle(EditorStyles.textField);
            fileTextFieldStyle.fixedHeight = 20;
            EditorGUILayout.SelectableLabel(m_localizationFilesPath, fileTextFieldStyle, GUILayout.Width(300), GUILayout.Height(EditorGUIUtility.singleLineHeight));
            GUILayout.Space(5);
            if (GUILayout.Button("Browse", GUILayout.Width(75), GUILayout.Height(18)))
            {
                m_localizationFilesPath = EditorUtility.OpenFolderPanel("Localization Files Path", null, null);
            }

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);

            // Draw module list.
            if (m_moduleReorderableList != null)
            {
                m_moduleReorderableList.DoLayoutList();
            }

            EditorGUILayout.EndVertical();

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
                GUILayout.Toolbar(0, new string[1] { m_selectedModule.moduleName }, GUILayout.Width(200));
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(5);

                m_moduleDetailViewScrollPosition = EditorGUILayout.BeginScrollView(m_moduleDetailViewScrollPosition, false, false);

                EditorGUI.BeginChangeCheck();

                // Module Name.
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Module Name:");
                m_selectedModule.moduleName = EditorGUILayout.TextField(m_selectedModule.moduleName, GUILayout.Width(200));
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(10);

                // Languages
                if (m_languageReorderableList != null)
                {
                    m_languageReorderableList.list = m_selectedModule.languages;
                    m_languageReorderableList.DoLayoutList();
                }

                if (EditorGUI.EndChangeCheck())
                {
                    SaveConfig();
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
            if (m_iniFile == null)
            {
                m_iniFile = INIConfigFile.ParseINIConfigFile(QuickUnityEditor.EditorConfigPath);
                m_localizationFilesPath = m_iniFile.GetValue(LocalizationUtility.INIConfigFileSectionName, LocalizationUtility.LocalizationFilesPathConfigKey);
                m_languagePresets = m_iniFile.GetListValue<LanguagePreset>(LocalizationUtility.INIConfigFileSectionName, LocalizationUtility.LanguagePresetsConfigKey);
                m_modules = m_iniFile.GetListValue<LocalizationModule>(LocalizationUtility.INIConfigFileSectionName, LocalizationUtility.ModulesConfigKey);

                if (m_languagePresets != null)
                {
                    if (m_languageDisplayNames.Count > 0)
                    {
                        m_languageDisplayNames.Clear();
                    }

                    m_languagePresets.ForEach((preset) =>
                    {
                        m_languageDisplayNames.Add(preset.displayName);
                    });
                }

                // Check if the localization files path is valid.
                if (!string.IsNullOrEmpty(m_localizationFilesPath) && !Directory.Exists(m_localizationFilesPath))
                {
                    m_localizationFilesPath = null;
                    LocalizationUtility.DisplayInvalidLocalizationFilesPathDialog();
                }
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

            if (m_iniFile != null && m_modules != null)
            {
                m_iniFile.AddOrUpdateValue(LocalizationUtility.INIConfigFileSectionName, LocalizationUtility.LocalizationFilesPathConfigKey, m_localizationFilesPath);
                m_iniFile.AddOrUpdateListValue(LocalizationUtility.INIConfigFileSectionName, LocalizationUtility.ModulesConfigKey, m_modules);
                m_iniFile.Save(QuickUnityEditor.EditorConfigPath);
            }
        }

        #endregion Private Functions
    }
}