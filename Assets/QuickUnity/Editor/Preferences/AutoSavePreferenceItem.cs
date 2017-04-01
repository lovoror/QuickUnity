/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2017 Jerry Lee
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

namespace QuickUnityEditor.Preferences
{
    /// <summary>
    /// This class provide method for adding preference section "Auto Save".
    /// </summary>
    public sealed class AutoSavePreferenceItem
    {
        /// <summary>
        /// The collections of GUI contents.
        /// </summary>
        private static class Styles
        {
            /// <summary>
            /// The style of field isAutoSaveEnabled.
            /// </summary>
            public static readonly GUIContent isAutoSaveEnabledStyle = Utilities.EditorGUIUtility.TextContent("Enable AutoSave", "Whether to automatically save after a time interval");

            /// <summary>
            /// The style of field isAutoSaveScenesEnabled.
            /// </summary>
            public static readonly GUIContent isAutoSaveScenesEnabledStyle = Utilities.EditorGUIUtility.TextContent("Save Scenes", "Whether to automatically save scenes during an autosave");

            /// <summary>
            /// The style of field isAutoSaveAssetsEnabled.
            /// </summary>
            public static readonly GUIContent isAutoSaveAssetsEnabledStyle = Utilities.EditorGUIUtility.TextContent("Save Assets", "Whether to automatically save assets during an autosave");

            /// <summary>
            /// The style of field frequencyInMinutes.
            /// </summary>
            public static readonly GUIContent frequencyInMinutesStyle = Utilities.EditorGUIUtility.TextContent("Frequency in Minutes", "The time interval after which to auto save");

            /// <summary>
            /// The style of field askWhenSaving.
            /// </summary>
            public static readonly GUIContent askWhenSavingStyle = Utilities.EditorGUIUtility.TextContent("Ask When Saving", "Whether to show confirm dialog when saving");
        }

        /// <summary>
        /// Add preferences section named "Auto Save" to the Preferences Window.
        /// </summary>
        [PreferenceItem("Auto Save")]
        public static void PreferenceGUI()
        {
            // AutoSave toggle button.
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            AutoSave.instance.isAutoSaveEnabled = EditorGUILayout.Toggle(Styles.isAutoSaveEnabledStyle, AutoSave.instance.isAutoSaveEnabled);
            GUILayout.EndVertical();

            EditorGUI.BeginDisabledGroup(!AutoSave.instance.isAutoSaveEnabled);

            // AutoSave scenes toggle button.
            GUILayout.BeginVertical();
            AutoSave.instance.isAutoSaveScenesEnabled = EditorGUILayout.Toggle("Save Scenes", AutoSave.instance.isAutoSaveScenesEnabled);
            GUILayout.EndVertical();

            // AutoSave assets toggle button.
            GUILayout.BeginVertical();
            AutoSave.instance.isAutoSaveAssetsEnabled = EditorGUILayout.Toggle("Save Assets", AutoSave.instance.isAutoSaveAssetsEnabled);
            GUILayout.EndVertical();

            // AutoSave time minutes int value field.
            GUILayout.BeginVertical();
            int value = EditorGUILayout.IntField(Styles.frequencyInMinutesStyle, (int)AutoSave.instance.autoSaveTimeMinutes);

            if (value >= 1)
            {
                AutoSave.instance.autoSaveTimeMinutes = (uint)value;
            }
            else
            {
                AutoSave.instance.autoSaveTimeMinutes = 1;
            }

            GUILayout.EndVertical();

            // AutoSave warning seconds int value slider field.
            GUILayout.BeginVertical();
            AutoSave.instance.askWhenSaving = EditorGUILayout.Toggle(Styles.askWhenSavingStyle, AutoSave.instance.askWhenSaving);
            GUILayout.EndVertical();

            EditorGUI.EndDisabledGroup();
        }
    }
}