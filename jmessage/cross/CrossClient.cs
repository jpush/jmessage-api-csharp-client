using System;
using System.Collections.Generic;
using System.Collections;
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
        private const String CROSS_USER_PATH = "/v1/cross/users/";

        private String appKey;
        private String masterSecret;
        public CrossClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper crossAddMembers(string appkey,string gid, List<string> add)
        {
            Hashtable payload = new Hashtable();
            payload.Add("appkey", appkey);
            payload.Add("add", add);
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(payload);
            Preconditions.checkArgument(payloads != null, "Payload should not be empty");
            String payloadJson = this.ToString(payloads);
            return crossAddMembers(appkey, gid , payloadJson);
        }
        public ResponseWrapper crossAddMembers(string appkey, string gid, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload String should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += CROSS_GROUP_PATH;
            url += gid;
            url += "/members";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossRemoveMembers(string appkey, string gid, List<string> remove)
        {
            Hashtable payload = new Hashtable();
            payload.Add("appkey", appkey);
            payload.Add("remove", remove);
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(payload);
            Preconditions.checkArgument(payloads != null, "Payload should not be empty");
            String payloadJson = this.ToString(payloads);
            return crossRemoveMembers(appkey, gid, payloadJson);
        }

        public ResponseWrapper crossRemoveMembers(string appkey, string gid, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload String should not be empty");
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



        public ResponseWrapper crossAddBlacklist(string appkey, string username, List<string> usernames)
        {
            Hashtable payload = new Hashtable();
            payload.Add("appkey", appkey);
            payload.Add("usernames", usernames);
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(payload);
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            String payloadJson = this.ToString(payloads);
            return crossAddBlacklist(appkey, username, payloadJson);
        }
        public ResponseWrapper crossAddBlacklist(string appkey, string username, string payloadString)
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

        public ResponseWrapper crossRemoveBlacklist(string appkey, string username, List<string> usernames)
        {
            Hashtable payload = new Hashtable();
            payload.Add("appkey", appkey);
            payload.Add("usernames", usernames);
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(payload);
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            String payloadJson = this.ToString(payloads);
            return crossRemoveBlacklist(appkey, username, payloadJson);
        }
        public ResponseWrapper crossRemoveBlacklist(string appkey, string username, string payloadString)
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

        public ResponseWrapper crossAddSingleNodisturb(string appkey, string username, List<string> add)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/nodisturb";
            Hashtable payload = new Hashtable();
            payload.Add("appkey", appkey);
            Hashtable single = new Hashtable();
            single.Add("add", add);
            payload.Add("single", single);
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(payload);
            string payloadString = this.ToString(payloads);
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossRemoveSingleNodisturb(string appkey, string username, List<string> remove)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/nodisturb";
            Hashtable payload = new Hashtable();
            payload.Add("appkey", appkey);
            Hashtable single = new Hashtable();
            single.Add("remove", remove);
            payload.Add("single", single);
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(payload);
            string payloadString = this.ToString(payloads);
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossAddGroupNodisturb(string appkey, string username, List<string> add)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/nodisturb";
            Hashtable payload = new Hashtable();
            payload.Add("appkey", appkey);
            Hashtable group = new Hashtable();
            group.Add("add", add);
            payload.Add("group", group);
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(payload);
            string payloadString = this.ToString(payloads);
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossRemoveGroupNodisturb(string appkey, string username, List<string> remove)
        {
            String url = HOST_NAME_SSL;
            url += CROSS_USER_PATH;
            url += username;
            url += "/nodisturb";
            Hashtable payload = new Hashtable();
            payload.Add("appkey", appkey);
            Hashtable group = new Hashtable();
            group.Add("remove", remove);
            payload.Add("group", group);
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(payload);
            string payloadString = this.ToString(payloads);
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper crossAddFriends(string appkey, string username, List<string> users)
        {
            Preconditions.checkArgument(users != null, "Payload should not be empty");
            Hashtable payload = new Hashtable();
            payload["appkey"] = appkey;
            payload["users"] = users;
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


        public ResponseWrapper crossDeleteFriends(string appkey, string username, List<string> users)
        {
            Preconditions.checkArgument(users != null, "Payload should not be empty");
            Hashtable payload = new Hashtable();
            payload["appkey"] = appkey;
            payload["users"] = users;
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

        public ResponseWrapper crossUpdateFriends(string appkey, string username, Hashtable users)
        {
            Preconditions.checkArgument(users != null, "Payload should not be empty");
            List<Hashtable> payloads = new List<Hashtable> { };
            payloads.Add(users);
            String payloadJson = this.ToString(payloads);
            return crossUpdateFriends(appkey, username, payloadJson);
        }

        public ResponseWrapper crossUpdateFriends(string appkey, string username, string payload)
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

        public string ToString(List<string> payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public string ToString(Hashtable payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public string ToString(List<Hashtable> payloads)
        {
            return JsonConvert.SerializeObject(payloads,
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
