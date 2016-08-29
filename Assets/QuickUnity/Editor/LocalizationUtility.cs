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
using ICSharpCode.SharpZipLib.Zip;
using Pathfinding.Serialization.JsonFx;
using QuickUnity.Editor.Localization;
using QuickUnity.Localization;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using UnityEditor;

namespace QuickUnity.Editor
{
    /// <summary>
    /// This script adds the QuinUnity/Localization menu items to the Unity Editor.
    /// </summary>
    public static class LocalizationUtility
    {
        /// <summary>
        /// The ini configuration file section name.
        /// </summary>
        public const string INIConfigFileSectionName = "Localization";

        /// <summary>
        /// The localization files path configuration key.
        /// </summary>
        public const string LocalizationFilesPathConfigKey = "localizationFilesPath";

        /// <summary>
        /// The modules configuration key name.
        /// </summary>
        public const string ModulesConfigKey = "modules";

        /// <summary>
        /// The language presets configuration key name.
        /// </summary>
        public const string LanguagePresetsConfigKey = "languagePresets";

        /// <summary>
        /// The localization archive file extension.
        /// </summary>
        public const string LocalizationArchiveFileExtension = ".archive";

        /// <summary>
        /// The localization resource file extension.
        /// </summary>
        public const string LocalizationResFileExtension = ".locres";

        /// <summary>
        /// The menu item priority.
        /// </summary>
        public const int MenuItemPriority = HelpUtility.MenuItemPriority - QuickUnityEditor.MenuItemSeparatedNumber;

        /// <summary>
        /// The LanguagePresets window
        /// </summary>
        private static LanguagePresetsWindow s_languagePresetsWindow;

        /// <summary>
        /// The LocalizationDashboard window
        /// </summary>
        private static LocalizationDashboardWindow s_localizationDashboardWindow;

        #region Menu Functions

        /// <summary>
        /// Shows the LanguagePresets window.
        /// </summary>
        [MenuItem("QuickUnity/Localization/Languages", false, MenuItemPriority)]
        public static void ShowLanguagePresetsWindow()
        {
            if (s_languagePresetsWindow == null)
            {
                s_languagePresetsWindow = EditorWindow.GetWindow<LanguagePresetsWindow>(false, "Languages");
                EditorWindow.FocusWindowIfItsOpen<LanguagePresetsWindow>();
            }

            if (s_localizationDashboardWindow != null)
            {
                s_localizationDashboardWindow.Close();
                s_localizationDashboardWindow = null;
            }

            s_languagePresetsWindow.Show();
        }

        /// <summary>
        /// Shows the LocalizationDashboard window.
        /// </summary>
        [MenuItem("QuickUnity/Localization/Localization Dashboard", false, MenuItemPriority)]
        public static void ShowLocalizationDashboardWindow()
        {
            if (s_localizationDashboardWindow == null)
            {
                s_localizationDashboardWindow = EditorWindow.GetWindow<LocalizationDashboardWindow>(false, "Localization");
                EditorWindow.FocusWindowIfItsOpen<LocalizationDashboardWindow>();
            }

            if (s_languagePresetsWindow != null)
            {
                s_languagePresetsWindow.Close();
                s_languagePresetsWindow = null;
            }

            s_localizationDashboardWindow.Show();
        }

        #endregion Menu Functions

        /// <summary>
        /// Shows the localization archive window.
        /// </summary>
        /// <param name="moduleLang">The module language.</param>
        /// <param name="localiztionFilePath">The localiztion file path.</param>
        /// <param name="language">The language.</param>
        /// <param name="moduleName">Name of the module.</param>
        public static void ShowLocalizationArchiveWindow(ModuleLanguage moduleLang, string localiztionFilePath, string language, string moduleName)
        {
            if (!string.IsNullOrEmpty(localiztionFilePath))
            {
                List<LocalizationArchive> list = null;
                string targetPath = Path.Combine(localiztionFilePath, language);

                // If target path
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                string filePath = Path.Combine(targetPath, moduleName + LocalizationArchiveFileExtension);

                if (File.Exists(filePath))
                {
                    // Load data.
                    string text = File.ReadAllText(filePath);
                    list = JsonReader.Deserialize<List<LocalizationArchive>>(text);
                }

                if (LocalizationArchiveWindow.currentWindow != null)
                {
                    LocalizationArchiveWindow.currentWindow.Close();
                    LocalizationArchiveWindow.currentWindow = null;
                }

                string windowTitle = moduleName + " - " + language;
                LocalizationArchiveWindow window = EditorWindow.GetWindow<LocalizationArchiveWindow>(false, windowTitle);
                LocalizationArchiveWindow.currentWindow = window;
                EditorWindow.FocusWindowIfItsOpen<LocalizationArchiveWindow>();
                window.archiveFilePath = filePath;
                window.localizationArchives = list;
                window.moduleLanguage = moduleLang;
                window.Show(false);
            }
            else
            {
                DisplayInvalidLocalizationFilesPathDialog();
            }
        }

        /// <summary>
        /// Imports the translation text file.
        /// </summary>
        /// <param name="translationTextFilePath">The translation text file path.</param>
        /// <returns>The translation JSON string.</returns>
        public static string ImportTranslationTextFile(string translationTextFilePath)
        {
            if (string.IsNullOrEmpty(translationTextFilePath))
            {
                return null;
            }

            if (File.Exists(translationTextFilePath))
            {
                string json = null;
                string fileExtension = Path.GetExtension(translationTextFilePath).ToLower();

                if (fileExtension == ".txt" || fileExtension == ".json")
                {
                    json = File.ReadAllText(translationTextFilePath, Encoding.UTF8);
                }
                else if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    List<LocalizationArchive> archives = new List<LocalizationArchive>();
                    FileStream fileStream = File.Open(translationTextFilePath, FileMode.Open, FileAccess.Read);
                    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fileStream);
                    DataSet result = excelReader.AsDataSet();
                    DataTable table = result.Tables[0];
                    DataRowCollection rows = table.Rows;

                    for (int i = 0, length = rows.Count; i < length; ++i)
                    {
                        string key = rows[i][0].ToString().Trim();
                        string translation = rows[i][1].ToString();

                        archives.Add(new LocalizationArchive(key, translation));
                    }

                    json = JsonWriter.Serialize(archives);
                }

                return json;
            }
            else
            {
                EditorUtility.DisplayDialog("Error!", "Translation text file is not valid!", "Ok");
                return null;
            }
        }

        /// <summary>
        /// Exports the translation archive file.
        /// </summary>
        /// <param name="targertPath">The targert path.</param>
        /// <param name="archiveFilePath">The archive file path.</param>
        public static void ExportTranslationArchiveFile(string targertPath, string archiveFilePath)
        {
            if (File.Exists(archiveFilePath))
            {
                string text = File.ReadAllText(archiveFilePath);

                if (!string.IsNullOrEmpty(text))
                {
                    List<LocalizationArchive> list = JsonReader.Deserialize<List<LocalizationArchive>>(text);

                    if (list != null && list.Count > 0)
                    {
                        File.WriteAllText(targertPath, text, Encoding.UTF8);
                    }
                }
            }
        }

        /// <summary>
        /// Compiles the translation file.
        /// </summary>
        /// <param name="localiztionFilePath">The localiztion file path.</param>
        /// <param name="language">The language.</param>
        /// <param name="moduleName">Name of the module.</param>
        public static void CompileTranslationFile(string localiztionFilePath, string language, string moduleName)
        {
            if (!string.IsNullOrEmpty(localiztionFilePath))
            {
                string targetPath = Path.Combine(localiztionFilePath, language);

                // If target path
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }

                string filePath = Path.Combine(targetPath, moduleName + LocalizationArchiveFileExtension);

                if (File.Exists(filePath))
                {
                    using (FileStream fs = File.OpenRead(filePath))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        fs.Close();

                        string resFilePath = Path.Combine(targetPath, moduleName + LocalizationResFileExtension);

                        using (FileStream zipFile = File.Create(resFilePath))
                        {
                            using (ZipOutputStream ZipStream = new ZipOutputStream(zipFile))
                            {
                                ZipEntry ZipEntry = new ZipEntry(moduleName);
                                ZipStream.PutNextEntry(ZipEntry);
                                ZipStream.SetLevel(5);
                                ZipStream.Write(buffer, 0, buffer.Length);
                                ZipStream.Finish();
                                ZipStream.Close();
                            }
                        }
                    }

                    EditorUtility.DisplayDialog("Done", "Compile Translation is completed!", "Ok");
                }
            }
            else
            {
                DisplayInvalidLocalizationFilesPathDialog();
            }
        }

        /// <summary>
        /// Displays the invalid localization files path dialog.
        /// </summary>
        public static void DisplayInvalidLocalizationFilesPathDialog()
        {
            EditorUtility.DisplayDialog("Warning!", "Localization files path is not valid!", "Ok");
        }
    }
}