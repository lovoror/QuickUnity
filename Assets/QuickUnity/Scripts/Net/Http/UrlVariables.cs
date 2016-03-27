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

using System.Collections.Generic;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// The UrlVariables class allows you to transfer variables between an application and a server.
    /// </summary>
    public class UrlVariables
    {
        /// <summary>
        /// The key/value pair variables.
        /// </summary>
        private Dictionary<string, string> mVariables;

        /// <summary>
        /// Gets the key/value pair variables.
        /// </summary>
        /// <value>The variables.</value>
        public Dictionary<string, string> variables
        {
            get { return mVariables; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlVariables"/> class.
        /// </summary>
        /// <param name="source">The source.</param>
        public UrlVariables(string source = null)
        {
            mVariables = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(source))
                Decode(source);
        }

        /// <summary>
        /// Converts the variable string to properties of the specified Dictionary object.
        /// </summary>
        /// <param name="source">A URL-encoded query string containing name/value pairs.</param>

        public void Decode(string source)
        {
            string[] pairStrArr = source.Split('&');

            if (pairStrArr.Length > 0)
            {
                foreach (string pairStr in pairStrArr)
                {
                    string[] kvPair = pairStr.Split('=');

                    if (kvPair.Length == 2)
                        mVariables.Add(kvPair[0], kvPair[1]);
                }
            }
        }

        /// <summary>
        /// Returns a string containing all enumerable variables, in the MIME content encoding application/x-www-form-urlencoded.
        /// </summary>
        /// <returns>A <see cref="System.String" /> A URL-encoded string containing name/value pairs.</returns>

        public override string ToString()
        {
            List<string> kvPairs = new List<string>();

            foreach (KeyValuePair<string, string> kvp in mVariables)
            {
                string kvPair = kvp.Key + "=" + kvp.Value;
                kvPairs.Add(kvPair);
            }

            return string.Join("&", kvPairs.ToArray());
        }
    }
}