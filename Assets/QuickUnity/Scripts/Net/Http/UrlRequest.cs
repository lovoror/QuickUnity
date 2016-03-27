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

using System;
using System.Collections.Generic;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// Request method enum of url request.
    /// </summary>
    public enum UrlRequestMethod
    {
        Get,
        Post
    }

    /// <summary>
    /// The UrlRequest class captures all of the information in a single HTTP request.
    /// </summary>
    public class UrlRequest
    {
        /// <summary>
        /// Controls the HTTP form submission method.
        /// </summary>
        private UrlRequestMethod mMethod = UrlRequestMethod.Get;

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        /// <value>The method.</value>
        public UrlRequestMethod method
        {
            get { return mMethod; }
            set { mMethod = value; }
        }

        /// <summary>
        /// An object containing data to be transmitted with the URL request.
        /// </summary>
        private object mData;

        /// <summary>
        /// Gets or sets an object containing data to be transmitted with the URL request.
        /// </summary>
        /// <value>The data.</value>
        public object data
        {
            get { return mData; }
            set { mData = value; }
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
            set { mUrl = value; }
        }

        /// <summary>
        /// The complete callback response function.
        /// </summary>
        private Action<UrlResponse> mCompleteCallback;

        /// <summary>
        /// Gets or sets the complete response callback.
        /// </summary>
        /// <value>The complete callback.</value>
        public Action<UrlResponse> completeCallback
        {
            get { return mCompleteCallback; }
            set { mCompleteCallback = value; }
        }

        /// <summary>
        /// The error callback function.
        /// </summary>
        private Action<string> mErrorCallback;

        /// <summary>
        /// Gets or sets the error callback function.
        /// </summary>
        /// <value>
        /// The error callback function.
        /// </value>
        public Action<string> errorCallback
        {
            get { return mErrorCallback; }
            set { mErrorCallback = value; }
        }

        /// <summary>
        /// The dictionary of HTTP request headers to be appended to the HTTP request.
        /// </summary>
        private Dictionary<string, string> mRequestHeaders;

        /// <summary>
        /// Gets or sets the request headers.
        /// </summary>
        /// <value>The request headers.</value>
        public Dictionary<string, string> requestHeaders
        {
            get { return mRequestHeaders; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlRequest" /> class.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="completeCallback">The complete callback.</param>
        /// <param name="errorCallback">The error callback.</param>
        public UrlRequest(string url, Action<UrlResponse> completeCallback = null, Action<string> errorCallback = null)
        {
            mUrl = url;
            mCompleteCallback = completeCallback;
            mErrorCallback = errorCallback;
        }

        /// <summary>
        /// Adds the header to be appended to the HTTP request.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddHeader(string key, string value)
        {
            if (mRequestHeaders == null)
                mRequestHeaders = new Dictionary<string, string>();

            mRequestHeaders.Add(key, value);
        }
    }
}