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

using QuickUnity.Events;
using System.Collections.Generic;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// When you use UrlLoader, it will dispatch HTTPEvent.
    /// </summary>
    public class HttpEvent : Event
    {
        /// <summary>
        /// Dispatched when a load operation start.
        /// </summary>
        public const string OPEN = "open";

        /// <summary>
        /// Dispatched when a load operation in progress.
        /// </summary>
        public const string PROGRESS = "progress";

        /// <summary>
        /// Dispatched when a load operation is complete.
        /// </summary>
        public const string COMPLETE = "complete";

        /// <summary>
        /// Dispatched when a load operation get error message.
        /// </summary>
        public const string ERROR = "error";

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
        /// The progress of downloading.
        /// </summary>
        private float mProgress = 0.0f;

        /// <summary>
        /// Gets or sets the progress of downloading.
        /// </summary>
        /// <value>
        /// The progress.
        /// </value>
        public float progress
        {
            get { return mProgress; }
            set { mProgress = value; }
        }

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
        /// An object containing data to be transmitted with the URL request or the data received from the load operation.
        /// </summary>
        private object mData;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object data
        {
            get { return mData; }
        }

        /// <summary>
        /// Returns an error message if there was an error during the download.
        /// </summary>
        private string mError;

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error.</value>
        public string error
        {
            get { return mError; }
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
        /// Initializes a new instance of the <see cref="HttpEvent" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="target">The target.</param>
        /// <param name="request">The request.</param>
        public HttpEvent(string type, object target = null, UrlRequest request = null)
            : base(type, target)
        {
            mUrl = request.url;
            mData = request.data;
            mRequestHeaders = request.requestHeaders;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpEvent" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="target">The target of event.</param>
        /// <param name="response">The response object.</param>
        public HttpEvent(string type, object target = null, UrlResponse response = null)
            : base(type, target)
        {
            if (response != null)
            {
                mUrl = response.url;
                mStatusCode = response.statusCode;
                mError = response.error;
                mData = response.data;
                mResponseHeaders = response.responseHeaders;
            }
        }
    }
}