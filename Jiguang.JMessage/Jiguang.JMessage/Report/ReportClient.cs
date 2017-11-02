using Jiguang.JMessage.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JMessage.Report
{
    public class ReportClient
    {
        private HttpClient httpClient;

        public ReportClient(string appKey, string masterSecret)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://report.im.jpush.cn/v2")
            };
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(appKey + ":" + masterSecret));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
        }

        public async Task<HttpResponse> GetMessageHistoryOfUserAsync(string username, int count, string beginTime, string endTime)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (count < 1 || count > 1000)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (string.IsNullOrEmpty(beginTime))
                throw new ArgumentNullException(nameof(beginTime));

            if (string.IsNullOrEmpty(endTime))
                throw new ArgumentNullException(nameof(endTime));

            var url = $"/users/{username}/messages?count={count}&begin_time={beginTime}&end_time={endTime}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取指定用户的历史消息。
        /// </summary>
        /// <param name="username">待查询用户的用户名。</param>
        /// <param name="count">查询的总条数，一次最多 1000。</param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        public HttpResponse GetMessageHistoryOfUser(string username, int count, string beginTime, string endTime)
        {
            Task<HttpResponse> task = Task.Run(() => GetMessageHistoryOfUserAsync(username, count, beginTime, endTime));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetMessageHistoryOfUserAsync(string username, string cursor)
        {

        }

        public HttpResponse GetMessageHistoryOfUser(string username, string cursor)
        {
            Task<HttpResponse> task = Task.Run(() => GetMessageHistoryOfUserAsync(username, cursor));
            task.Wait();
            return task.Result;
        }
    }
}
