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
using System;
using System.Collections.Generic;

namespace QuickUnity.LocalServer
{
    /// <summary>
    /// Wrapper class for invoking iBoxDB functions.
    /// </summary>
    public class iBoxDBWrapper : IDisposable
    {
        /// <summary>
        /// The server object.
        /// </summary>
        private DB m_server;

        /// <summary>
        /// The database object.
        /// </summary>
        private DB.AutoBox m_db;

        /// <summary>
        /// Initializes a new instance of the <see cref="IBoxDBWrapper" /> class.
        /// </summary>
        /// <param name="dbPath">The database path.</param>
        /// <param name="dbDestAddr">The database destnation address.</param>
        /// <param name="autoOpen">if set to <c>true</c> [automatic open].</param>
        public iBoxDBWrapper(string dbPath, long dbDestAddr = 0, bool autoOpen = true)
        {
            DB.Root(dbPath);
            m_server = new DB(dbDestAddr, dbPath);
            m_server.MinConfig();

            if (autoOpen)
                Open();
        }

        #region Public Functions

        /// <summary>
        /// Opens this database.
        /// </summary>
        public void Open()
        {
            if (m_server != null)
                m_db = m_server.Open();
        }

        /// <summary>
        /// Creates the table.
        /// </summary>
        /// <typeparam name="T">The type that be mapped to table.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="key">Name of the primary key.</param>
        public void CreateTable<T>(string tableName, string key) where T : class
        {
            try
            {
                m_server.GetConfig().EnsureTable<T>(tableName, key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Selects the data list by some conditions.
        /// </summary>
        /// <typeparam name="T">The type of data.</typeparam>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="conditions">The conditions.</param>
        /// <returns></returns>
        public List<T> Select<T>(string tableName, Dictionary<string, object> conditions) where T : class, new()
        {
            try
            {
                using (m_db.Cube())
                {
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
                    IBEnumerable<T> items = m_db.Select<T>(sql, values.ToArray());
                    return new List<T>(items);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Closes this database.
        /// </summary>
        public void Close()
        {
            if (m_server != null)
                m_server.Close();
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            Close();

            if (m_server != null)
                m_server.Dispose();

            m_server = null;
            m_db = null;
        }

        #endregion Public Functions
    }
}