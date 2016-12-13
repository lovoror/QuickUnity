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

using QuickUnity.Patterns;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace QuickUnity.Data
{
    /// <summary>
    /// Class DataTableManager to manage all data tables.
    /// </summary>
    /// <seealso cref="System.IDisposable"/>
    /// <seealso cref="QuickUnity.Patterns.Singleton{QuickUnity.Data.DataTableManager}"/>
    public class DataTableManager : Singleton<DataTableManager>, IDisposable
    {
        /// <summary>
        /// The folder name of data tables storage.
        /// </summary>
        public const string DataTablesStorageFolderName = "DataTables";

        /// <summary>
        /// The scriptable object of preferences data.
        /// </summary>
        private DataTablePreferences m_preferencesData;

        /// <summary>
        /// The database path.
        /// </summary>
        private string m_databasePath;

        /// <summary>
        /// The database adapter of address map.
        /// </summary>
        private BoxDBAdapter m_addressMapDBAdapter;

        /// <summary>
        /// Prevents a default instance of the <see cref="DataTableManager"/> class from being created.
        /// </summary>
        private DataTableManager()
        {
            Initialize();
        }

        #region Public Functions

        /// <summary>
        /// Gets the object of data table row.
        /// </summary>
        /// <typeparam name="T">The type definition of data table row.</typeparam>
        /// <param name="primaryValue">The primary value.</param>
        /// <returns>T The object of type definition.</returns>
        public T GetDataTableRow<T>(object primaryValue) where T : DataTableRow, new()
        {
            DataTableAddressMap addressMap = GetDatabaseAddressMap<T>();
            BoxDBAdapter dbAdapter = GetDatabaseBoxAdapter(addressMap);

            if (dbAdapter != null)
            {
                string tableName = addressMap.type;
                dbAdapter.EnsureTable<T>(tableName, addressMap.primaryFieldName);
                dbAdapter.Open();
                T data = dbAdapter.Select<T>(tableName, primaryValue);
                dbAdapter.Dispose();
                return data;
            }

            return default(T);
        }

        /// <summary>
        /// Gets the data table rows.
        /// </summary>
        /// <typeparam name="T">The type definition of data table row.</typeparam>
        /// <param name="conditions">The conditions.</param>
        /// <param name="multiConditionOperators">The multi condition operators.</param>
        /// <returns>T[] The result list of data table rows.</returns>
        public T[] GetDataTableRows<T>(List<BoxDBQueryCondition> conditions,
            List<BoxDBMultiConditionOperator> multiConditionOperators = null) where T : DataTableRow, new()
        {
            DataTableAddressMap addressMap = GetDatabaseAddressMap<T>();
            BoxDBAdapter dbAdapter = GetDatabaseBoxAdapter(addressMap);

            if (dbAdapter != null)
            {
                string tableName = addressMap.type;
                dbAdapter.EnsureTable<T>(tableName, addressMap.primaryFieldName);
                dbAdapter.Open();
                List<T> results = dbAdapter.Select<T>(tableName, conditions, multiConditionOperators);
                dbAdapter.Dispose();

                if (results != null)
                {
                    return results.ToArray();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all data table rows.
        /// </summary>
        /// <typeparam name="T">The type definition of data table row.</typeparam>
        /// <returns>T[] The object array of type definition.</returns>
        public T[] GetAllDataTableRows<T>() where T : DataTableRow, new()
        {
            DataTableAddressMap addressMap = GetDatabaseAddressMap<T>();
            BoxDBAdapter dbAdapter = GetDatabaseBoxAdapter(addressMap);

            if (dbAdapter != null)
            {
                string tableName = addressMap.type;
                dbAdapter.EnsureTable<T>(tableName, addressMap.primaryFieldName);
                dbAdapter.Open();
                List<T> results = dbAdapter.SelectAll<T>(tableName);
                dbAdapter.Dispose();

                if (results != null)
                {
                    return results.ToArray();
                }
            }

            return null;
        }

        /// <summary>
        /// Gets all data table rows count.
        /// </summary>
        /// <typeparam name="T">The type definition of data table row.</typeparam>
        /// <returns>System.Int64 All data table row count.</returns>
        public long GetAllDataTableRowsCount<T>() where T : DataTableRow, new()
        {
            DataTableAddressMap addressMap = GetDatabaseAddressMap<T>();
            BoxDBAdapter dbAdapter = GetDatabaseBoxAdapter(addressMap);

            if (dbAdapter != null)
            {
                string tableName = addressMap.type;
                dbAdapter.EnsureTable<T>(tableName, addressMap.primaryFieldName);
                dbAdapter.Open();
                long count = dbAdapter.SelectCount(tableName);
                dbAdapter.Dispose();
                return count;
            }

            return 0;
        }

        /// <summary>
        /// Gets the data table row count.
        /// </summary>
        /// <typeparam name="T">The type definition of data table row.</typeparam>
        /// <param name="conditions">The conditions.</param>
        /// <param name="multiConditionOperators">The multi condition operators.</param>
        /// <returns>System.Int64 The data table row count.</returns>
        public long GetDataTableRowsCount<T>(List<BoxDBQueryCondition> conditions,
            List<BoxDBMultiConditionOperator> multiConditionOperators = null) where T : DataTableRow, new()
        {
            DataTableAddressMap addressMap = GetDatabaseAddressMap<T>();
            BoxDBAdapter dbAdapter = GetDatabaseBoxAdapter(addressMap);

            if (dbAdapter != null)
            {
                string tableName = addressMap.type;
                dbAdapter.EnsureTable<T>(tableName, addressMap.primaryFieldName);
                dbAdapter.Open();
                long count = dbAdapter.SelectCount(tableName, conditions, multiConditionOperators);
                dbAdapter.Dispose();
                return count;
            }

            return 0;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            if (m_addressMapDBAdapter != null)
            {
                m_addressMapDBAdapter.Dispose();
                m_addressMapDBAdapter = null;
            }
        }

        #endregion Public Functions

        /// <summary>
        /// Initializes.
        /// </summary>
        private void Initialize()
        {
#if UNITY_EDITOR
            m_preferencesData = UnityEditor.AssetDatabase.LoadAssetAtPath<DataTablePreferences>("Assets/Resources/DataTablePreferences.asset");
#else
            DataTablePreferences[] objects = Resources.FindObjectsOfTypeAll<DataTablePreferences>();

            if (objects != null && objects.Length > 0)
            {
                m_preferencesData = objects[0];
            }
#endif

            m_databasePath = Path.Combine(Application.persistentDataPath, DataTablesStorageFolderName);

            if (m_preferencesData && m_preferencesData.dataTablesStorageLocation == DataTableStorageLocation.StreamingAssetsPath)
            {
                m_databasePath = Path.Combine(Application.streamingAssetsPath, DataTablesStorageFolderName);
            }

            if (m_preferencesData && m_preferencesData.dataTablesStorageLocation == DataTableStorageLocation.ResourcesPath)
            {
                TextAsset binAsset = Resources.Load<TextAsset>(DataTablesStorageFolderName + "/db1");

                if (binAsset)
                {
                    m_addressMapDBAdapter = new BoxDBAdapter(m_databasePath, binAsset.bytes);
                }
            }
            else
            {
                m_addressMapDBAdapter = new BoxDBAdapter(m_databasePath);
            }

            m_addressMapDBAdapter.EnsureTable<DataTableAddressMap>(typeof(DataTableAddressMap).Name, DataTableAddressMap.PrimaryKey);
            m_addressMapDBAdapter.Open();
        }

        /// <summary>
        /// Gets the database address.
        /// </summary>
        /// <typeparam name="T">The type definition of data.</typeparam>
        /// <returns>System.Int64 The database address.</returns>
        private DataTableAddressMap GetDatabaseAddressMap<T>()
        {
            string name = typeof(T).Name;

            if (m_addressMapDBAdapter == null)
            {
                Initialize();
            }

            DataTableAddressMap addressMap = m_addressMapDBAdapter.Select<DataTableAddressMap>(typeof(DataTableAddressMap).Name, name);
            return addressMap;
        }

        /// <summary>
        /// Gets the adapter of database.
        /// </summary>
        /// <param name="addressMap">The object of DataTableAddressMap.</param>
        /// <returns>BoxDBAdapter The adapter of database.</returns>
        private BoxDBAdapter GetDatabaseBoxAdapter(DataTableAddressMap addressMap)
        {
            BoxDBAdapter dbAdapter = null;

            if (addressMap != null && addressMap.localAddress > 1)
            {
                if (m_preferencesData.dataTablesStorageLocation == DataTableStorageLocation.ResourcesPath)
                {
                    TextAsset binAsset = Resources.Load<TextAsset>(DataTablesStorageFolderName + "/db" + addressMap.localAddress);

                    if (binAsset)
                    {
                        dbAdapter = new BoxDBAdapter(m_databasePath, binAsset.bytes);
                    }
                }
                else
                {
                    dbAdapter = new BoxDBAdapter(m_databasePath, addressMap.localAddress);
                }
            }

            return dbAdapter;
        }
    }
}