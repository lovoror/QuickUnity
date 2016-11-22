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

using QuickUnity.Data;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace QuickUnityEditor.Data
{
    /// <summary>
    /// The editor window of DataTable Preferences.
    /// </summary>
    /// <seealso cref="UnityEditor.EditorWindow"/>
    public class DataTablePreferencesWindow : EditorWindow
    {
        /// <summary>
        /// The collections of GUI contents.
        /// </summary>
        private static class Styles
        {
            /// <summary>
            /// The style of field dataTablesStorageLocation.
            /// </summary>
            public static readonly GUIContent dataTablesStorageLocationStyle = Utilities.EditorGUIUtility.TextContent("DataTables Storage Location", "Where to store data tables.");

            /// <summary>
            /// The style of field dataRowScriptsStorageLocation.
            /// </summary>
            public static readonly GUIContent dataRowScriptsStorageLocationStyle = Utilities.EditorGUIUtility.TextContent("DataTableRow Scripts Storage Location", "Where to store DataTableRow scripts.");

            /// <summary>
            /// The style of field autoGenerateScriptsNamespace.
            /// </summary>
            public static readonly GUIContent autoGenerateScriptsNamespaceStyle = Utilities.EditorGUIUtility.TextContent("Auto Generate Scripts Namespace", "Whether to generate namespace automatically.");

            /// <summary>
            /// The style of field dataTableRowScriptsNamespace.
            /// </summary>
            public static readonly GUIContent dataTableRowScriptsNamespaceStyle = Utilities.EditorGUIUtility.TextContent("DataTableRow Scripts Namespace", "The namespace of DataTableRow scripts.");
        }

        /// <summary>
        /// The resources path.
        /// </summary>
        private static readonly string s_resourcesPath = Path.Combine("Assets", "Resources");

        /// <summary>
        /// The editor window instance.
        /// </summary>
        private static DataTablePreferencesWindow s_editorWindow;

        /// <summary>
        /// Shows the editor window.
        /// </summary>
        [MenuItem("QuickUnity/DataTable/Preferences...", false, 102)]
        public static void ShowEditorWindow()
        {
            if (s_editorWindow == null)
            {
                s_editorWindow = CreateInstance<DataTablePreferencesWindow>();
                s_editorWindow.titleContent = new GUIContent("DataTable Preferences");
                s_editorWindow.minSize = new Vector2(500, 100);
            }

            s_editorWindow.ShowUtility();
        }

        /// <summary>
        /// Loads the preferences data.
        /// </summary>
        /// <returns>The loaded preferences data.</returns>
        public static DataTablePreferences LoadPreferencesData()
        {
            if (!AssetDatabase.IsValidFolder(s_resourcesPath))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            return Utilities.EditorUtility.LoadScriptableObjectAsset<DataTablePreferences>(s_resourcesPath);
        }

        /// <summary>
        /// Creates the default preferences data.
        /// </summary>
        /// <returns>The default preferences data.</returns>
        public static DataTablePreferences CreateDefaultPreferencesData()
        {
            return Utilities.EditorUtility.CreateScriptableObjectAsset<DataTablePreferences>(s_resourcesPath);
        }

        /// <summary>
        /// Saves the preference data.
        /// </summary>
        /// <param name="preferencesData">The preferences data.</param>
        public static void SavePreferenceData(DataTablePreferences preferencesData)
        {
            Utilities.EditorUtility.SaveScriptableObjectAsset(s_resourcesPath, preferencesData);
        }

        /// <summary>
        /// The preferences data
        /// </summary>
        private DataTablePreferences m_preferencesData;

        #region Messages

        /// <summary>
        /// Called as the new window is opened.
        /// </summary>
        private void Awake()
        {
            DataTablePreferences preferencesData = LoadPreferencesData();

            if (preferencesData != null)
            {
                m_preferencesData = preferencesData;
            }
            else
            {
                m_preferencesData = CreateDefaultPreferencesData();
            }
        }

        /// <summary>
        /// Editor GUI.
        /// </summary>
        private void OnGUI()
        {
            CalculateLabelWidth();

            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            m_preferencesData.dataTablesStorageLocation = (DataTableStorageLocation)EditorGUILayout.EnumPopup(Styles.dataTablesStorageLocationStyle, m_preferencesData.dataTablesStorageLocation);
            GUILayout.Space(2.5f);
            m_preferencesData.dataTableRowScriptsStorageLocation = Utilities.EditorGUIUtility.FolderField(Styles.dataRowScriptsStorageLocationStyle, m_preferencesData.dataTableRowScriptsStorageLocation, "Data Row Scripts Storage Location");
            m_preferencesData.dataTableRowScriptsStorageLocation = Utilities.EditorUtility.ConvertToAssetPath(m_preferencesData.dataTableRowScriptsStorageLocation);
            GUILayout.Space(2.5f);
            m_preferencesData.autoGenerateScriptsNamespace = EditorGUILayout.Toggle(Styles.autoGenerateScriptsNamespaceStyle, m_preferencesData.autoGenerateScriptsNamespace);
            GUILayout.Space(2.5f);
            EditorGUI.BeginDisabledGroup(m_preferencesData.autoGenerateScriptsNamespace);
            m_preferencesData.dataTableRowScriptsNamespace = EditorGUILayout.TextField(Styles.dataTableRowScriptsNamespaceStyle, m_preferencesData.dataTableRowScriptsNamespace);
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10);
            GUILayout.EndVertical();
            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// OnDestroy is called when the EditorWindow is closed.
        /// </summary>
        private void OnDestroy()
        {
            SavePreferenceData(m_preferencesData);
        }

        #endregion Messages

        /// <summary>
        /// Calculates the width of the label.
        /// </summary>
        private void CalculateLabelWidth()
        {
            SetEditorLabelWidth(EditorStyles.popup, Styles.dataTablesStorageLocationStyle);
            SetEditorLabelWidth(EditorStyles.label, Styles.dataRowScriptsStorageLocationStyle);
            SetEditorLabelWidth(EditorStyles.toggle, Styles.autoGenerateScriptsNamespaceStyle);
            SetEditorLabelWidth(EditorStyles.textField, Styles.dataTableRowScriptsNamespaceStyle);
        }

        /// <summary>
        /// Sets the width of the editor label.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <param name="content">The content.</param>
        private void SetEditorLabelWidth(GUIStyle style, GUIContent content)
        {
            float labelWidth = Utilities.EditorGUIUtility.GetLabelWidth(style, content);

            if (labelWidth > EditorGUIUtility.labelWidth)
            {
                EditorGUIUtility.labelWidth = labelWidth;
            }
        }
    }
}