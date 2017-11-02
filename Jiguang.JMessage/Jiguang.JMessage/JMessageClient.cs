using Jiguang.JMessage.Message;
using Jiguang.JMessage.Module;
using Jiguang.JMessage.Report;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Jiguang.JMessage
{
    public class JMessageClient
    {
        public static HttpClient HttpClient;

        public UserClient User { get; }
        public MessageClient Message { get; }
        public GroupClient Group { get; }
        public ReportClient Report { get; }

        public static string AppKey;
        public static string MasterSecret;

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
        }
    }
}
