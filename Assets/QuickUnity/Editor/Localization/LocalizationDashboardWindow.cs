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

namespace QuickUnity.Editor.Localization
{
    /// <summary>
    /// The Localization Dashboard Window.
    /// </summary>
    /// <seealso cref="UnityEditor.EditorWindow"/>
    public class LocalizationDashboardWindow : EditorWindow
    {
        private const int TopPadding = 2;

        /// <summary>
        /// The module list.
        /// </summary>
        private ReorderableList m_moduleList;

        private void OnEnable()
        {
            List<LocalizationModule> list = new List<LocalizationModule>();
            m_moduleList = new ReorderableList(list, typeof(LocalizationModule), true, true, true, true);

            m_moduleList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "Modules");
            m_moduleList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                rect.y += TopPadding;
                rect.height = EditorGUIUtility.singleLineHeight;

                EditorGUI.LabelField(rect, list[index].name);
            };
        }

        /// <summary>
        /// GUI Implementation.
        /// </summary>
        private void OnGUI()
        {
            if (m_moduleList != null)
            {
                m_moduleList.DoLayoutList();
            }
        }
    }
}