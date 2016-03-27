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
using QuickUnity.Tasks;
using QuickUnity.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuickUnity.Net.Http
{
    /// <summary>
    /// Data format enum of url loader.
    /// </summary>
    public enum UrlLoaderDataFormat
    {
        Text,
        Texture,
        TextureNonreadable,
        Movie,
        Binary,
        AssetBundle,
        AudioClip
    }

    /// <summary>
    /// The UrlLoader class downloads data from a url as text, binary data, or URL-encoded variables.
    /// </summary>
    public class UrlLoader : EventDispatcher
    {
        /// <summary>
        /// The data format binding properties.
        /// </summary>
        private static Dictionary<UrlLoaderDataFormat, string> mDataFormatBindingProperties = new Dictionary<UrlLoaderDataFormat, string>()
        {
            { UrlLoaderDataFormat.Text, "text" },
            { UrlLoaderDataFormat.Texture, "texture" },
            { UrlLoaderDataFormat.TextureNonreadable, "textureNonReadable" },
            { UrlLoaderDataFormat.Movie, "movie" },
            { UrlLoaderDataFormat.Binary, "bytes" },
            { UrlLoaderDataFormat.AssetBundle, "assetBundle" },
            { UrlLoaderDataFormat.AudioClip, "audioClip" }
        };

        /// <summary>
        /// A UrlRequest object specifying the URL to download.
        /// </summary>
        private UrlRequest mRequest;

        /// <summary>
        /// The request task.
        /// </summary>
        private ITask mRequestTask;

        /// <summary>
        /// Indicates the number of bytes that have been loaded thus far during the load operation.
        /// </summary>
        private int mBytesLoaded = 0;

        /// <summary>
        /// Gets or sets the bytes loaded.
        /// </summary>
        /// <value>The bytes loaded.</value>
        public int bytesLoaded
        {
            get { return mBytesLoaded; }
        }

        /// <summary>
        /// Indicates the total number of bytes in the downloaded data.
        /// </summary>
        private int mBytesTotal = 0;

        /// <summary>
        /// Gets the total number of bytes in the downloaded data.
        /// </summary>
        /// <value>
        /// The bytes total.
        /// </value>
        public int bytesTotal
        {
            get { return mBytesTotal; }
        }

        /// <summary>
        /// The data received from the load operation.
        /// </summary>
        private object mData;

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public object data
        {
            get { return mData; }
            set { mData = value; }
        }

        /// <summary>
        /// Controls whether the downloaded data is received as text or other type.
        /// </summary>
        private UrlLoaderDataFormat mDataFormat = UrlLoaderDataFormat.Text;

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        /// <value>The data format.</value>
        public UrlLoaderDataFormat dataFormat
        {
            get { return mDataFormat; }
            set { mDataFormat = value; }
        }

        /// <summary>
        /// Parses the HTTP status code.
        /// </summary>
        /// <param name="responseHeaders">The response headers.</param>
        /// <returns></returns>
        public static int ParseHttpStatusCode(Dictionary<string, string> responseHeaders)
        {
            string statusStr = responseHeaders["STATUS"];
            string[] statusStrArr = statusStr.Split(' ');

            if (statusStrArr.Length > 1)
                return int.Parse(statusStrArr[1]);

            return 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlLoader"/> class.
        /// </summary>
        /// <param name="request">The request.</param>
        public UrlLoader(UrlRequest request = null)
        {
            if (request != null)
                Load(request);
        }

        /// <summary>
        /// Closes the load operation in progress.
        /// </summary>
        public void Close()
        {
            if (mRequestTask != null)
            {
                mRequestTask.Stop();
                TaskManager.instance.RemoveTask(mRequestTask);
                mRequestTask = null;
            }
        }

        /// <summary>
        /// Loads the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public void Load(UrlRequest request)
        {
            mRequest = request;

            if (mRequest == null || string.IsNullOrEmpty(mRequest.url))
                return;

            mRequestTask = new Task(Request());
            string taskName = string.Format("{0}?timestap={1}", request.url, DateTime.Now.Millisecond.ToString());
            TaskManager.instance.AddTask(taskName, mRequestTask);
            mRequestTask.Start();
        }

        /// <summary>
        /// Request to server.
        /// </summary>
        /// <returns>IEnumerator.</returns>
        private IEnumerator Request()
        {
            WWW www = null;
            WWWForm form = null;
            string url = mRequest.url;

            if (mRequest.data is byte[])
            {
                // Post data.
                mRequest.method = UrlRequestMethod.Post;
            }
            else if (mRequest.data is UrlVariables)
            {
                // If transform data by get method, then add variables to the end of url.
                if (mRequest.method == UrlRequestMethod.Get)
                {
                    url += ((UrlVariables)mRequest.data).ToString();
                }
                else if (mRequest.method == UrlRequestMethod.Post)
                {
                    form = new WWWForm();
                    UrlVariables variables = (UrlVariables)mRequest.data;

                    foreach (KeyValuePair<string, string> kvp in variables.variables)
                    {
                        form.AddField(kvp.Key, kvp.Value);
                    }
                }
            }

            if (form != null)
            {
                www = new WWW(url, form);
            }
            else
            {
                if (mRequest.method == UrlRequestMethod.Post)
                {
                    www = (mRequest.requestHeaders != null) ?
                        new WWW(url, (byte[])mRequest.data, mRequest.requestHeaders) :
                        new WWW(url, (byte[])mRequest.data);
                }
                else if (mRequest.method == UrlRequestMethod.Get)
                {
                    www = new WWW(url);
                }
            }

            DispatchEvent(new HttpEvent(HttpEvent.OPEN, this, mRequest));

            yield return www;

            int statusCode = 0;
            UrlResponse response = null;

            if (www.responseHeaders.Count > 0)
            {
                statusCode = ParseHttpStatusCode(www.responseHeaders);
                response = new UrlResponse(statusCode, www);
            }

            // Remove task from TaskManager.
            if (mRequestTask != null)
                TaskManager.instance.RemoveTask(mRequestTask);

            if (www.responseHeaders.Count > 0)
                DispatchEvent(new HttpStatusEvent(HttpStatusEvent.HTTP_STATUS, this, statusCode));

            // Handle response data from server.
            if (!string.IsNullOrEmpty(www.error))
            {
                Debug.LogWarning("Http request error: " + www.error);

                if (mRequest.errorCallback != null)
                    mRequest.errorCallback.Invoke(www.error);

                DispatchEvent(new HttpEvent(HttpEvent.ERROR, this, response));
            }
            else
            {
                HttpEvent httpEvent = null;
                mBytesLoaded = www.bytesDownloaded;
                mBytesTotal = www.size;

                if (!www.isDone)
                {
                    // In progress of downloading.
                    httpEvent = new HttpEvent(HttpEvent.PROGRESS, this, response);
                    httpEvent.progress = www.progress;
                    DispatchEvent(httpEvent);
                }
                else
                {
                    // Dispatch event HttpStatusEvent.HTTP_STATUS.
                    if (www.responseHeaders.Count > 0)
                    {
                        if (statusCode > 0)
                        {
                            HttpStatusEvent httpStatusEvent = new HttpStatusEvent(HttpStatusEvent.HTTP_RESPONSE_STATUS, this, statusCode);
                            httpStatusEvent.responseHeaders = www.responseHeaders;
                            DispatchEvent(httpStatusEvent);
                        }
                    }

                    object data = ReflectionUtility.GetObjectPropertyValue(www, mDataFormatBindingProperties[mDataFormat]);

                    if (response != null)
                        response.data = data;

                    // Callback invoke
                    if (mRequest != null && mRequest.completeCallback != null)
                        mRequest.completeCallback.Invoke(response);

                    // Dispatch event HttpEvent.COMPLETE.
                    httpEvent = new HttpEvent(HttpEvent.COMPLETE, this, response);
                    DispatchEvent(httpEvent);
                }
            }

            www.Dispose();
        }
    }
}