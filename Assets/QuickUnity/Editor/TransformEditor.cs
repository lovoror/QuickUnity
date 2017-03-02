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

namespace QuickUnityEditor
{
    /// <summary>
    /// Custom Editor for Transform.
    /// </summary>
    /// <seealso cref="UnityEditor.Editor"/>
    [CustomEditor(typeof(Transform))]
    [CanEditMultipleObjects]
    internal class TransformEditor : Editor
    {
        private static readonly string[] s_coordinateButtonTexts = new string[] { "Local", "Global" };

        private bool m_showLocalCoordinate = true;

        private bool m_showGlobalCoordinate = true;

        private int m_selectedIndex;

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();

            // Draw button bar to select local coordinate or global coordinate.
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            m_selectedIndex = GUILayout.Toolbar(m_selectedIndex, s_coordinateButtonTexts, GUILayout.Width(300));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);

            // Draw properties.
            Transform targetObject = target as Transform;
            Undo.RecordObject(targetObject, "Transform Change");

            if (targetObject)
            {
                if (m_selectedIndex == 0)
                {
                    // Draw local coordinate properties.
                    targetObject.localPosition = EditorGUILayout.Vector3Field(new GUIContent("Local Position", "Position of the transform relative to the parent transform."),
                        targetObject.localPosition);

                    targetObject.localEulerAngles = EditorGUILayout.Vector3Field(new GUIContent("Local Rotation", "The rotation as Euler angles in degrees relative to the parent transform's rotation."),
                        targetObject.localRotation.eulerAngles);

                    targetObject.localScale = EditorGUILayout.Vector3Field(new GUIContent("Local Scale", "The scale of the transform relative to the parent."),
                        targetObject.localScale);
                }
                else
                {
                    // Draw global coordinate properties.
                    targetObject.position = EditorGUILayout.Vector3Field(new GUIContent("Position", "The position of the transform in world space."),
                        targetObject.position);

                    targetObject.eulerAngles = EditorGUILayout.Vector3Field(new GUIContent("Rotation", "The rotation as Euler angles in degrees."),
                        targetObject.rotation.eulerAngles);

                    GUI.enabled = false;
                    EditorGUILayout.Vector3Field(new GUIContent("Scale", "The global scale of the object (Read Only)."),
                        targetObject.lossyScale);
                    GUI.enabled = true;
                }
            }

            EditorGUILayout.EndVertical();
        }
    }
}