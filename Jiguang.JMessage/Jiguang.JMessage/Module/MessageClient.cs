using Jiguang.JMessage.Model;
using Jiguang.JSMS.Model;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JMessage.Module
{
    /// <summary>
    /// 消息相关 API。
    /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_17"/></para>
    /// </summary>
    public class MessageClient
    {

        public async Task<HttpResponse> SendAsync(Message message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));

            HttpContent httpContent = new StringContent(message.ToString(), Encoding.UTF8);
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/messages", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
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
    }
}
