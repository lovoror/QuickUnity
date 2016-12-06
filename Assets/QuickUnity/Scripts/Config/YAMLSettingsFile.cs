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

using QuickUnity.Core.Miscs;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace QuickUnity.Config
{
    /// <summary>
    /// Contains methods for processing the YAML format settings file.
    /// </summary>
    public class YAMLSettingsFile
    {
        /// <summary>
        /// The file extension of setting file.
        /// </summary>
        private const string SettingsFileExtension = ".asset";

        /// <summary>
        /// Serializes the specified object at file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="source">The specified obejct.</param>
        /// <param name="autoGenerateFileName">if set to <c>true</c> [automatic generate file name].</param>
        public static void Serialize(string filePath, object source, bool autoGenerateFileName = true)
        {
            if (string.IsNullOrEmpty(filePath) || source == null)
            {
                return;
            }

            try
            {
                if (autoGenerateFileName)
                {
                    string fileName = source.GetType().Name + SettingsFileExtension;
                    filePath = Path.Combine(filePath, fileName);
                }

                StreamWriter writer = File.CreateText(filePath);
                Serializer serializer = new Serializer();
                serializer.Serialize(writer, source);
                writer.Close();
            }
            catch (Exception exception)
            {
                DebugLogger.LogException(exception);
            }
        }

        /// <summary>
        /// Deserializes the specified file path.
        /// </summary>
        /// <typeparam name="T">The type definition of the object deserialized.</typeparam>
        /// <param name="filePath">The file path.</param>
        /// <param name="autoParseFileName">if set to <c>true</c> [automatic parse file name].</param>
        /// <returns>T The object deserialized.</returns>
        public static T Deserialize<T>(string filePath, bool autoParseFileName = true)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    if (autoParseFileName)
                    {
                        string fileName = typeof(T).Name + SettingsFileExtension;
                        filePath = Path.Combine(filePath, fileName);
                    }

                    StreamReader reader = File.OpenText(filePath);
                    Deserializer deserializer = new Deserializer();
                    T obj = deserializer.Deserialize<T>(reader);
                    reader.Close();
                    return obj;
                }
                catch (Exception exception)
                {
                    DebugLogger.LogException(exception);
                }
            }

            return default(T);
        }
    }
}