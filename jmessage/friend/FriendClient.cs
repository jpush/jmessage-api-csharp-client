using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.friend
{
    public class FriendClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String USER_PATH = "/v1/users/";

        private String appKey;
        private String masterSecret;
        public FriendClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper addFriends(string username,List <string> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return addFriends(username,payloadJson);
        }
        public ResponseWrapper addFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }


        public ResponseWrapper deleteFriends(string username, List<string> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return deleteFriends(username, payloadJson);
        }
        public ResponseWrapper deleteFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendDelete (url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper updateFriends(string username, List<Dictionary<string, string>> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            String payloadJson = this.ToString(payload);
            return updateFriends(username, payloadJson);
        }

        public ResponseWrapper updateFriends(string username, string payload)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payload), "payloadString should not be empty");
            Console.WriteLine(payload);
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendPut(url, Authorization(), payload);
            return result;
        }

        public ResponseWrapper getFriends(string username)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendGet(url, Authorization(),null);
            return result;
        }

        public string ToString(List<string> payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }
        public string ToString(List<Dictionary<string,string>> payload)
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
