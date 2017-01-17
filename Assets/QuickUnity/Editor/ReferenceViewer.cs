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

using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace QuickUnityEditor
{
    /// <summary>
    /// The viewer for finding assets references.
    /// </summary>
    /// <seealso cref="UnityEditor.EditorWindow"/>
    public class ReferenceViewer : EditorWindow
    {
        /// <summary>
        /// Dialog messasges collection.
        /// </summary>
        internal sealed class DialogMessages
        {
            /// <summary>
            /// Dialog message of no reference.
            /// </summary>
            public const string NoReferenceDialog = "Sorry, found no reference to the target asset!";
        }

        /// <summary>
        /// The singleton instance of ReferenceViewer.
        /// </summary>
        private static ReferenceViewer s_referenceViewer;

        /// <summary>
        /// Shows the reference viewer.
        /// </summary>
        [MenuItem("Assets/Reference Viewer...", false, 21)]
        public static void ShowViewer()
        {
            if (s_referenceViewer == null)
            {
                s_referenceViewer = CreateInstance<ReferenceViewer>();
                s_referenceViewer.titleContent = new GUIContent("Reference Viewer");
                s_referenceViewer.minSize = new Vector2(500, 300);
            }

            s_referenceViewer.ShowUtility();
            s_referenceViewer.Focus();
            s_referenceViewer.FindAllReferences();
        }

        /// <summary>
        /// The references map.
        /// </summary>
        private Dictionary<string, string[]> m_referencesMap;

        /// <summary>
        /// The references list map.
        /// </summary>
        private Dictionary<string, ReorderableList> m_referencesListMap;

        /// <summary>
        /// The position of scroll view.
        /// </summary>
        private Vector2 m_scrollViewPos;

        /// <summary>
        /// Finds all references.
        /// </summary>
        public void FindAllReferences()
        {
            List<string> targetAssets = new List<string>();

            string[] guids = Selection.assetGUIDs;

            for (int i = 0, length = guids.Length; i < length; ++i)
            {
                string guid = guids[i];
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                string[] paths = Utilities.EditorUtility.GetObjectAssets(assetPath);

                if (paths != null)
                {
                    targetAssets.AddRange(paths);
                }
            }

            m_referencesMap = Utilities.EditorUtility.GetDependenciesMap(targetAssets);

            if (m_referencesMap.Count > 0)
            {
                m_referencesListMap = GenerateReferencesListMap();
                Repaint();
            }
            else
            {
                QuickUnityEditorApplication.DisplaySimpleDialog("", DialogMessages.NoReferenceDialog);
                Close();
            }
        }

        #region Messages

        /// <summary>
        /// Draw editor GUI.
        /// </summary>
        private void OnGUI()
        {
            GUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();

            GUILayout.Space(20);

            m_scrollViewPos = EditorGUILayout.BeginScrollView(m_scrollViewPos);

            if (m_referencesListMap != null)
            {
                foreach (KeyValuePair<string, ReorderableList> kvp in m_referencesListMap)
                {
                    EditorGUILayout.BeginVertical();

                    EditorGUILayout.LabelField(kvp.Key);

                    ReorderableList list = kvp.Value;
                    list.DoLayoutList();

                    EditorGUILayout.EndVertical();
                }
            }

            EditorGUILayout.EndScrollView();

            GUILayout.Space(20);

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(20);
        }

        #endregion Messages

        /// <summary>
        /// Generates the references list map.
        /// </summary>
        /// <returns>Dictionary&lt;System.String, ReorderableList&gt; The references list map.</returns>
        private Dictionary<string, ReorderableList> GenerateReferencesListMap()
        {
            Dictionary<string, ReorderableList> listMap = new Dictionary<string, ReorderableList>();

            if (m_referencesMap != null)
            {
                foreach (KeyValuePair<string, string[]> kvp in m_referencesMap)
                {
                    if (kvp.Value.Length > 0)
                    {
                        ReorderableList list = new ReorderableList(kvp.Value, typeof(string[]), false, false, false, false);
                        //list.headerHeight = 2;
                        list.elementHeight = 16;

                        // Draw header.
                        list.drawHeaderCallback = (Rect rect) =>
                        {
                            EditorGUI.LabelField(rect, "References List");
                        };

                        // Draw list element.
                        list.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
                        {
                            string assetPath = (string)list.list[index];
                            EditorGUI.ObjectField(rect, "", AssetDatabase.LoadAssetAtPath<Object>(assetPath), typeof(Object), true);
                        };

                        listMap.Add(kvp.Key, list);
                    }
                }
            }

            return listMap;
        }
    }
}