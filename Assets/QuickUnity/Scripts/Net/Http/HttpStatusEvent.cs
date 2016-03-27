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
    /// The application dispatches HttpStatusEvent objects when a network request returns an HTTP status code.
    /// </summary>
    public class HttpStatusEvent : Event
    {
        /// <summary>
        /// The HttpStatusEvent.HTTP_STATUS constant defines the value of the type property of a httpStatus event object.
        /// </summary>
        public const string HTTP_STATUS = "httpStatus";

        /// <summary>
        /// Unlike the httpStatus event, the httpResponseStatus event is delivered before any response data.
        /// </summary>
        public const string HTTP_RESPONSE_STATUS = "httpResponseStatus";

        /// <summary>
        /// The HTTP status code returned by the server.
        /// </summary>
        private int mStatus = 0;

        /// <summary>
        /// Gets or sets the HTTP status code returned by the server.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public int status
        {
            get { return mStatus; }
            set { mStatus = value; }
        }

        /// <summary>
        /// The response headers that the response returned, as an dictionary of WWW objects.
        /// </summary>
        private Dictionary<string, string> mResponseHeaders;

        /// <summary>
        /// Gets or sets the response headers.
        /// </summary>
        /// <value>
        /// The response headers.
        /// </value>
        public Dictionary<string, string> responseHeaders
        {
            get { return mResponseHeaders; }
            set { mResponseHeaders = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpStatusEvent"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="target">The target of event.</param>
        /// <param name="status">The status.</param>
        public HttpStatusEvent(string type, object target = null, int status = 0)
            : base(type, target)
        {
            mStatus = status;
        }
    }
}