using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.cross;
using jmessage.cross.user;

namespace jmessage.user
{
    public class CrossUserClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String CROSS_GROUP_PATH = "/v1/cross/groups/";
        private const String CROSS_USER_PATH = "/v1/cross/users/";

        private String appKey;
        private String masterSecret;
        public CrossUserClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public ResponseWrapper crossAppsAddBlacklist(string username, List<CrossBlacklistPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            String payloadJson = this.ToString(payload);
            return crossAppsAddBlacklist(username, payloadJson);
        }
        public ResponseWrapper crossAppsAddBlacklist(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload String should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/blacklist";
            ResponseWrapper result = sendPut(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossAppsRemoveBlacklist(string username, List<CrossBlacklistPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            String payloadJson = this.ToString(payload);
            return crossAppsRemoveBlacklist(username, payloadJson);
        }
        public ResponseWrapper crossAppsRemoveBlacklist(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload String should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/blacklist";
            ResponseWrapper result = sendDelete(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossGetBlacklist(string username)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/blacklist";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper crossAppsSetNodisturb(string username, List<CrossNodisturbPayload> payload)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/nodisturb";
            string payloadString = this.ToString(payload);
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public string ToString(List<CrossBlacklistPayload> payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public string ToString(List<CrossNodisturbPayload> payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }



        public String Authorization()
        {

            Debug.Assert(!string.IsNullOrEmpty(this.appKey));
            Debug.Assert(!string.IsNullOrEmpty(this.masterSecret));

            String origin = this.appKey + ":" + this.masterSecret;
            return Base64.getBase64Encode(origin);
        }
    }
}
