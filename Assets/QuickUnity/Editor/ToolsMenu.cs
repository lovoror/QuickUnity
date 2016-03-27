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

using QuickUnity.Editor.Tools;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/Tools menu options to the Unity Editor. This class cannot be inherited.
    /// </summary>
    public static class ToolsMenu
    {
        /// <summary>
        /// The automatic save editor window
        /// </summary>
        private static AutoSaveEditorWindow s_autoSaveEditorWindow;

        /// <summary>
        /// Gets the automatic save editor window.
        /// </summary>
        /// <value>The automatic save editor window.</value>
        public static AutoSaveEditorWindow autoSaveEditorWindow
        {
            get { return s_autoSaveEditorWindow; }
        }

        /// <summary>
        /// Shows AutoSave editor window.
        /// </summary>
        [MenuItem("QuickUnity/Tools/AutoSave")]
        private static void ShowAutoSaveEditorWindow()
        {
            if (s_autoSaveEditorWindow == null)
                s_autoSaveEditorWindow = EditorWindow.GetWindowWithRect<AutoSaveEditorWindow>(new Rect(0, 0, 256, 120), true, "AutoSave Editor");

            s_autoSaveEditorWindow.Show();
        }
    }
}