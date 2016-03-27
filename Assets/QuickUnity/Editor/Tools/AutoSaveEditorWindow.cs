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

namespace QuickUnity.Editor.Tools
{
    /// <summary>
    /// AutoSaveEditorWindow help you to set up parameters about saving scene and assets automatically.
    /// </summary>
    public class AutoSaveEditorWindow : EditorWindow
    {
        /// <summary>
        /// The automatic save enabled
        /// </summary>
        private bool m_autoSaveEnabled;

        /// <summary>
        /// Whether saving current scene.
        /// </summary>
        private bool m_saveCurrentSceneEnabled;

        /// <summary>
        /// Whether saving project.
        /// </summary>
        private bool m_saveProjectEnabled;

        /// <summary>
        /// The interval time of automatic save.
        /// </summary>
        private int m_autoSaveInterval = 180;

        /// <summary>
        /// The mark of reseting save time
        /// </summary>
        private bool m_resetSaveTime = false;

        /// <summary>
        /// Editor GUI code.
        /// </summary>
        private void OnGUI()
        {
            // Show autosave enabled toggle GUI.
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            m_autoSaveEnabled = EditorGUILayout.ToggleLeft("Enable AutoSave", m_autoSaveEnabled);
            GUILayout.EndVertical();

            EditorGUI.BeginDisabledGroup(!m_autoSaveEnabled);

            // Show toggle GUI of enabling save current scene.
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            m_saveCurrentSceneEnabled = EditorGUILayout.ToggleLeft("Enable save current scene", m_saveCurrentSceneEnabled);
            GUILayout.EndVertical();

            // Show toggle GUI of enabling save project.
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            m_saveProjectEnabled = EditorGUILayout.ToggleLeft("Enable save project", m_saveProjectEnabled);
            GUILayout.EndVertical();

            // Show interval time of automatic save.
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Interval time");
            m_autoSaveInterval = EditorGUILayout.IntField(m_autoSaveInterval, GUILayout.Width(50f));
            GUILayout.Label("seconds");
            GUILayout.Space(150f);
            GUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();

            // Whether tell AutoSaveEditorDetector to reset save time.
            if (m_autoSaveInterval != AutoSave.autoSaveInterval)
                m_resetSaveTime = true;

            // Save data.
            AutoSave.autoSaveEnabled = m_autoSaveEnabled;
            AutoSave.saveCurrentSceneEnabled = m_saveCurrentSceneEnabled;
            AutoSave.saveProjectEnabled = m_saveProjectEnabled;
            AutoSave.autoSaveInterval = m_autoSaveInterval;
        }

        /// <summary>
        /// Called when [focus].
        /// </summary>
        private void OnFocus()
        {
            m_autoSaveEnabled = AutoSave.autoSaveEnabled;
            m_saveCurrentSceneEnabled = AutoSave.saveCurrentSceneEnabled;
            m_saveProjectEnabled = AutoSave.saveProjectEnabled;
            m_autoSaveInterval = AutoSave.autoSaveInterval;
        }

        /// <summary>
        /// Called when [destroy].
        /// </summary>
        private void OnDestroy()
        {
            if (m_resetSaveTime)
                AutoSaveEditorDetector.ResetSaveTime();
        }
    }
}