using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace F20SC_Browser {

    /// <summary>
    /// The Web Response Struct hold data that is recieved from a GET request. Status Code, Response Headers and the Response Body
    /// </summary>
    public struct WebResponse {
        public string url { private set; get; }
        public int statusCode { private set; get; }
        public string responseBody { private set; get; }
        public string responseHeaders { private set; get; }

        public WebResponse(string url, int statusCode, string responseBody, string responseHeaders) {
            this.url = url;
            this.statusCode = statusCode;
            this.responseBody = responseBody;
            this.responseHeaders = responseHeaders;
        }

    };

    /// <summary>
    /// The Web Requests class is for creating GET requests.
    /// </summary>
    public class WebRequests {        

        public WebRequests() {
        }

        /// <summary>
        /// GET request for a given URL.
        /// </summary>
        /// <param name="url"> The input URL  </param>
        /// <returns>Returns the struct containing the status code, body and header. </returns>
        public WebResponse GetURL(string url) {

            int statusCode = 0;
            string body = string.Empty;
            string header = string.Empty;

            

            try {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream)) {
                    body = reader.ReadToEnd();
                    statusCode = (int)response.StatusCode;

                    for(int i = 0; i < response.Headers.Count; i++) {
                        header += response.Headers.Keys[i] + ": " + response.Headers[i] + Environment.NewLine;
                    }
                }
            } catch (Exception exception) {

                if(exception is WebException) {
                    if (((WebException)exception).Response == null) {
                        statusCode = -1;
                    } else {
                        statusCode = (int)((HttpWebResponse)((WebException)exception).Response).StatusCode;
                    }
                }
               
                if(exception is UriFormatException) {
                    statusCode = -1;
                }

            }

            return new WebResponse(url, statusCode, body, header);
        }

    }
}
