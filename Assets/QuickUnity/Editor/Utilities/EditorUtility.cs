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

using System.IO;
using UnityEditor;
using UnityEngine;

namespace QuickUnityEditor.Utilities
{
    /// <summary>
    /// Miscellaneous helper stuff for Editor.
    /// </summary>
    public static class EditorUtility
    {
        /// <summary>
        /// The extension of ScriptableObject asset.
        /// </summary>
        private static readonly string scriptableObjectAssetExtension = ".asset";

        /// <summary>
        /// Loads the asset of ScriptableObject.
        /// </summary>
        /// <typeparam name="T">The type definition of ScriptableObject.</typeparam>
        /// <param name="path">The path.</param>
        /// <param name="assetName">Name of the asset.</param>
        /// <returns>The ScriptableObject of type definition.</returns>
        public static T LoadScriptableObjectAsset<T>(string path, string assetName = null) where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(assetName))
            {
                assetName = typeof(T).Name;
            }

            string assetPath = Path.Combine(path, assetName + scriptableObjectAssetExtension);
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        /// <summary>
        /// Creates the asset of ScriptableObject.
        /// </summary>
        /// <typeparam name="T">The type definition of ScriptableObject.</typeparam>
        /// <param name="path">The path.</param>
        /// <param name="assetName">Name of the asset.</param>
        /// <returns>The new instance of ScriptableObject of type definition.</returns>
        public static T CreateScriptableObjectAsset<T>(string path, string assetName = null) where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();
            SaveScriptableObjectAsset(path, asset, assetName);
            return asset;
        }

        /// <summary>
        /// Saves the asset of ScriptableObject.
        /// </summary>
        /// <typeparam name="T">The type definition of ScriptableObject.</typeparam>
        /// <param name="path">The path.</param>
        /// <param name="scriptableObject">The object of ScriptableObject.</param>
        /// <param name="assetName">Name of the asset.</param>
        public static void SaveScriptableObjectAsset<T>(string path, T scriptableObject, string assetName = null) where T : ScriptableObject
        {
            if (!string.IsNullOrEmpty(path))
            {
                int index = path.IndexOf("Assets");

                if (index == 0)
                {
                    if (string.IsNullOrEmpty(assetName))
                    {
                        assetName = typeof(T).Name;
                    }

                    assetName += scriptableObjectAssetExtension;
                    string assetPath = Path.Combine(path, assetName);

                    T targetAsset = AssetDatabase.LoadAssetAtPath<T>(assetPath);

                    if (targetAsset != null)
                    {
                        targetAsset = scriptableObject;
                        UnityEditor.EditorUtility.SetDirty(targetAsset);
                    }
                    else
                    {
                        AssetDatabase.CreateAsset(scriptableObject, assetPath);
                    }

                    AssetDatabase.Refresh();
                    AssetDatabase.SaveAssets();
                }
            }
        }

        /// <summary>
        /// Converts absolute path to asset path.
        /// </summary>
        /// <param name="absolutePath">The absolute path.</param>
        /// <returns>The asset path for project.</returns>
        public static string ConvertToAssetPath(string absolutePath)
        {
            if (!string.IsNullOrEmpty(absolutePath))
            {
                int index = absolutePath.IndexOf("Assets");

                if (index != -1)
                {
                    return absolutePath.Substring(index);
                }
            }

            return absolutePath;
        }
    }
}