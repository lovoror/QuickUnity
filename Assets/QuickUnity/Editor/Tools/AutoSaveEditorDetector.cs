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
    /// Detector of auto-save.
    /// </summary>
    [InitializeOnLoad]
    public static class AutoSaveEditorDetector
    {
        /// <summary>
        /// The next save time.
        /// </summary>
        private static double s_nextSaveTime = 0.0d;

        /// <summary>
        /// Initializes static members of the <see cref="AutoSaveEditorDetector"/> class.
        /// </summary>
        static AutoSaveEditorDetector()
        {
            s_nextSaveTime = EditorApplication.timeSinceStartup;
            EditorApplication.update += OnUpdate;
        }

        /// <summary>
        /// Resets the save time.
        /// </summary>
        public static void ResetSaveTime()
        {
            s_nextSaveTime = EditorApplication.timeSinceStartup;
        }

        /// <summary>
        /// Called when [ editor application update].
        /// </summary>
        private static void OnUpdate()
        {
            if (AutoSave.autoSaveEnabled)
            {
                if (EditorApplication.timeSinceStartup >= s_nextSaveTime)
                {
                    // Automatically save current scene.
                    if (AutoSave.saveCurrentSceneEnabled && !string.IsNullOrEmpty(EditorApplication.currentScene))
                        EditorApplication.SaveScene(EditorApplication.currentScene);

                    // Automatically save project.
                    if (AutoSave.saveProjectEnabled)
                        EditorApplication.SaveAssets();

                    s_nextSaveTime = EditorApplication.timeSinceStartup + AutoSave.autoSaveInterval;
                }
            }
        }
    }
}