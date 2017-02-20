using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.cross.friend
{
    public class CrossFriendClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String CROSS_GROUP_PATH = "/v1/cross/groups/";
        private const String CROSS_USER_PATH = "/v1/cross/users/";

        private String appKey;
        private String masterSecret;
        public CrossFriendClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper crossAddFriends(string username, CrossFriendPayload payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            String payloadJson = this.ToString(payload);
            return crossAddFriends(username, payloadJson);
        }
        public ResponseWrapper crossAddFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload String should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }


        public ResponseWrapper crossDeleteFriends(string username, CrossFriendPayload payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            String payloadJson = this.ToString(payload);
            return crossDeleteFriends(username, payloadJson);
        }

        public ResponseWrapper crossDeleteFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload String should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendDelete(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossUpdateFriends(string username, List<CrossFriendInfoPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            String payloadJson = this.ToString(payload);
            return crossUpdateFriends(username, payloadJson);
        }
        public ResponseWrapper crossUpdateFriends(string username, string payload)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payload), "payload String should not be empty");
            Console.WriteLine(payload);
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendPut(url, Authorization(), payload);
            return result;
        }


        public string ToString(CrossFriendPayload payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public string ToString(List<CrossFriendInfoPayload> payload)
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
