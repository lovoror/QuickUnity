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

using QuickUnity.Utilities;
using System.Collections.Generic;

namespace QuickUnity.Config
{
    /// <summary>
    /// The configuration metadata.
    /// </summary>
    public abstract class ConfigMetadata
    {
        /// <summary>
        /// The index table localAddress.
        /// </summary>
        public const long IndexTableLocalAddress = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigMetadata"/> class.
        /// </summary>
        public ConfigMetadata()
        {
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            string output = string.Empty;
            Dictionary<string, object> map = ReflectionUtility.GetObjectFieldsValues(this);

            foreach (KeyValuePair<string, object> kvp in map)
            {
                output += kvp.Key + ": " + kvp.Value + ", ";
            }

            return base.ToString() + " (" + output.Substring(0, output.Length - 2) + ")";
        }
    }

    /// <summary>
    /// The metadata local address value object.
    /// </summary>
    public class MetadataLocalAddress : ConfigMetadata
    {
        /// <summary>
        /// The name of table.
        /// </summary>
        public const string TableName = "MetadataTableIndexs";

        /// <summary>
        /// The primary key.
        /// </summary>
        public const string PrimaryKey = "typeName";

        /// <summary>
        /// The type name of object.
        /// </summary>
        public string typeName;

        /// <summary>
        /// The type namespace.
        /// </summary>
        public string typeNamespace;

        /// <summary>
        /// The local address.
        /// </summary>
        public long localAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataLocalAddress" /> class.
        /// </summary>
        public MetadataLocalAddress()
            : base()
        {
        }
    }

    /// <summary>
    /// The configuration parameter object.
    /// </summary>
    public class ConfigParameter : ConfigMetadata
    {
        /// <summary>
        /// The name of table.
        /// </summary>
        public const string TableName = "ConfigParameters";

        /// <summary>
        /// The primary key.
        /// </summary>
        public const string PrimaryKey = "key";

        /// <summary>
        /// The key.
        /// </summary>
        public string key;

        /// <summary>
        /// The value.
        /// </summary>
        public string value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigParameter"/> class.
        /// </summary>
        public ConfigParameter()
            : base()
        {
        }
    }
}