using System;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace jmessage.message
{
    public class FileClient : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const string MESSAGE_PATH = "/v1/resource";

        private string appKey;
        private string masterSecret;

        public FileClient(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!string.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public ResponseWrapper getMedia(string mediaId)
        {
            Preconditions.checkArgument(mediaId != null, "mediaId should not be empty");
            string url = HOST_NAME_SSL;
            url += MESSAGE_PATH;
            url += "?mediaId=";
            url += mediaId;
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public string HttpPostData(string url, int timeOut, string fileKeyName, string filePath, NameValueCollection stringDict)
        {
            string responseContent;
            var memStream = new MemoryStream();
            var webRequest = (HttpWebRequest)WebRequest.Create(url);
            // 边界符
            var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
            // 边界符
            var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            // 最后的结束符
            var endBoundary = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n");
            webRequest.Headers.Add("Authorization", "Basic " + Authorization());
            webRequest.Method = "POST";
            webRequest.Timeout = timeOut;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;

            // 写入文件
            const string filePartHeader = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                 "Content-Type: application/octet-stream\r\n\r\n";
            var header = string.Format(filePartHeader, fileKeyName, filePath);
            var headerbytes = Encoding.UTF8.GetBytes(header);

            memStream.Write(beginBoundary, 0, beginBoundary.Length);
            memStream.Write(headerbytes, 0, headerbytes.Length);

            var buffer = new byte[1024];
            int bytesRead; // = 0

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }

            if (stringDict != null)
            {
                // 写入字符串的 Key
                var stringKeyHeader = "\r\n--" + boundary +
                                       "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                       "\r\n\r\n{1}\r\n";

                foreach (byte[] formitembytes in from string key in stringDict.Keys
                                                 select string.Format(stringKeyHeader, key, stringDict[key])
                                                 into formitem
                                                 select Encoding.UTF8.GetBytes(formitem))
                {
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }
            }
            else
            {
                // 写入字符串的 key
                var stringKeyHeader = "\r\n--" + boundary +
                                       "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                                       "\r\n\r\n{1}\r\n";

                byte[] formitembytes = new byte[0];
                formitembytes = null;
                {
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }
            }

            // 写入最后的结束边界符
            memStream.Write(endBoundary, 0, endBoundary.Length);
            webRequest.ContentLength = memStream.Length;

            var requestStream = webRequest.GetRequestStream();
            memStream.Position = 0;

            var tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();
            try
            {
                var httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

                using (var httpStreamReader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.GetEncoding("utf-8")))
                {
                    responseContent = httpStreamReader.ReadToEnd();
                }

                fileStream.Close();
                httpWebResponse.Close();
                webRequest.Abort();

                return responseContent;
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpStatusCode errorCode = ((HttpWebResponse)e.Response).StatusCode;
                    string statusDescription = ((HttpWebResponse)e.Response).StatusDescription;
                    using (StreamReader sr = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream(), Encoding.UTF8))
                    {
                        responseContent = sr.ReadToEnd();
                    }
                    HttpStatusCode responseCode = errorCode;
                    string exceptionstring = e.Message;
                    Debug.Print(e.Message);
                    Console.WriteLine(responseContent);
                    Console.WriteLine(string.Format("fail  to get response - {0}", errorCode) + " " + DateTime.Now);
                    return responseContent;
                }
                else
                {
                    return "fail to get response";
                }
            }
        }

        public string ToString(MessagePayload payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string Authorization()
        {
            Debug.Assert(!string.IsNullOrEmpty(appKey));
            Debug.Assert(!string.IsNullOrEmpty(masterSecret));

            string origin = appKey + ":" + masterSecret;
            return Base64.getBase64Encode(origin);
        }
    }
}
