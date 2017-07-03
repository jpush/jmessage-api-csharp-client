using System;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace jmessage.message
{
    /// <summary>
    /// Description of ImportTool.
    /// </summary>
    public sealed class FileUpload : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const string MESSAGE_PATH = "/v1/resource";

        private string appKey;
        private string masterSecret;

        public FileUpload(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!string.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public void HttpUploadFile(string url, string file, string paramName, string contentType, NameValueCollection nvc)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";
            wr.KeepAlive = true;
            wr.Credentials = CredentialCache.DefaultCredentials;
            wr.Headers.Add("Authorization", "Basic " + this.Authorization());

            Stream rs = wr.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in nvc.Keys)
            {
                rs.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                rs.Write(formitembytes, 0, formitembytes.Length);
            }
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, paramName, file, contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);
            rs.Close();
            rs = null;

            WebResponse wresp = null;
            try
            {
                wresp = wr.GetResponse();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    HttpStatusCode errorCode = ((HttpWebResponse)e.Response).StatusCode;
                    string statusDescription = ((HttpWebResponse)e.Response).StatusDescription;
                    using (StreamReader sr = new StreamReader(((HttpWebResponse)e.Response).GetResponseStream(), System.Text.Encoding.UTF8))
                    {
                        string responseContent = sr.ReadToEnd();
                        Console.WriteLine(responseContent);
                    }
                    Debug.Print(e.Message);
                    Console.WriteLine(string.Format("fail  to get response - {0}", errorCode) + " " + DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (wresp != null)
                {
                    wresp.Close();
                    wresp = null;
                }
                wr = null;
            }
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