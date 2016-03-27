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

using Excel;
using iBoxDB.LocalServer;
using QuickUnity.Config;
using QuickUnity.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace QuickUnity.Editor.Config
{
    /// <summary>
    /// The metadata head information struct.
    /// </summary>
    public struct MetadataHeadInfo
    {
        /// <summary>
        /// The key string.
        /// </summary>
        public string key;

        /// <summary>
        /// The type string.
        /// </summary>
        public string type;

        /// <summary>
        /// The parsed type.
        /// </summary>
        public string parsedType;

        /// <summary>
        /// The comment string.
        /// </summary>
        public string comment;
    }

    /// <summary>
    /// ConfigEditor handle all methods of config data.
    /// </summary>
    public static class ConfigEditor
    {
        /// <summary>
        /// The default primary key.
        /// </summary>
        private const string DefaultPrimaryKey = "id";

        /// <summary>
        /// The default metadata namespace.
        /// </summary>
        private const string DefaultMetadataNamespace = "QuickUnity.Config";

        /// <summary>
        /// The name of Config VO script file.
        /// </summary>
        private const string ConfigVOScriptTplFileName = "ConfigVOClassTpl";

        /// <summary>
        /// The search pattern of excel files.
        /// </summary>
        private const string ExcelFileSearchPattern = "*.xlsx";

        /// <summary>
        /// The default row index of key in table.
        /// </summary>
        private const int DefaultKeyRowIndex = 0;

        /// <summary>
        /// The default row index of type in table.
        /// </summary>
        private const int DefaultTypeRowIndex = 1;

        /// <summary>
        /// The default row index of comments in table.
        /// </summary>
        private const int DefaultCommentsRowIndex = 2;

        /// <summary>
        /// The start row index of data.
        /// </summary>
        private const int DefaultDataStartRowIndex = 3;

        /// <summary>
        /// The default list separator.
        /// </summary>
        private const string DefaultListSeparator = "|";

        /// <summary>
        /// The default Key/Value separator.
        /// </summary>
        private const string DefaultKVSeparator = "#";

        /// <summary>
        /// The supported type parsers.
        /// </summary>
        private static readonly Dictionary<string, Type> s_supportedTypeParsers = new Dictionary<string, Type>()
        {
            { "boolList", typeof(BoolListTypeParser) },
            { "bool", typeof(BoolTypeParser) },
            { "byteList", typeof(ByteListTypeParser) },
            { "byte", typeof(ByteTypeParser) },
            { "sbyteList", typeof(SByteListTypeParser) },
            { "sbyte", typeof(SByteTypeParser) },
            { "shortList", typeof(Int16ListTypeParser) },
            { "short", typeof(Int16TypeParser) },
            { "ushortList", typeof(UInt16ListTypeParser) },
            { "ushort", typeof(UInt16TypeParser) },
            { "intList", typeof(Int32ListTypeParser) },
            { "int", typeof(Int32TypeParser) },
            { "uintList", typeof(UInt32ListTypeParser) },
            { "uint", typeof(UInt32TypeParser) },
            { "longList", typeof(Int64ListTypeParser) },
            { "long", typeof(Int64TypeParser) },
            { "ulongList", typeof(UInt64ListTypeParser) },
            { "ulong", typeof(UInt64TypeParser) },
            { "floatList", typeof(FloatListTypeParser) },
            { "float", typeof(FloatTypeParser) },
            { "doubleList", typeof(DoubleListTypeParser) },
            { "double", typeof(DoubleTypeParser) },
            { "decimalList", typeof(DecimalListTypeParser) },
            { "decimal", typeof(DecimalTypeParser) },
            { "stringList", typeof(StringListTypeParser) },
            { "string", typeof(StringTypeParser) }
        };

        /// <summary>
        /// The type parsers.
        /// </summary>
        private static Dictionary<string, ITypeParser> s_typeParsers = new Dictionary<string, ITypeParser>();

        /// <summary>
        /// The table index local server.
        /// </summary>
        private static DB s_tableIndexServer;

        /// <summary>
        /// Gets the table index server.
        /// </summary>
        /// <value>
        /// The table index server.
        /// </value>
        public static DB tableIndexServer
        {
            get
            {
                if (s_tableIndexServer == null)
                {
                    s_tableIndexServer = new DB(ConfigMetadata.IndexTableLocalAddress);
                    DatabaseConfig.Config config = s_tableIndexServer.GetConfig();

                    if (config != null)
                    {
                        config.EnsureTable<MetadataLocalAddress>(MetadataLocalAddress.TableName, MetadataLocalAddress.PrimaryKey);
                        config.EnsureTable<ConfigParameter>(ConfigParameter.TableName, ConfigParameter.PrimaryKey);
                    }

                    s_tableIndexServer.MinConfig();
                }

                return s_tableIndexServer;
            }
        }

        /// <summary>
        /// The table index database.
        /// </summary>
        private static DB.AutoBox s_tableIndexDB;

        /// <summary>
        /// Gets the table index database.
        /// </summary>
        /// <value>
        /// The table index database.
        /// </value>
        public static DB.AutoBox tableIndexDB
        {
            get
            {
                if (s_tableIndexDB == null && tableIndexServer != null)
                    s_tableIndexDB = tableIndexServer.Open();

                return s_tableIndexDB;
            }
        }

        /// <summary>
        /// The metadata key format function.
        /// </summary>
        public static Func<string, string> metadataKeyFormatter;

        /// <summary>
        /// The metadata name format function.
        /// </summary>
        public static Func<string, string> metadataNameFormatter;

        /// <summary>
        /// Gets or sets a value indicating whether [debug mode].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [debug mode]; otherwise, <c>false</c>.
        /// </value>
        public static bool debugMode
        {
            get { return EditorPrefs.GetBool(EditorUtility.projectRootDirName + ".ConfigEditor.debugMode"); }
            set { EditorPrefs.SetBool(EditorUtility.projectRootDirName + ".ConfigEditor.debugMode", value); }
        }

        /// <summary>
        /// Gets or sets the primary key.
        /// </summary>
        /// <value>
        /// The primary key.
        /// </value>
        public static string primaryKey
        {
            get
            {
                string value = EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.primaryKey");

                if (string.IsNullOrEmpty(value))
                {
                    primaryKey = DefaultPrimaryKey;
                    value = DefaultPrimaryKey;
                }

                return value;
            }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.primaryKey", value); }
        }

        /// <summary>
        /// Gets or sets the index of the key row.
        /// </summary>
        /// <value>
        /// The index of the key row.
        /// </value>
        public static int keyRowIndex
        {
            get
            {
                int value = EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".ConfigEditor.keyRowIndex");

                if (value == 0)
                {
                    keyRowIndex = DefaultKeyRowIndex;
                    value = DefaultKeyRowIndex;
                }

                return value;
            }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".ConfigEditor.keyRowIndex", value); }
        }

        /// <summary>
        /// Gets or sets the index of the type row.
        /// </summary>
        /// <value>
        /// The index of the type row.
        /// </value>
        public static int typeRowIndex
        {
            get
            {
                int value = EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".ConfigEditor.typeRowIndex");

                if (value == 0)
                {
                    typeRowIndex = DefaultTypeRowIndex;
                    value = DefaultTypeRowIndex;
                }

                return value;
            }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".ConfigEditor.typeRowIndex", value); }
        }

        /// <summary>
        /// Gets or sets the index of the comments row.
        /// </summary>
        /// <value>
        /// The index of the comments row.
        /// </value>
        public static int commentsRowIndex
        {
            get
            {
                int value = EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".ConfigEditor.commentsRowIndex");

                if (value == 0)
                {
                    commentsRowIndex = DefaultCommentsRowIndex;
                    value = DefaultCommentsRowIndex;
                }

                return value;
            }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".ConfigEditor.commentsRowIndex", value); }
        }

        /// <summary>
        /// Gets or sets the index of the data start row.
        /// </summary>
        /// <value>
        /// The index of the data start row.
        /// </value>
        public static int dataStartRowIndex
        {
            get
            {
                int value = EditorPrefs.GetInt(EditorUtility.projectRootDirName + ".ConfigEditor.dataStartRowIndex");

                if (value == 0)
                {
                    dataStartRowIndex = DefaultDataStartRowIndex;
                    value = DefaultDataStartRowIndex;
                }

                return value;
            }
            set { EditorPrefs.SetInt(EditorUtility.projectRootDirName + ".ConfigEditor.dataStartRowIndex", value); }
        }

        /// <summary>
        /// Gets or sets the metadata namespace.
        /// </summary>
        /// <value>
        /// The metadata namespace.
        /// </value>
        public static string metadataNamespace
        {
            get
            {
                string value = EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.metadataNamespace");

                if (string.IsNullOrEmpty(value))
                {
                    metadataNamespace = DefaultMetadataNamespace;
                    value = DefaultMetadataNamespace;
                }

                return value;
            }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.metadataNamespace", value); }
        }

        /// <summary>
        /// Gets or sets the list separator.
        /// </summary>
        /// <value>
        /// The list separator.
        /// </value>
        public static string listSeparator
        {
            get
            {
                string value = EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.listSeparator");

                if (string.IsNullOrEmpty(value))
                {
                    listSeparator = DefaultListSeparator;
                    value = DefaultListSeparator;
                }

                return value;
            }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.listSeparator", value); }
        }

        /// <summary>
        /// Gets or sets the excel files path.
        /// </summary>
        /// <value>
        /// The excel files path.
        /// </value>
        public static string excelFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.excelFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.excelFilesPath", value); }
        }

        /// <summary>
        /// Gets or sets the script files path.
        /// </summary>
        /// <value>
        /// The script files path.
        /// </value>
        public static string scriptFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.scriptFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.scriptFilesPath", value); }
        }

        /// <summary>
        /// Gets or sets the database files path.
        /// </summary>
        /// <value>
        /// The database path.
        /// </value>
        public static string databaseFilesPath
        {
            get { return EditorPrefs.GetString(EditorUtility.projectRootDirName + ".ConfigEditor.databaseFilesPath"); }
            set { EditorPrefs.SetString(EditorUtility.projectRootDirName + ".ConfigEditor.databaseFilesPath", value); }
        }

        #region Public Static Functions

        /// <summary>
        /// Generates the configuration metadata.
        /// </summary>
        public static void GenerateConfigMetadata()
        {
            if (!ValidateGeneration())
                return;

            PrepareConfigMetadataGeneration();
            GenerateVOScripts();
        }

        /// <summary>
        /// Called when [editor complete compile].
        /// </summary>
        public static void OnEditorCompleteCompile()
        {
            CompleteMetadataGeneration();
        }

        #endregion Public Static Functions

        #region Private Static Functions

        /// <summary>
        /// Validates the generation.
        /// </summary>
        /// <returns></returns>
        private static bool ValidateGeneration()
        {
            if (string.IsNullOrEmpty(excelFilesPath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please set the path of excel files !", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(scriptFilesPath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please set the path of script files !", "OK");
                return false;
            }

            if (string.IsNullOrEmpty(databaseFilesPath))
            {
                UnityEditor.EditorUtility.DisplayDialog("Error", "Please set the path of database files !", "OK");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Prepares the configuration metadata generation.
        /// </summary>
        private static void PrepareConfigMetadataGeneration()
        {
            // Clear all logs.
            EditorUtility.ClearConsole();

            // Create database files directory.
            if (!Directory.Exists(databaseFilesPath))
                Directory.CreateDirectory(databaseFilesPath);

            // Clear database.
            EditorUtility.DeleteAllAssetsInDirectory(databaseFilesPath);

            // Delete old script files.
            EditorUtility.DeleteAllAssetsInDirectory(scriptFilesPath);
        }

        /// <summary>
        /// Generates the Value Object scripts.
        /// </summary>
        private static void GenerateVOScripts()
        {
            DirectoryInfo dirInfo = new DirectoryInfo(excelFilesPath);
            FileInfo[] fileInfos = dirInfo.GetFiles(ExcelFileSearchPattern);
            string tplText = EditorUtility.ReadTextAsset(ConfigVOScriptTplFileName);

            if (!string.IsNullOrEmpty(tplText))
            {
                for (int i = 0, length = fileInfos.Length; i < length; ++i)
                {
                    FileInfo fileInfo = fileInfos[i];

                    if (fileInfo != null)
                    {
                        string filePath = fileInfo.FullName;
                        string fileName = Path.GetFileNameWithoutExtension(fileInfo.Name);

                        // Format metadata name.
                        if (metadataNameFormatter != null)
                            fileName = metadataNameFormatter(fileName);

                        try
                        {
                            FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                            DataSet result = excelReader.AsDataSet();
                            DataTable table = result.Tables[0];
                            List<MetadataHeadInfo> headInfos = GenerateMetadataHeadInfos(table);
                            string fieldsString = GenerateVOFieldsString(headInfos);
                            string targetScriptPath = scriptFilesPath;

                            // Generate VO script files.
                            if (!string.IsNullOrEmpty(targetScriptPath))
                            {
                                targetScriptPath += Path.AltDirectorySeparatorChar + fileName + EditorUtility.ScriptFileExtensions;
                                string tplTextCopy = (string)tplText.Clone();
                                tplTextCopy = tplTextCopy.Replace("{$Namespace}", metadataNamespace);
                                tplTextCopy = tplTextCopy.Replace("{$ClassName}", fileName);
                                tplTextCopy = tplTextCopy.Replace("{$Fields}", fieldsString);
                                EditorUtility.WriteText(targetScriptPath, tplTextCopy);
                            }
                        }
                        catch (Exception exception)
                        {
                            Debug.LogError(string.Format("Error Message: {0}, Stack Trace: {1}", exception.Message, exception.StackTrace));
                        }
                    }

                    // Show progress of generation.
                    UnityEditor.EditorUtility.DisplayProgressBar("Holding on", "The progress of generating VO scripts.", (float)(i + 1) / length);
                }

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            }
            else
            {
                Debug.LogError(string.Format("Value Object script template file is null !"));
            }
        }

        /// <summary>
        /// Generates the metadata head informations.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns></returns>
        private static List<MetadataHeadInfo> GenerateMetadataHeadInfos(DataTable table)
        {
            DataColumnCollection columns = table.Columns;
            DataRowCollection rows = table.Rows;
            int columnCount = columns.Count;
            List<MetadataHeadInfo> headInfos = new List<MetadataHeadInfo>();

            for (int i = 0; i < columnCount; i++)
            {
                string key = rows[keyRowIndex][i].ToString().Trim();
                string typeString = rows[DefaultTypeRowIndex][i].ToString().Trim();
                string comment = rows[DefaultCommentsRowIndex][i].ToString().Trim();

                // Format key.
                if (metadataKeyFormatter != null)
                    key = metadataKeyFormatter(key);

                // If key is not empty string, type is not empty string, and type is supported,  then add item to list.
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(typeString) && IsSupportedDataType(typeString))
                {
                    ITypeParser typeParser = GetTypeParser(typeString);
                    MetadataHeadInfo headInfo = new MetadataHeadInfo();
                    headInfo.key = key;
                    headInfo.type = typeString;
                    headInfo.parsedType = typeParser.ParseType(typeString);
                    headInfo.comment = CommentFormatter(comment);
                    headInfos.Add(headInfo);
                }
            }

            return headInfos;
        }

        /// <summary>
        /// Generates the VO fields string.
        /// </summary>
        /// <param name="headInfos">The MetadataHeadInfo list.</param>
        /// <returns>The VO fields string.</returns>
        private static string GenerateVOFieldsString(List<MetadataHeadInfo> headInfos)
        {
            string fieldsString = string.Empty;

            if (headInfos != null && headInfos.Count > 0)
            {
                for (int i = 0, length = headInfos.Count; i < length; ++i)
                {
                    MetadataHeadInfo headInfo = headInfos[i];
                    fieldsString += string.Format("\t\t/// <summary>{0}\t\t/// {1}{2}\t\t/// </summary>{3}\t\tpublic {4} {5};",
                        Environment.newline,
                        headInfo.comment,
                        Environment.newline,
                        Environment.newline,
                        headInfo.parsedType,
                        headInfo.key);

                    if (i < length - 1)
                        fieldsString += Environment.newline + Environment.newline;
                }
            }

            return fieldsString;
        }

        /// <summary>
        /// Completes the metadata generation.
        /// </summary>
        private static void CompleteMetadataGeneration()
        {
            // Set the root path of database.
            DB.Root(databaseFilesPath);

            DirectoryInfo dirInfo = new DirectoryInfo(excelFilesPath);
            FileInfo[] fileInfos = dirInfo.GetFiles(ExcelFileSearchPattern);

            for (int i = 0, length = fileInfos.Length; i < length; ++i)
            {
                FileInfo fileInfo = fileInfos[i];

                if (fileInfo != null)
                {
                    string filePath = fileInfo.FullName;
                    string fileName = Path.GetFileNameWithoutExtension(fileInfo.Name);

                    // Format metadata name.
                    if (metadataNameFormatter != null)
                        fileName = metadataNameFormatter(fileName);

                    FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                    DataSet result = excelReader.AsDataSet();
                    DataTable table = result.Tables[0];
                    List<MetadataHeadInfo> headInfos = GenerateMetadataHeadInfos(table);

                    // Generate data list.
                    List<ConfigMetadata> dataList = GenerateDataList(table, fileName, headInfos);

                    // Save data list.
                    Type type = ReflectionUtility.GetType(GetMetadataFullName(fileName));

                    if (type != null)
                    {
                        ReflectionUtility.InvokeStaticGenericMethod(typeof(ConfigEditor),
                            "SaveDataList",
                            type,
                            new object[] { fileName, dataList, i });
                    }

                    // Show progress of generation.
                    UnityEditor.EditorUtility.DisplayProgressBar("Holding on", "The progress of configuration metadata generation.", (float)(i + 1) / length);
                }
            }

            // Save configuration parameters.
            SaveConfigParameters();

            // Destroy database.
            DestroyDatabase();

            // Show progress bar of complete action.
            Thread.Sleep(500);

            // Destroy progress bar.
            UnityEditor.EditorUtility.ClearProgressBar();

            // Show alert.
            UnityEditor.EditorUtility.DisplayDialog("Tip", "The metadata generated !", "OK");
        }

        /// <summary>
        /// Generates the data list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="headInfos">The MetadataHeadInfo list.</param>
        /// <returns></returns>
        private static List<ConfigMetadata> GenerateDataList(DataTable table, string className, List<MetadataHeadInfo> headInfos)
        {
            List<ConfigMetadata> dataList = new List<ConfigMetadata>();

            int rowCount = table.Rows.Count;
            for (int i = DefaultDataStartRowIndex; i < rowCount; ++i)
            {
                ConfigMetadata metadata = (ConfigMetadata)ReflectionUtility.CreateClassInstance(GetMetadataFullName(className));

                for (int j = 0, keysCount = headInfos.Count; j < keysCount; ++j)
                {
                    string cellValue = table.Rows[i][j].ToString().Trim();
                    MetadataHeadInfo headInfo = headInfos[j];

                    // The value of primary key can not be null or empty string.
                    if (headInfo.key == primaryKey && string.IsNullOrEmpty(cellValue))
                        continue;

                    ITypeParser parser = GetTypeParser(headInfo.type);

                    if (parser != null && !string.IsNullOrEmpty(cellValue))
                    {
                        object value = parser.ParseValue(cellValue);
                        ReflectionUtility.SetObjectFieldValue(metadata, headInfo.key, value);
                    }
                }

                dataList.Add(metadata);
            }

            return dataList;
        }

        /// <summary>
        /// Saves the data list.
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="dataList">The data list.</param>
        /// <param name="index">The index.</param>
        private static void SaveDataList<T>(string tableName, List<ConfigMetadata> dataList, int index) where T : class
        {
            // Create new database.
            Type type = ReflectionUtility.GetType(GetMetadataFullName(tableName));
            long localAddress = ConfigMetadata.IndexTableLocalAddress + index + 1;

            if (type != null)
            {
                // Insert table index.
                if (tableIndexDB != null)
                {
                    bool success = tableIndexDB.Insert(MetadataLocalAddress.TableName,
                        new MetadataLocalAddress() { typeName = type.Name, typeNamespace = type.Namespace, localAddress = localAddress });

                    if (success)
                    {
                        // Insert data list into new table by localAddress.
                        DB dataServer = new DB(localAddress);

                        if (metadataKeyFormatter != null)
                            primaryKey = metadataNameFormatter(primaryKey);

                        ReflectionUtility.InvokeGenericMethod(dataServer.GetConfig(),
                            "EnsureTable",
                            type,
                            new object[] { tableName, new string[] { primaryKey } });
                        dataServer.MinConfig();
                        DB.AutoBox dataDb = dataServer.Open();

                        foreach (object configMetadata in dataList)
                        {
                            success = dataDb.Insert(tableName, (T)configMetadata);

                            if (success)
                            {
                                if (debugMode)
                                    Debug.LogFormat("Insert Metadata object success: [tableName={0}, type={1}, object={2}]", tableName, type.FullName, configMetadata);
                            }
                            else
                            {
                                Debug.LogErrorFormat("Insert Metadata object failed: [tableName={0}, type={1}]", tableName, type.FullName);
                            }
                        }

                        dataServer.Dispose();
                        dataServer = null;
                    }
                    else
                    {
                        Debug.LogErrorFormat("Insert MetadataLocalAddress object into table [{0}] failed: [typeName={1}, localAddress={2}]",
                            tableName, type.FullName, localAddress);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the configuration parameters.
        /// </summary>
        private static void SaveConfigParameters()
        {
            if (tableIndexDB != null)
            {
                // Save parameter "PrimaryKey".
                string paramKey = Enum.GetName(typeof(ConfigParameterName), ConfigParameterName.PrimaryKey);
                bool success = tableIndexDB.Insert(ConfigParameter.TableName, new ConfigParameter() { key = paramKey, value = primaryKey });

                if (!success)
                    Debug.LogErrorFormat("Insert ConfigParameter object failed: [key={0}, value={1}]", paramKey, primaryKey);
            }
        }

        /// <summary>
        /// Destroys the database.
        /// </summary>
        private static void DestroyDatabase()
        {
            if (s_tableIndexDB != null)
                s_tableIndexDB = null;

            if (s_tableIndexServer != null)
            {
                s_tableIndexServer.Dispose();
                s_tableIndexServer = null;
            }
        }

        /// <summary>
        /// Determines whether [the specified data type] [is supported type].
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private static bool IsSupportedDataType(string type)
        {
            foreach (KeyValuePair<string, Type> kvp in s_supportedTypeParsers)
            {
                if (kvp.Key == type)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the type parser.
        /// </summary>
        /// <param name="typeString">The type string.</param>
        /// <returns></returns>
        private static ITypeParser GetTypeParser(string typeString)
        {
            ITypeParser parser = null;

            if (!s_typeParsers.ContainsKey(typeString))
            {
                parser = TypeParserFactory.CreateTypeParser(s_supportedTypeParsers[typeString]);
                s_typeParsers.Add(typeString, parser);
            }
            else
            {
                parser = s_typeParsers[typeString];
            }

            return parser;
        }

        /// <summary>
        /// Gets the full name of the metadata.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private static string GetMetadataFullName(string name)
        {
            return string.Format("{0}.{1}", metadataNamespace, name);
        }

        /// <summary>
        /// Format the comment string.
        /// </summary>
        /// <param name="comment">The comment string.</param>
        /// <returns>The formatted comment string.</returns>
        private static string CommentFormatter(string comment)
        {
            return comment.Replace(Environment.UnixNewline, Environment.newline + "\t\t/// ");
        }

        #endregion Private Static Functions
    }
}