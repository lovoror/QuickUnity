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

using QuickUnity.Editor.Localization;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/Localization Dashboard menu item to the Unity Editor.
    /// </summary>
    /// <seealso cref="UnityEngine.ScriptableObject"/>
    public class LocalizationDashboardMenu : ScriptableObject
    {
        /// <summary>
        /// The menu item priority.
        /// </summary>
        public const int MenuItemPriority = HelpMenu.MenuItemPriority - QuickUnityEditor.MenuItemSeparatedNumber;

        /// <summary>
        /// Shows the localization dashboard window.
        /// </summary>
        [MenuItem("QuickUnity/Localization Dashboard", false, MenuItemPriority)]
        private static void ShowLocalizationDashboardWindow()
        {
            LocalizationDashboardWindow window = EditorWindow.GetWindow<LocalizationDashboardWindow>(
                false, "Localization");
            EditorWindow.FocusWindowIfItsOpen<LocalizationDashboardWindow>();
            window.Show();
        }
    }
}