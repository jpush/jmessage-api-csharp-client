using System;
using System.Collections.Generic;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.cross.user;

namespace jmessage.user
{
    public class CrossUserClient : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const string CROSS_GROUP_PATH = "/v1/cross/groups/";
        private const string CROSS_USER_PATH = "/v1/cross/users/";

        private string appKey;
        private string masterSecret;

        public CrossUserClient(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!string.IsNullOrEmpty(masterSecret), "masterSecret should be set");

            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public ResponseWrapper crossAddBlacklist(string username, List<CrossBlacklistPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");

            string payloadJson = ToString(payload);
            return crossAddBlacklist(username, payloadJson);
        }

        public ResponseWrapper crossAddBlacklist(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload string should not be empty");

            Console.WriteLine(payloadString);

            string url = HOST_NAME_SSL + CROSS_USER_PATH + username + "/blacklist";
            ResponseWrapper result = sendPut(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossRemoveBlacklist(string username, List<CrossBlacklistPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return crossRemoveBlacklist(username, payloadJson);
        }

        public ResponseWrapper crossRemoveBlacklist(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload string should not be empty");

            Console.WriteLine(payloadString);

            string url = HOST_NAME_SSL + CROSS_USER_PATH + username + "/blacklist";
            return sendDelete(url, Authorization(), payloadString);
        }

        public ResponseWrapper crossGetBlacklist(string username)
        {
            string url = HOST_NAME_SSL + CROSS_USER_PATH + username + "/blacklist";
            return sendGet(url, Authorization(), null);
        }

        public ResponseWrapper crossSetNodisturb(string username, List<CrossNodisturbPayload> payload)
        {
            string url = HOST_NAME_SSL + CROSS_USER_PATH + username + "/nodisturb";
            string payloadString = ToString(payload);
            return sendPost(url, Authorization(), payloadString);
        }

        public string ToString(List<CrossBlacklistPayload> payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string ToString(List<CrossNodisturbPayload> payload)
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
