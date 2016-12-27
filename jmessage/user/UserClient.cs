using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.user
{
    public class UserClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String ADMIN_PATH = "/v1/admins/";
        private const String USER_PATH = "/v1/users/";
        private const String PUSH_PATH = "/v3/push";

        private String appKey;
        private String masterSecret;
        public UserClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper registUser(List <UserPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return registUser(payloadJson);
        }
        public ResponseWrapper registUser(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper registAdmin(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            payload.Check();
            String payloadJson = payload.ToString(payload);
            return registAdmin(payloadJson);
        }
        public ResponseWrapper registAdmin(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");

            String url = HOST_NAME_SSL;
            url += ADMIN_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public string ToString(List<UserPayload> payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public  String Authorization()
        {

            Debug.Assert(!string.IsNullOrEmpty(this.appKey));
            Debug.Assert(!string.IsNullOrEmpty(this.masterSecret));

            String origin = this.appKey + ":" + this.masterSecret;
            return Base64.getBase64Encode(origin);
        }
    }
}
