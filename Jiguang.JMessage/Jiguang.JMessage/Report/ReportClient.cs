using Jiguang.JMessage.Model;
using System;
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

        /// <summary>
        /// <see cref="GetMessageHistory(int, string, string)"/>
        /// </summary>
        public async Task<HttpResponse> GetMessageHistoryAsync(int count, string beginTime, string endTime)
        {
            if (count < 1 || count > 1000)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (string.IsNullOrEmpty(beginTime))
                throw new ArgumentNullException(nameof(beginTime));

            if (string.IsNullOrEmpty(endTime))
                throw new ArgumentNullException(nameof(endTime));

            var url = $"/messages?count={count}&begin_time={beginTime}&end_time={endTime}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取消息历史。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im_report_v2/#_2"/></para>
        /// </summary>
        /// <param name="count">查询的总条数，每次最多 1000 条。</param>
        /// <param name="beginTime">记录开始时间，格式 yyyy-MM-dd HH:mm:ss。</param>
        /// <param name="endTime">记录结束时间，格式 yyyy-MM-dd HH:mm:ss。和 beginTime 之间最大不能相差 7 天。</param>
        public HttpResponse GetMessageHistory(int count, string beginTime, string endTime)
        {
            Task<HttpResponse> task = Task.Run(() => GetMessageHistoryAsync(count, beginTime, endTime));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetMessageHistoryAsync(string cursor)
        {
            if (string.IsNullOrEmpty(cursor))
                throw new ArgumentNullException(nameof(cursor));

            var url = $"/messages?cursor={cursor}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取消息历史。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im_report_v2/#_2"/></para>
        /// </summary>
        /// <param name="cursor">当第一次请求后如果后面有数据，会返回一个 cursor 回来用这个获取接下来的消息（cursor 有效时间是 120 秒，过期后需要重第一个请求获取，重新遍历）。</param>
        public HttpResponse GetGroupMessageHistory(string cursor)
        {
            Task<HttpResponse> task = Task.Run(() => GetMessageHistoryAsync(cursor));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetUserMessageHistoryAsync(string username, int count, string beginTime, string endTime)
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
        /// 获取指定用户的历史消息，结果按发送时间升序排序。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im_report_v2/#_3"/></para>
        /// </summary>
        /// <param name="username">待查询用户的用户名。</param>
        /// <param name="count">查询的总条数，一次最多 1000 条。</param>
        /// <param name="beginTime">记录开始时间，格式 yyyy-MM-dd HH:mm:ss</param>
        /// <param name="endTime">记录结束时间，格式 yyyy-MM-dd HH:mm:ss</param>
        public HttpResponse GetUserMessageHistory(string username, int count, string beginTime, string endTime)
        {
            Task<HttpResponse> task = Task.Run(() => GetUserMessageHistoryAsync(username, count, beginTime, endTime));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetUserMessageHistoryAsync(string username, string cursor)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (string.IsNullOrEmpty(cursor))
                throw new ArgumentNullException(nameof(cursor));

            var url = $"/users/{username}/messages?cursor={cursor}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取用户消息，结果按发送时间升序排序。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im_report_v2/#_3"/></para>
        /// </summary>
        /// <param name="username">待查询用户的用户名。</param>
        /// <param name="cursor">当第一次请求后如果后面有数据，会返回一个 cursor 回来用这个获取接下来的消息（cursor 有效时间是 120 秒，过期后需要重第一个请求获取，重新遍历）。</param>
        public HttpResponse GetUserMessageHistory(string username, string cursor)
        {
            Task<HttpResponse> task = Task.Run(() => GetUserMessageHistoryAsync(username, cursor));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetGroupMessageHistory(string, int, string, string)"/>
        /// </summary>
        public async Task<HttpResponse> GetGroupMessageHistoryAsync(string groupId, int count, string beginTime, string endTime)
        {
            if (string.IsNullOrEmpty(groupId))
                throw new ArgumentNullException(nameof(groupId));

            if (count < 1 || count > 1000)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (string.IsNullOrEmpty(beginTime))
                throw new ArgumentNullException(nameof(beginTime));

            if (string.IsNullOrEmpty(endTime))
                throw new ArgumentNullException(nameof(endTime));

            var url = $"/groups/{groupId}/messages?count={count}&begin_time={beginTime}&end_time={endTime}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取群组消息历史。
        /// </summary>
        /// <param name="groupId">群组 Id。</param>
        /// <param name="count">查询的总条数，一次最多 1000 条。</param>
        /// <param name="beginTime">记录开始时间，格式 yyyy-MM-dd HH:mm:ss</param>
        /// <param name="endTime">记录结束时间，格式 yyyy-MM-dd HH:mm:ss</param>
        public HttpResponse GetGroupMessageHistory(string groupId, int count, string beginTime, string endTime)
        {
            Task<HttpResponse> task = Task.Run(() => GetGroupMessageHistoryAsync(groupId, count, beginTime, endTime));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> GetGroupMessageHistoryAsync(string groupId, string cursor)
        {
            if (string.IsNullOrEmpty(groupId))
                throw new ArgumentNullException(nameof(groupId));

            if (string.IsNullOrEmpty(cursor))
                throw new ArgumentNullException(nameof(cursor));

            var url = $"/groups/{groupId}/messages?cursor={cursor}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取群组消息。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im_report_v2/#_4"/></para>
        /// </summary>
        /// <param name="username">待查询群组的 Id。</param>
        /// <param name="cursor">当第一次请求后如果后面有数据，会返回一个 cursor 回来用这个获取接下来的消息（cursor 有效时间是 120 秒，过期后需要重第一个请求获取，重新遍历）。</param>
        public HttpResponse GetGroupMessageHistory(string groupId, string cursor)
        {
            Task<HttpResponse> task = Task.Run(() => GetUserMessageHistoryAsync(groupId, cursor));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetUserStatistic(string, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetUserStatisticAsync(string startTime, int duration)
        {
            if (string.IsNullOrEmpty(startTime))
                throw new ArgumentNullException(nameof(startTime));

            if (duration < 0 || duration > 60)
                throw new ArgumentOutOfRangeException(nameof(duration));

            var url = $"/statistic/users?time_unit=DAY&start={startTime}&duration={duration}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取用户统计数据（VIP only）。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im_report_v2/#_5"/></para>
        /// </summary>
        /// <param name="username">待查询用户的用户名。</param>
        /// <param name="startTime">开始时间，格式为 yyyy-MM-dd。</param>
        /// <param name="duration">持续时长，单位天，最长为 60 天。</param>
        public HttpResponse GetUserStatistic(string startTime, int duration)
        {
            Task<HttpResponse> task = Task.Run(() => GetUserStatisticAsync(startTime, duration));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetGroupStatistic(string, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetGroupStatisticAsync(string startTime, int duration)
        {
            if (string.IsNullOrEmpty(startTime))
                throw new ArgumentNullException(nameof(startTime));

            if (duration < 0 || duration > 60)
                throw new ArgumentOutOfRangeException(nameof(duration));

            var url = $"/statistic/groups?time_unit=DAY&start={startTime}&duration={duration}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取群组统计（VIP only）。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im_report_v2/#_7"/></para>
        /// </summary>
        /// <param name="startTime">开始时间，格式为：yyyy-MM-dd。</param>
        /// <param name="duration">持续时长，单位天，最长 60 天。</param>
        public HttpResponse GetGroupStatistic(string startTime, int duration)
        {
            Task<HttpResponse> task = Task.Run(() => GetGroupStatisticAsync(startTime, duration));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetMessagesStatistic(string, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetMessagesStatisticAsync(string startTime, int duration)
        {
            if (string.IsNullOrEmpty(startTime))
                throw new ArgumentNullException(nameof(startTime));

            if (duration < 0 || duration > 60)
                throw new ArgumentOutOfRangeException(nameof(duration));

            var url = $"/statistic/messages?time_unit=DAY&start={startTime}&duration={duration}";

            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(url).ConfigureAwait(false);
            string content = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, content);
        }

        /// <summary>
        /// 获取消息统计（VIP only）。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im_report_v2/#_6"/></para>
        /// </summary>
        /// <param name="startTime">开始时间，格式为：yyyy-MM-dd。</param>
        /// <param name="duration">持续时长，单位天，最长 60 天。</param>
        public HttpResponse GetMessagesStatistic(string startTime, int duration)
        {
            Task<HttpResponse> task = Task.Run(() => GetMessagesStatisticAsync(startTime, duration));
            task.Wait();
            return task.Result;
        }
    }
}
