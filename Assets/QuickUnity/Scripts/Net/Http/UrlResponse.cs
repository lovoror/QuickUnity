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
using UnityEngine;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// The UrlRequest class captures all of the information in a single HTTP response.
    /// </summary>
    public class UrlResponse
    {
        /// <summary>
        /// The status code.
        /// </summary>
        private int mStatusCode;

        /// <summary>
        /// Gets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int statusCode
        {
            get { return mStatusCode; }
        }

        /// <summary>
        /// The URL to be requested.
        /// </summary>
        private string mUrl;

        /// <summary>
        /// Gets or sets the URL to be requested.
        /// </summary>
        /// <value>The URL.</value>
        public string url
        {
            get { return mUrl; }
        }

        /// <summary>
        /// The response data.
        /// </summary>
        public object data;

        /// <summary>
        /// Returns an error message if there was an error during the download.
        /// </summary>
        private string mError;

        /// <summary>
        /// Returns an error message if there was an error during the download (Read Only).
        /// </summary>
        /// <value>
        /// An error message if there was an error during the download.
        /// </value>
        public string error
        {
            get { return mError; }
        }

        /// <summary>
        /// The dictionary of headers returned by the request.
        /// </summary>
        private Dictionary<string, string> mResponseHeaders;

        /// <summary>
        /// Gets or sets the dictionary of headers returned by the request.
        /// </summary>
        /// <value>
        /// The dictionary of headers returned by the request.
        /// </value>
        public Dictionary<string, string> responseHeaders
        {
            get { return mResponseHeaders; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlResponse" /> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="www">The WWW.</param>
        public UrlResponse(int statusCode, WWW www)
        {
            mStatusCode = statusCode;

            if (www != null)
            {
                mUrl = www.url;
                mError = www.error;
                mResponseHeaders = www.responseHeaders;
            }
        }
    }
}