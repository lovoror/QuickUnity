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

namespace QuickUnityEditor.Data
{
    /// <summary>
    /// Handle the process of data tables import.
    /// </summary>
    public static class DataImport
    {
        /// <summary>
        /// The message content of dataTableRowScriptsStorageLocation is null or empty.
        /// </summary>
        private const string DataTableRowScriptsStorageLocationNullMessage = "Please set the storage location of DataTableRow scripts first: [QuickUnity/DataTable/Preferences...]";

        /// <summary>
        /// Imports data.
        /// </summary>
        [MenuItem("QuickUnity/DataTable/Import Data...", false, 100)]
        [MenuItem("Assets/Import Data...", false, 20)]
        public static void Import()
        {
            if (CheckPreferencesData())
            {
            }
        }

        /// <summary>
        /// Checks the preferences data.
        /// </summary>
        /// <returns><c>true</c> if preferences data is ready, <c>false</c> otherwise.</returns>
        private static bool CheckPreferencesData()
        {
            bool success = false;

            // Load preferences data or create default preferences data.
            DataTablePreferences preferencesData = DataTablePreferencesWindow.LoadPreferencesData();

            if (!preferencesData)
            {
                preferencesData = DataTablePreferencesWindow.CreateDefaultPreferencesData();
                DataTablePreferencesWindow.SavePreferenceData(preferencesData);
            }

            // Check preferences data is safe.
            if (string.IsNullOrEmpty(preferencesData.dataTableRowScriptsStorageLocation))
            {
                if (EditorUtility.DisplayDialog("", DataTableRowScriptsStorageLocationNullMessage, QuickUnityEditorApplication.OkButtonLabel))
                {
                    DataTablePreferencesWindow.ShowEditorWindow();
                }
            }

            return success;
        }
    }
}