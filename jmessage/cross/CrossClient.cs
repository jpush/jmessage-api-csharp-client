using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.cross
{
    public class CrossClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String CROSS_GROUP_PATH = "/v1/cross/groups/";
        private const String CROSS_USER_PATH = "/v1/users/";

        private String appKey;
        private String masterSecret;
        public CrossClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper crossAddMembers(string appkey,string gid, List<Dictionary<string, string>> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return crossAddMembers(appkey, gid , payloadJson);
        }
        public ResponseWrapper crossAddMembers(string appkey, string gid, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_GROUP_PATH;
            url += gid;
            url += "/members";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossRemoveMembers(string appkey, string gid, List<Dictionary<string, string>> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return crossRemoveMembers(appkey, gid, payloadJson);
        }
        public ResponseWrapper crossRemoveMembers(string appkey, string gid, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_GROUP_PATH;
            url += gid;
            url += "/members";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }


        public ResponseWrapper crossGetMembers(string gid)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_GROUP_PATH;
            url += gid;
            url += "/members/";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }



        public ResponseWrapper crossAddBlacklist(string appkey, string username, List<Dictionary<string, string>> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return crossAddBlacklist(appkey, username, payloadJson);
        }
        public ResponseWrapper crossAddBlacklist(string appkey, string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_GROUP_PATH;
            url += username;
            url += "/blacklist";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossRemoveBlacklist(string appkey, string username, List<Dictionary<string, string>> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return crossRemoveBlacklist(appkey, username, payloadJson);
        }
        public ResponseWrapper crossRemoveBlacklist(string appkey, string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_GROUP_PATH;
            url += username;
            url += "/members";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
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

        public ResponseWrapper crossSetSingleNodisturb(string appkey, string username, LinkedList<string> add, LinkedList<string> remove)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/nodisturb";
            SingleNodisturbPayload single = new SingleNodisturbPayload(appkey, add, remove);
            NodisturbPayload nodisturb = new NodisturbPayload();
            nodisturb.single = single;
            string payloadString = this.ToString(nodisturb);
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossSetGroupNodisturb(string appkey, string username,  LinkedList<int> add, LinkedList<int> remove)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/nodisturb";
            GroupNodisturbPayload group = new GroupNodisturbPayload(appkey, add, remove);
            NodisturbPayload nodisturb = new NodisturbPayload();
            nodisturb.group = group;
            string payloadString = this.ToString(nodisturb);
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossAddFriends(string appkey, string username, List<string> users)
        {
            Preconditions.checkArgument(users != null, "pushPayload should not be empty");
            string usersString = ToString(users);
            Dictionary<string, string> payload = new Dictionary<string, string> { };
            payload["appkey"] = appkey;
            payload["users"] = usersString;
            String payloadJson = this.ToString(payload);
            return crossAddFriends(username, payloadJson);
        }
        public ResponseWrapper crossAddFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }


        public ResponseWrapper deleteFriends(string appkey, string username, List<string> users)
        {
            Preconditions.checkArgument(users != null, "pushPayload should not be empty");
            string usersString = ToString(users);
            Dictionary<string, string> payload = new Dictionary<string, string> { };
            payload["appkey"] = appkey;
            payload["users"] = usersString;
            String payloadJson = this.ToString(payload);
            return deleteFriends(username, payloadJson);
        }
        public ResponseWrapper deleteFriends(string username, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendDelete(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper putFriends(string appkey, string username, List<Dictionary<string, string>> users)
        {
            Preconditions.checkArgument(users != null, "pushPayload should not be empty");
            foreach (Dictionary<string, string> user in users)
            {
                user["appkey"] = appkey;
            }
            String payloadJson = this.ToString(users);

            return putFriends(username, payloadJson);
        }

        public ResponseWrapper putFriends(string username, string payload)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payload), "payloadString should not be empty");
            Console.WriteLine(payload);
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/friends";
            ResponseWrapper result = sendPut(url, Authorization(), payload);
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

        public string ToString(NodisturbPayload payload)
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

        public string ToString(Dictionary<string, string> payload)
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
