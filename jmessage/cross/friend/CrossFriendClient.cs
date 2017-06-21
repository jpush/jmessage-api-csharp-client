using System;
using System.Collections.Generic;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.cross.friend
{
    public class CrossFriendClient : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const string CROSS_GROUP_PATH = "/v1/cross/groups/";
        private const string CROSS_USER_PATH = "/v1/cross/users/";

        private string appKey;
        private string masterSecret;

        public CrossFriendClient(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");

            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public ResponseWrapper crossAddFriends(string username, CrossFriendPayload payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return crossAddFriends(username, payloadJson);
        }

        public ResponseWrapper crossAddFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload string should not be empty");
            Console.WriteLine(payloadString);
            string url = HOST_NAME_SSL + CROSS_USER_PATH + username + "/friends";
            return sendPost(url, Authorization(), payloadString);
        }

        public ResponseWrapper crossDeleteFriends(string username, CrossFriendPayload payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return crossDeleteFriends(username, payloadJson);
        }

        public ResponseWrapper crossDeleteFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload string should not be empty");

            Console.WriteLine(payloadString);

            string url = HOST_NAME_SSL + CROSS_USER_PATH + username + "/friends";
            return sendDelete(url, Authorization(), payloadString);
        }

        public ResponseWrapper crossUpdateFriends(string username, List<CrossFriendInfoPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return crossUpdateFriends(username, payloadJson);
        }

        public ResponseWrapper crossUpdateFriends(string username, string payload)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payload), "payload string should not be empty");

            Console.WriteLine(payload);

            string url = HOST_NAME_SSL + CROSS_USER_PATH + username + "/friends";
            return sendPut(url, Authorization(), payload);
        }

        public string ToString(CrossFriendPayload payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string ToString(List<CrossFriendInfoPayload> payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string Authorization()
        {
            Debug.Assert(!string.IsNullOrEmpty(appKey));
            Debug.Assert(!string.IsNullOrEmpty(masterSecret));

            return Base64.getBase64Encode(appKey + ":" + masterSecret);
        }
    }
}
