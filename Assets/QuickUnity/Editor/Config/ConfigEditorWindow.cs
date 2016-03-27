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

using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The configuration editor window.
    /// </summary>
    public class ConfigEditorWindow : EditorWindow
    {
        /// <summary>
        /// The debug mode.
        /// </summary>
        private bool m_debugMode;

        /// <summary>
        /// The primary key.
        /// </summary>
        private string m_primaryKey;

        /// <summary>
        /// The key row index.
        /// </summary>
        private int m_keyRowIndex;

        /// <summary>
        /// The type row index.
        /// </summary>
        private int m_typeRowIndex;

        /// <summary>
        /// The comments row index.
        /// </summary>
        private int m_commentsRowIndex;

        /// <summary>
        /// The data start row index.
        /// </summary>
        private int m_dataStartRowIndex;

        /// <summary>
        /// The metadata namespace.
        /// </summary>
        private string m_metadataNamespace;

        /// <summary>
        /// The list separator.
        /// </summary>
        private string m_listSeparator;

        /// <summary>
        /// The Key/Value separator.
        /// </summary>
        private string m_kvSeparator;

        /// <summary>
        /// The excel files path.
        /// </summary>
        private string m_excelFilesPath;

        /// <summary>
        /// The script files path.
        /// </summary>
        private string m_scriptFilesPath;

        /// <summary>
        /// The database files path.
        /// </summary>
        private string m_dbFilesPath;

        /// <summary>
        /// The mark whether the editor is compiling.
        /// </summary>
        private bool m_isCompiling = false;

        #region Messages

        /// <summary>
        /// Called when [GUI].
        /// </summary>
        private void OnGUI()
        {
            // Get data.
            m_debugMode = ConfigEditor.debugMode;
            m_primaryKey = ConfigEditor.primaryKey;
            m_keyRowIndex = ConfigEditor.keyRowIndex;
            m_typeRowIndex = ConfigEditor.typeRowIndex;
            m_commentsRowIndex = ConfigEditor.commentsRowIndex;
            m_dataStartRowIndex = ConfigEditor.dataStartRowIndex;
            m_metadataNamespace = ConfigEditor.metadataNamespace;
            m_listSeparator = ConfigEditor.listSeparator;
            m_excelFilesPath = ConfigEditor.excelFilesPath;
            m_scriptFilesPath = ConfigEditor.scriptFilesPath;
            m_dbFilesPath = ConfigEditor.databaseFilesPath;

            // Debug mode.
            GUILayout.BeginVertical();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(58);
            GUILayout.Label("Debug Mode: ");
            GUILayout.Space(7);
            m_debugMode = EditorGUILayout.Toggle(m_debugMode, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Primary key set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(54);
            GUILayout.Label("Primary  Key: ");
            GUILayout.Space(7);
            m_primaryKey = EditorGUILayout.TextField(m_primaryKey, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Key row index set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(42);
            GUILayout.Label("Key Row Index: ");
            GUILayout.Space(7);
            m_keyRowIndex = EditorGUILayout.IntField(m_keyRowIndex, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Type row index set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(36);
            GUILayout.Label("Type Row Index: ");
            GUILayout.Space(7);
            m_typeRowIndex = EditorGUILayout.IntField(m_typeRowIndex, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Comments row index set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(2);
            GUILayout.Label("Comments Row Index: ");
            GUILayout.Space(7);
            m_commentsRowIndex = EditorGUILayout.IntField(m_commentsRowIndex, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Data start row index set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(6);
            GUILayout.Label("Data Start Row Index: ");
            GUILayout.Space(7);
            m_dataStartRowIndex = EditorGUILayout.IntField(m_dataStartRowIndex, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Namespace.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(8);
            GUILayout.Label("Metadata Namespace: ");
            GUILayout.Space(7);
            m_metadataNamespace = EditorGUILayout.TextField(m_metadataNamespace, GUILayout.Width(275f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // List separator.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(50);
            GUILayout.Label("List Separator: ");
            GUILayout.Space(7);
            m_listSeparator = EditorGUILayout.TextField(m_listSeparator, GUILayout.Width(100f));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            // Excel files path set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(42);
            GUILayout.Label("Excel Files Path: ");
            EditorGUI.BeginDisabledGroup(true);
            m_excelFilesPath = EditorGUILayout.TextField(m_excelFilesPath, GUILayout.Width(350f));
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10f);
            if (GUILayout.Button("Browse"))
            {
                ConfigEditor.excelFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("Load excel files of Directory", "", "");
            }
            GUILayout.EndHorizontal();

            // Script files path set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(38);
            GUILayout.Label("Script Files Path: ");
            EditorGUI.BeginDisabledGroup(true);
            m_scriptFilesPath = EditorGUILayout.TextField(m_scriptFilesPath, GUILayout.Width(350f));
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10f);
            if (GUILayout.Button("Browse"))
            {
                ConfigEditor.scriptFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("VO script files of Directory", "Assets/Scripts", "");
            }
            GUILayout.EndHorizontal();

            // Database files path set.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Space(54);
            GUILayout.Label("DB Files Path: ");
            EditorGUI.BeginDisabledGroup(true);
            m_dbFilesPath = EditorGUILayout.TextField(m_dbFilesPath, GUILayout.Width(350f));
            EditorGUI.EndDisabledGroup();
            GUILayout.Space(10f);
            if (GUILayout.Button("Browse"))
            {
                ConfigEditor.databaseFilesPath = UnityEditor.EditorUtility.OpenFolderPanel("Database files of Directory You Want to Save", "Assets", "");
            }
            GUILayout.EndHorizontal();

            // Button bar.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal(GUILayout.Width(300));
            GUILayout.Space(150);
            if (GUILayout.Button("Clear All Paths Set"))
            {
                ConfigEditor.excelFilesPath = string.Empty;
                ConfigEditor.scriptFilesPath = string.Empty;
                ConfigEditor.databaseFilesPath = string.Empty;
            }
            GUILayout.Space(30);
            if (GUILayout.Button("Generate Metadata"))
            {
                ConfigEditor.GenerateConfigMetadata();
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            // Save data.
            ConfigEditor.debugMode = m_debugMode;
            ConfigEditor.primaryKey = m_primaryKey;
            ConfigEditor.metadataNamespace = m_metadataNamespace;
            ConfigEditor.listSeparator = m_listSeparator;
            ConfigEditor.keyRowIndex = m_keyRowIndex;
            ConfigEditor.typeRowIndex = m_typeRowIndex;
            ConfigEditor.commentsRowIndex = m_commentsRowIndex;
            ConfigEditor.dataStartRowIndex = m_dataStartRowIndex;
        }

        /// <summary>
        /// Called 100 times per second on all visible windows.
        /// </summary>
        private void Update()
        {
            if (m_isCompiling && !EditorApplication.isCompiling)
            {
                // Complete compile.
                ConfigEditor.OnEditorCompleteCompile();
            }

            m_isCompiling = EditorApplication.isCompiling;
        }

        #endregion Messages
    }
}