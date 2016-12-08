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
            string path = Path.Combine(QuickUnityApplication.ResourcesFolderName, typeof(DataTablePreferences).Name);
            DataTablePreferences[] objects = Resources.FindObjectsOfTypeAll<DataTablePreferences>();

            if (objects != null && objects.Length > 0)
            {
                m_preferencesData = objects[0];
            }

            m_databasePath = Path.Combine(Application.persistentDataPath, DataTablesStorageFolderName);

            if (m_preferencesData && m_preferencesData.dataTablesStorageLocation == DataTableStorageLocation.StreamingAssetsPath)
            {
                m_databasePath = Path.Combine(Application.streamingAssetsPath, DataTablesStorageFolderName);
            }

            m_addressMapDBAdapter = new BoxDBAdapter(m_databasePath);
            m_addressMapDBAdapter.EnsureTable<DataTableAddressMap>(typeof(DataTableAddressMap).Name, DataTableAddressMap.PrimaryKey);
            m_addressMapDBAdapter.Open();
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

            if (addressMap.localAddress > 1)
            {
                BoxDBAdapter dbAdapter = new BoxDBAdapter(m_databasePath, addressMap.localAddress);
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
        /// Gets the database address.
        /// </summary>
        /// <typeparam name="T">The type definition of data.</typeparam>
        /// <returns>System.Int64 The database address.</returns>
        private DataTableAddressMap GetDatabaseAddressMap<T>()
        {
            string name = typeof(T).Name;

            if (m_addressMapDBAdapter != null)
            {
                DataTableAddressMap addressMap = m_addressMapDBAdapter.Select<DataTableAddressMap>(typeof(DataTableAddressMap).Name, name);
                return addressMap;
            }

            return null;
        }
    }
}