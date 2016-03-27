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

using iBoxDB.LocalServer;
using QuickUnity.Patterns;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Config
{
    /// <summary>
    /// The name of configuration parameter.
    /// </summary>
    public enum ConfigParameterName
    {
        PrimaryKey
    }

    /// <summary>
    /// Manage all configuration metadata.
    /// </summary>
    public sealed class ConfigManager : Singleton<ConfigManager>
    {
        /// <summary>
        /// The primary key.
        /// </summary>
        private string m_primaryKey = string.Empty;

        /// <summary>
        /// The table index database.
        /// </summary>
        private DB.AutoBox m_tableIndexDB;

        /// <summary>
        /// The metadata database map.
        /// </summary>
        private Dictionary<Type, DB.AutoBox> m_metadataDBMap;

        /// <summary>
        /// Prevents a default instance of the <see cref="ConfigManager"/> class from being created.
        /// </summary>
        private ConfigManager()
        {
            m_metadataDBMap = new Dictionary<Type, DB.AutoBox>();
        }

        #region API

        /// <summary>
        /// Sets the database root path.
        /// </summary>
        /// <param name="rootPath">The root path.</param>
        public void SetDatabaseRootPath(string rootPath)
        {
            DB.Root(rootPath);
            CreateTableIndexDB();
        }

        /// <summary>
        /// Gets the configuration metadata.
        /// </summary>
        /// <typeparam name="T">The type of metadata. </typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T GetConfigMetadata<T>(long id) where T : ConfigMetadata, new()
        {
            DB.AutoBox db = GetDB<T>();

            if (db != null)
            {
                Type type = typeof(T);
                string tableName = type.Name;
                T item = db.SelectKey<T>(tableName, id);
                return item;
            }

            return null;
        }

        /// <summary>
        /// Gets the configuration metadata list.
        /// </summary>
        /// <typeparam name="T">The type of metadata.</typeparam>
        /// <param name="conditions">The conditions dictionary.</param>
        /// <returns></returns>
        public List<T> GetConfigMetadataList<T>(Dictionary<string, object> conditions) where T : ConfigMetadata, new()
        {
            DB.AutoBox db = GetDB<T>();

            if (db != null)
            {
                Type type = typeof(T);
                string tableName = type.Name;
                string sql = "from " + tableName + " where";

                int i = 0;
                int length = conditions.Keys.Count;

                foreach (string key in conditions.Keys)
                {
                    sql += " " + key + "==?";

                    if (i < length - 1)
                        sql += " &";

                    i++;
                }

                List<object> values = new List<object>(conditions.Values);
                IBEnumerable<T> items = db.Select<T>(sql, values.ToArray());
                return new List<T>(items);
            }

            return null;
        }

        /// <summary>
        /// Disposes the metadata database.
        /// </summary>
        /// <typeparam name="T">The type of metadata database.</typeparam>
        public void DisposeMetadataDB<T>()
        {
            Type type = typeof(T);
            DB.AutoBox db = m_metadataDBMap[type];

            if (db != null)
            {
                db.GetDatabase().Dispose();
                m_metadataDBMap.Remove(type);
                db = null;
            }
        }

        /// <summary>
        /// Disposes all metadata databases.
        /// </summary>
        public void DisposeAllMetadataDB()
        {
            foreach (KeyValuePair<Type, DB.AutoBox> kvp in m_metadataDBMap)
            {
                DB.AutoBox db = kvp.Value;

                if (db != null)
                    db.GetDatabase().Dispose();
            }

            m_metadataDBMap.Clear();
        }

        /// <summary>
        /// Disposes all databases.
        /// </summary>
        public void DisposeAllDB()
        {
            if (m_tableIndexDB != null)
            {
                m_tableIndexDB.GetDatabase().Dispose();
                m_tableIndexDB = null;
            }

            DisposeAllMetadataDB();
        }

        #endregion API

        #region Private Functions

        /// <summary>
        /// Creates the table index database.
        /// </summary>
        private void CreateTableIndexDB()
        {
            if (m_tableIndexDB == null)
            {
                DB server = new DB(ConfigMetadata.IndexTableLocalAddress);
                DatabaseConfig.Config serverConfig = server.GetConfig();

                if (serverConfig != null)
                {
                    serverConfig.EnsureTable<MetadataLocalAddress>(MetadataLocalAddress.TableName, MetadataLocalAddress.PrimaryKey);
                    serverConfig.EnsureTable<ConfigParameter>(ConfigParameter.TableName, ConfigParameter.PrimaryKey);
                }

                m_tableIndexDB = server.Open();
            }
        }

        /// <summary>
        /// Gets the primary key.
        /// </summary>
        /// <returns></returns>
        private string GetPrimaryKey()
        {
            if (string.IsNullOrEmpty(m_primaryKey))
            {
                string paramKey = Enum.GetName(typeof(ConfigParameterName), ConfigParameterName.PrimaryKey);
                object param = GetConfigParameter(paramKey);

                if (param != null)
                    m_primaryKey = param.ToString();
            }

            return m_primaryKey;
        }

        /// <summary>
        /// Gets the configuration parameter.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The parameter value.</returns>
        private string GetConfigParameter(string key)
        {
            ConfigParameter configParam = m_tableIndexDB.SelectKey<ConfigParameter>(ConfigParameter.TableName, key);

            if (configParam != null)
                return configParam.value;

            return null;
        }

        /// <summary>
        /// Gets the table local address.
        /// </summary>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <returns>The local address of table.</returns>
        private long GetTableLocalAddress<T>() where T : ConfigMetadata
        {
            if (m_tableIndexDB == null)
                CreateTableIndexDB();

            Type type = typeof(T);
            string sql = string.Format("from {0} where typeNamespace==? & typeName==?", MetadataLocalAddress.TableName);
            IBEnumerable<MetadataLocalAddress> items = m_tableIndexDB.Select<MetadataLocalAddress>(sql, type.Namespace, type.Name);

            if (items != null)
            {
                List<MetadataLocalAddress> list = new List<MetadataLocalAddress>(items);

                if (list.Count > 0)
                    return list[0].localAddress;
            }

            return -1;
        }

        /// <summary>
        /// Gets the database.
        /// </summary>
        /// <typeparam name="T">The type of metadata. </typeparam>
        /// <returns>The database object. </returns>
        private DB.AutoBox GetDB<T>() where T : ConfigMetadata, new()
        {
            Type type = typeof(T);
            string tableName = type.Name;
            DB.AutoBox db = null;

            if (m_metadataDBMap.ContainsKey(type))
            {
                db = m_metadataDBMap[type];
            }
            else
            {
                long localAddress = GetTableLocalAddress<T>();

                if (localAddress != -1)
                {
                    DB server = new DB(localAddress);
                    string primaryKey = GetPrimaryKey();

                    if (!string.IsNullOrEmpty(primaryKey))
                    {
                        server.GetConfig().EnsureTable<T>(tableName, primaryKey);
                        db = server.Open();
                        m_metadataDBMap.Add(type, db);
                    }
                    else
                    {
                        Debug.LogError("Table primary key can not be null or empty !");
                    }
                }
            }

            return db;
        }

        #endregion Private Functions
    }
}