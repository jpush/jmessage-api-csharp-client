using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Jiguang.JMessage
{
    public class JMessageClient
    {
        public static readonly HttpClient HttpClient;

        public User User { get; set; }

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

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes(appKey + ":" + masterSecret));
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
        }
    }
}
