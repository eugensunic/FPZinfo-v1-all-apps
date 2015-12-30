using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace FPZinfo
{
    class HttpClient : WebClient
    {
        CookieContainer cookieContainer = new CookieContainer();

        public HttpClient()
            : base()
        {

        }

        /// <summary>
        /// Get the string by URI string.
        /// </summary>
        /// <param name="strUri">The Uri the request is sent to.</param>
        /// <returns>string</returns>
        public async Task<string> GetStringAsync(string strUri)
        {
            Uri uri = new Uri(strUri);
            string result = await GetStringAsync(uri);
            return result;
        }

        /// <summary>
        /// Get the string by URI.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>string</returns>
        public Task<string> GetStringAsync(Uri requestUri)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

            try
            {
                this.DownloadStringCompleted += (s, e) =>
                {
                    if (e.Error == null)
                    {
                        tcs.TrySetResult(e.Result);
                    }
                    else
                    {
                        tcs.TrySetException(e.Error);
                    }
                };

                this.DownloadStringAsync(requestUri);

            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            if (tcs.Task.Exception != null)
            {
                throw tcs.Task.Exception;
            }

            return tcs.Task;
        }

        /// <summary>
        ///  Send a GET request to the specified Uri and return the response body as a
        ///  byte array in an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>
        /// Returns System.Threading.Tasks.Task TResult>.The task object
        /// representing the asynchronous operation.
        ///  </returns>
        public async Task<byte[]> GetByteArrayAsync(string requestUri)
        {
            Uri uri = new Uri(requestUri);
            byte[] data = await GetByteArrayAsync(uri);
            return data;
        }

        /// <summary>
        ///  Send a GET request to the specified Uri and return the response body as a
        ///  byte array in an asynchronous operation.
        /// </summary>
        /// <param name="requestUri">The Uri the request is sent to.</param>
        /// <returns>Returns byte array.The task object
        /// representing the asynchronous operation.</returns>
        public Task<byte[]> GetByteArrayAsync(Uri requestUri)
        {
            TaskCompletionSource<byte[]> tcs = new TaskCompletionSource<byte[]>();

            try
            {
                this.DownloadStringCompleted += (s, e) =>
                {
                    if (e.Error == null)
                    {
                        byte[] data = ConvertStringToByte(e.Result);
                        tcs.TrySetResult(data);
                    }
                    else
                    {
                        tcs.TrySetException(e.Error);
                    }
                };

                this.DownloadStringAsync(requestUri);

            }
            catch (Exception ex)
            {
                tcs.TrySetException(ex);
            }

            if (tcs.Task.Exception != null)
            {
                throw tcs.Task.Exception;
            }

            return tcs.Task;
        }

        /// <summary>
        /// Override the GetWebRequest method.
        /// </summary>
        /// <param name="address">The Uri the request is sent to.</param>
        /// <returns>HttpWebRequest</returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request =
               base.GetWebRequest(address) as HttpWebRequest;

            if (request != null)
            {
                request.Method = "GET";
                request.CookieContainer = cookieContainer;
            }

            return request;
        }

        /// <summary>
        /// Convert String to byte array.
        /// </summary>
        /// <param name="strTemp">string</param>
        /// <returns>byte array</returns>
        private static byte[] ConvertStringToByte(string strTemp)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] data = encoding.GetBytes(strTemp);

            return data;
        }
    }
}
