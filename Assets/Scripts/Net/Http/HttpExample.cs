using QuickUnity.Net.Http;
using UnityEngine;

namespace QuickUnity.Examples.Net.Http
{
    /// <summary>
    /// Http request example.
    /// </summary>
    public class HttpExample : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            UrlLoader loader = new UrlLoader();
            UrlRequest request = new UrlRequest("http://docs.unity3d.com/ScriptReference/index.html", OnComplete, OnError);
            //UrlRequest request = new UrlRequest("http://docs.unity3d.com/xxx", OnComplete, OnError);
            loader.Load(request);
        }

        /// <summary>
        /// Called when [complete].
        /// </summary>
        /// <param name="data">The data.</param>
        private void OnComplete(UrlResponse data)
        {
            Debug.LogFormat("Response data: {0}", data.data);
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="message">The message.</param>
        private void OnError(string message)
        {
            Debug.LogErrorFormat("Error: {0}", message);
        }
    }
}