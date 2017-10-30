using Jiguang.JMessage.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JMessage.Message
{
    /// <summary>
    /// 消息相关 API。
    /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_17"/></para>
    /// </summary>
    public class MessageClient
    {

        /// <summary>
        /// 发送消息。
        /// <para>https://docs.jiguang.cn/jmessage/server/rest_api_im/#_17</para>
        /// </summary>
        /// <param name="msgJsonStr">消息的 Json 字符串。用户可参照文档自行构造。</param>
        public async Task<HttpResponse> SendAsync(string msgJsonStr)
        {
            if (string.IsNullOrEmpty(msgJsonStr))
                throw new ArgumentNullException(nameof(msgJsonStr));

            HttpContent httpContent = new StringContent(msgJsonStr, Encoding.UTF8);
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/messages", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        public async Task<HttpResponse> SendAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            string msgJsonStr = message.ToString();
            return await SendAsync(msgJsonStr).ConfigureAwait(false);
        }

        /// <summary>
        /// 发送消息。
        /// </summary>
        /// <param name="message"></param>
        public HttpResponse Send(Message message)
        {
            Task<HttpResponse> task = Task.Run(() => SendAsync(message));
            task.Wait();
            return task.Result;
        }
        
        /// <summary>
        /// <seealso cref="Retract(string, string)"/>
        /// </summary>
        public async Task<HttpResponse> RetractAsync(string msgId, string username)
        {
            if (string.IsNullOrEmpty(msgId))
                throw new ArgumentNullException(nameof(msgId));

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            var url = $"/v1/messages/{username}/{msgId}/retract";
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, null).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 消息撤回。
        /// </summary>
        /// <param name="msgId">要撤回的消息 Id。</param>
        /// <param name="username">发送该消息的用户名。</param>
        public HttpResponse Retract(string msgId, string username)
        {
            Task<HttpResponse> task = Task.Run(() => RetractAsync(msgId, username));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="FileDownload(string)"/>
        /// </summary>
        public async Task<HttpResponse> FileDownloadAsync(string mediaId)
        {
            if (string.IsNullOrEmpty(mediaId))
                throw new ArgumentNullException(nameof(mediaId));

            var url = $"/v1/resource?mediaId={mediaId}";
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 文件下载。
        /// <para>https://docs.jiguang.cn/jmessage/server/rest_api_im/#_20</para>
        /// </summary>
        /// <param name="mediaId">资源的 mediaId（包括用户信息中的 avatar 字段）。</param>
        public HttpResponse FileDownload(string mediaId)
        {
            Task<HttpResponse> task = Task.Run(() => FileDownloadAsync(mediaId));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="FileUpload(string, string)"/>
        /// </summary>
        public async Task<HttpResponse> FileUploadAaync(string filePath, string type)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentNullException(nameof(filePath));

            if (string.IsNullOrEmpty(type))
                throw new ArgumentNullException(nameof(type));

            filePath = filePath.Trim();
            type = type.Trim();

            if (type != "image" || type != "voice" || type != "file")
                throw new ArgumentException("type is error.");

            MultipartFormDataContent form = new MultipartFormDataContent();
            ByteArrayContent fileContent = new ByteArrayContent(System.IO.File.ReadAllBytes(filePath));
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = filePath };
            form.Add(fileContent);

            var url = $"/v1/resource?type={type}";
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, form).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 文件上传。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_20"/></para>
        /// </summary>
        /// <param name="filePath">文件本地路径。</param>
        /// <param name="type">文件类型。支持："image", "voice", "file"。</param>
        public HttpResponse FileUpload(string filePath, string type)
        {
            Task<HttpResponse> task = Task.Run(() => FileUploadAaync(filePath, type));
            task.Wait();
            return task.Result;
        }
    }
}
