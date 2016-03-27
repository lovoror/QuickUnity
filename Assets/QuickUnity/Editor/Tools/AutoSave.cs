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

namespace QuickUnity.Editor.Tools
{
    /// <summary>
    /// AutoSave parameters. This class cannot be inherited.
    /// </summary>
    public static class AutoSave
    {
        /// <summary>
        /// Gets or sets a value indicating whether [automatic save enabled].
        /// </summary>
        /// <value><c>true</c> if [automatic save enabled]; otherwise, <c>false</c>.</value>
        public static bool autoSaveEnabled
        {
            get { return EditorPrefs.GetBool(EditorUtility.projectRootDirName + ".AutoSave.autoSaveEnabled"); }
            set { EditorPrefs.SetBool(EditorUtility.projectRootDirName + ".AutoSave.autoSaveEnabled", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [save current scene enabled].
        /// </summary>
        /// <value><c>true</c> if [save current scene enabled]; otherwise, <c>false</c>.</value>
        public static bool saveCurrentSceneEnabled
        {
            get { return EditorPrefs.GetBool(EditorUtility.projectRootDirName + ".AutoSave.saveCurrentSceneEnabled"); }
            set { EditorPrefs.SetBool(EditorUtility.projectRootDirName + ".AutoSave.saveCurrentSceneEnabled", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [save project enabled].
        /// </summary>
        /// <value><c>true</c> if [save project enabled]; otherwise, <c>false</c>.</value>
        public static bool saveProjectEnabled
        {
            get { return EditorPrefs.GetBool(EditorUtility.projectRootDirName + ".AutoSave.saveProjectEnabled"); }
            set { EditorPrefs.SetBool(EditorUtility.projectRootDirName + ".AutoSave.saveProjectEnabled", value); }
        }

        /// <summary>
        /// Gets or sets the automatic save interval time.
        /// </summary>
        /// <value>The automatic save interval.</value>
        public static int autoSaveInterval
        {
            get { return EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".AutoSave.autoSaveInterval"); }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".AutoSave.autoSaveInterval", value); }
        }
    }
}