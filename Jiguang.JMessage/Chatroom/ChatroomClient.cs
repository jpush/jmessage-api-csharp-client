using Jiguang.JMessage.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JMessage.Chatroom
{
    /// <summary>
    /// 聊天室相关 API。
    /// <para>https://docs.jiguang.cn/jmessage/server/rest_api_im/#_58</para>
    /// </summary>
    public class ChatroomClient
    {
        public async Task<HttpResponse> CreateAsync(ChatroomInfo chatroomInfo)
        {
            if (chatroomInfo == null)
                throw new ArgumentNullException(nameof(chatroomInfo));

            if (string.IsNullOrEmpty(chatroomInfo.Name))
                throw new ArgumentNullException(nameof(chatroomInfo.Name));

            if (string.IsNullOrEmpty(chatroomInfo.Owner))
                throw new ArgumentNullException(nameof(chatroomInfo.Owner));

            HttpContent httpContent = new StringContent(chatroomInfo.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/chatroom/", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 创建聊天室。
        /// </summary>
        /// <param name="chatroomInfo">聊天室对象。</param>
        public HttpResponse Create(ChatroomInfo chatroomInfo)
        {
            Task<HttpResponse> task = Task.Run(() => CreateAsync(chatroomInfo));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetChatroomInfoByIdAsync(List<long> chatroomIdList)
        {
            if (chatroomIdList == null || chatroomIdList.Count == 0)
                throw new ArgumentNullException(nameof(chatroomIdList));

            string jsonStr = JArray.FromObject(chatroomIdList).ToString();
            HttpContent httpContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/chatroom/batch", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 根据聊天室 id 获取聊天室详情。
        /// </summary>
        /// <param name="chatroomIdList">聊天室 id 列表。</param>
        public HttpResponse GetChatroomInfoById(List<long> chatroomIdList)
        {
            Task<HttpResponse> task = Task.Run(() => GetChatroomInfoByIdAsync(chatroomIdList));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetChatroomInfoOfUserAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            var url = $"/v1/users/{username}/chatroom";
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取指定用户的聊天室详情。
        /// </summary>
        /// <param name="username">用户名</param>
        public HttpResponse GetChatroomInfoOfUser(string username)
        {
            Task<HttpResponse> task = Task.Run(() => GetChatroomInfoOfUserAsync(username));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetChatroomInfoAsync(int start, int count)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            var url = $"/v1/chatroom?start={start}&count={count}";
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.GetAsync(url).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取当前应用下的聊天室列表。
        /// </summary>
        /// <param name="start">起始</param>
        /// <param name="count"></param>
        public HttpResponse GetChatroomInfo(int start, int count)
        {
            Task<HttpResponse> task = Task.Run(() => GetChatroomInfoAsync(start, count));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> UpdateChatroomInfoAsync(ChatroomInfo chatroomInfo)
        {
            if (chatroomInfo == null)
                throw new ArgumentNullException(nameof(chatroomInfo));

            if (chatroomInfo.Id == null)
                throw new ArgumentNullException(nameof(chatroomInfo));

            var url = $"/v1/chatroom/{chatroomInfo.Id}";
            var httpContent = new StringContent(chatroomInfo.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        public HttpResponse UpdateChatroomInfo(ChatroomInfo chatroomInfo)
        {
            Task<HttpResponse> task = Task.Run(() => UpdateChatroomInfoAsync(chatroomInfo));
            task.Wait();
            return task.Result;
        }
    }
}
