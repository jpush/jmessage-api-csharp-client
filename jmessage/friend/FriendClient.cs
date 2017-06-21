using System;
using System.Collections.Generic;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.friend
{
    public class FriendClient : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const string USER_PATH = "/v1/users/";

        private string appKey;
        private string masterSecret;

        public FriendClient(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!string.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public ResponseWrapper addFriends(string username, List<string> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return addFriends(username, payloadJson);
        }

        public ResponseWrapper addFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadstring should not be empty");

            Console.WriteLine(payloadString);

            string url = HOST_NAME_SSL + USER_PATH + username + "/friends";
            return sendPost(url, Authorization(), payloadString);
        }

        public ResponseWrapper deleteFriends(string username, List<string> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return deleteFriends(username, payloadJson);
        }

        public ResponseWrapper deleteFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadstring should not be empty");
            Console.WriteLine(payloadString);
            string url = HOST_NAME_SSL + USER_PATH + username + "/friends";
            return sendDelete(url, Authorization(), payloadString);
        }

        public ResponseWrapper updateFriends(string username, List<FriendInfoPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return updateFriends(username, payloadJson);
        }

        public ResponseWrapper updateFriends(string username, string payload)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payload), "payloadstring should not be empty");
            Console.WriteLine(payload);
            string url = HOST_NAME_SSL + USER_PATH + username + "/friends";
            return sendPut(url, Authorization(), payload);
        }

        public ResponseWrapper getFriends(string username)
        {
            string url = HOST_NAME_SSL + USER_PATH + username + "/friends";
            return sendGet(url, Authorization(), null);
        }

        public string ToString(List<string> payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string ToString(List<FriendInfoPayload> payload)
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
