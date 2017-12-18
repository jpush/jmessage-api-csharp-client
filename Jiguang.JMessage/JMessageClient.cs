using Jiguang.JMessage.Message;
using Jiguang.JMessage.Common;
using Jiguang.JMessage.User;
using Jiguang.JMessage.Report;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Jiguang.JMessage.Group;
using Jiguang.JMessage.Chatroom;

namespace Jiguang.JMessage
{
    public class JMessageClient
    {
        public static HttpClient HttpClient;

        public UserClient User { get; }
        public MessageClient Message { get; }
        public GroupClient Group { get; }
        public ReportClient Report { get; }
        public ChatroomClient Chatroom { get; }

        private string AppKey;
        private string MasterSecret;

        static JMessageClient()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.im.jpush.cn")
            };
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public JMessageClient(string appKey, string masterSecret)
        {
            if (string.IsNullOrEmpty(appKey))
                throw new ArgumentNullException(nameof(appKey));

            if (string.IsNullOrEmpty(masterSecret))
                throw new ArgumentNullException(nameof(masterSecret));

            AppKey = appKey;
            MasterSecret = masterSecret;

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(appKey + ":" + masterSecret));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

            User = new UserClient();
            Message = new MessageClient();
            Group = new GroupClient();
            Report = new ReportClient(appKey, masterSecret);
            Chatroom = new ChatroomClient();
        }

        /// <summary>
        /// <see cref="AddSensitiveWords(List{string})"/>
        /// </summary>
        public async Task<HttpResponse> AddSensitiveWordsAsync(List<string> wordList)
        {
            if (wordList == null)
                throw new ArgumentNullException(nameof(wordList));

            var url = "/v1/sensitiveword";

            JArray body = JArray.FromObject(wordList);
            var httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 为应用添加敏感词。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_49"/></para>
        /// </summary>
        /// <param name="wordList">敏感词字符串列表。每个词长度最多为 10，默认支持 100 个敏感词，有更多需求请访问官网联系商务。</param>
        public HttpResponse AddSensitiveWords(List<string> wordList)
        {
            Task<HttpResponse> task = Task.Run(() => AddSensitiveWordsAsync(wordList));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> UpdateSensitiveWordAsync(string oldWord, string newWord)
        {
            if (string.IsNullOrEmpty(oldWord))
                throw new ArgumentNullException(nameof(oldWord));

            if (string.IsNullOrEmpty(newWord))
                throw new ArgumentNullException(nameof(newWord));

            var url = "/v1/sensitiveword";

            JObject body = new JObject
            {
                { "old_word", oldWord },
                { "new_word", newWord }
            };

            var httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await HttpClient.PutAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 更新敏感词。
        /// </summary>
        /// <param name="oldWord">待更新的敏感词。</param>
        /// <param name="newWord">新的敏感词。</param>
        public HttpResponse UpdateSensitiveWord(string oldWord, string newWord)
        {
            Task<HttpResponse> task = Task.Run(() => UpdateSensitiveWordAsync(oldWord, newWord));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> RemoveSensitiveWordAsync(string word)
        {
            if (string.IsNullOrEmpty(word))
                throw new ArgumentNullException(nameof(word));

            JObject body = new JObject
            {
                { "word", word }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("https://api.im.jpush.cn/v1/sensitiveword"),
                Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 删除指定敏感词。
        /// </summary>
        /// <param name="word">待删除的敏感词。</param>
        public HttpResponse RemoveSensitiveWord(string word)
        {
            Task<HttpResponse> task = Task.Run(() => RemoveSensitiveWordAsync(word));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetSensitiveWords(int, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetSensitiveWordsAsync(int start, int count)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (count < 0 || count > 2000)
                throw new ArgumentOutOfRangeException(nameof(count));

            var url = $"/v1/sensitiveword?start={start}&count={count}";

            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取敏感词列表。
        /// </summary>
        /// <param name="start">起始序号，从 0 开始。</param>
        /// <param name="count">查询条数，最多为 2000。</param>
        public HttpResponse GetSensitiveWords(int start, int count)
        {
            Task<HttpResponse> task = Task.Run(() => GetSensitiveWordsAsync(start, count));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="SetSensitiveWordsStatus(bool)"/>
        /// </summary>
        public async Task<HttpResponse> SetSensitiveWordsStatusAsync(bool enable)
        {
            int status = enable ? 1 : 0;

            var url = $"/v1/sensitiveword/status?status={status}";

            var httpContent = new StringContent("", Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await HttpClient.PutAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 设置敏感词功能状态。
        /// </summary>
        /// <param name="enable">true: 启用；false: 禁用。</param>
        public HttpResponse SetSensitiveWordsStatus(bool enable)
        {
            Task<HttpResponse> task = Task.Run(() => SetSensitiveWordsStatusAsync(enable));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetSensitiveStatusWordsAsync()
        {
            var url = $"/v1/sensitiveword/status";

            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取敏感词功能当前状态。
        /// </summary>
        public HttpResponse GetSensitiveWordsStatus()
        {
            Task<HttpResponse> task = Task.Run(() => GetSensitiveStatusWordsAsync());
            task.Wait();
            return task.Result;
        }
    }
}
