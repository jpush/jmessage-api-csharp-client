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
        private const String GET_ADMIN_PATH = "/v1/admins?start=";
        private const String GET_USERS_PATH = "/v1/users/?start=";
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
        public ResponseWrapper registUser(List<UserPayload> payload)
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

        public ResponseWrapper getAdmin(int start, int count)
        {
            String url = HOST_NAME_SSL;
            url += GET_ADMIN_PATH;
            url += start.ToString();
            url += "&count=";
            url += count.ToString();
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper getUser(string username)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper getUsers(int start, int count)
        {
            String url = HOST_NAME_SSL;
            url += GET_USERS_PATH;
            url += start.ToString();
            url += "&count=";
            url += count.ToString();
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper updateUser(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            string username = payload.username;
            payload.username = null;
            String payloadJson = payload.ToString(payload);
            return updateUser(payloadJson, username);
        }

        public ResponseWrapper updateUser(string payloadString, string username)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            ResponseWrapper result = sendPut(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper updateUserPassword(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            String username = payload.username;
            payload.username = null;
            String payloadJson = payload.ToString(payload);
            return updateUserPassword(payloadJson, username);
        }

        public ResponseWrapper updateUserPassword(string payloadString, string username)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL + USER_PATH + username + "/password";
            ResponseWrapper result = sendPut(url, Authorization(), payloadString);
            return result;
        }


        public ResponseWrapper deleteUser(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            String username = payload.username;
            return deleteUser(username);
        }

        public ResponseWrapper deleteUser(string username)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(username), "payloadString should not be empty");
            String url = HOST_NAME_SSL + USER_PATH + username;
            ResponseWrapper result = sendDelete(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper getUserStat(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            string username = payload.username;
            return getUserStat(username);
        }

        public ResponseWrapper getUserStat(string username)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(username), "payloadString should not be empty");
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/userstat";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper putUserBlacklist(string username, List<string> users)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(username), "payloadString should not be empty");
            String url = HOST_NAME_SSL + USER_PATH + username + "/blacklist";
            string userslist = ToString(users);
            ResponseWrapper result = sendPut(url, Authorization(), userslist);
            return result;
        }

        public ResponseWrapper getUserBlacklist(string username)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(username), "payloadString should not be empty");
            String url = HOST_NAME_SSL + USER_PATH + username + "/blacklist";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper deleteUserBlacklist(string username, List<string> users)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(username), "payloadString should not be empty");
            String url = HOST_NAME_SSL + USER_PATH + username + "/blacklist";
            string userslist = ToString(users);

            ResponseWrapper result = sendDelete(url, Authorization(), userslist);
            return result;
        }


        public ResponseWrapper addSingleNodisturb(string username, List<String> users)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/nodisturb";
            Dictionary<String, Dictionary<String, List<String>>> nodisturb = new Dictionary<String, Dictionary<String, List<String>>> { };
            Dictionary<String, List<String>> single = new Dictionary<string, List<string>> { };
            List<String> add = users;
            single["add"] = add;
            nodisturb["single"] = single;
            String payload = ToString(nodisturb);
            ResponseWrapper result = sendPost(url, Authorization(), payload);
            return result;
        }

        public ResponseWrapper removeSingleNodisturb(string username, List<String> users)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/nodisturb";
            Dictionary<String, Dictionary<String, List<String>>> nodisturb = new Dictionary<String, Dictionary<String, List<String>>> { };
            Dictionary<String, List<String>> single = new Dictionary<string, List<string>> { };
            List<String> remove = users;
            single["remove"] = remove;
            nodisturb["single"] = single;
            String payload = ToString(nodisturb);
            ResponseWrapper result = sendPost(url, Authorization(), payload);
            return result;
        }

        public ResponseWrapper addGroupNodisturb(string username, List<String> users)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/nodisturb";
            Dictionary<String, Dictionary<String, List<String>>> nodisturb = new Dictionary<String, Dictionary<String, List<String>>> { };
            Dictionary<String, List<String>> group = new Dictionary<string, List<string>> { };
            List<String> add = users;
            group["add"] = users;
            nodisturb["group"] = group;
            String payload = ToString(nodisturb);
            ResponseWrapper result = sendPost(url, Authorization(), payload);
            return result;
        }


        public ResponseWrapper removeGroupNodisturb(string username, List<String> users)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/nodisturb";
            Dictionary<String, Dictionary<String, List<String>>> nodisturb = new Dictionary<String, Dictionary<String, List<String>>> { };
            Dictionary<String, List<String>> group = new Dictionary<string, List<string>> { };
            List<String> add = users;
            group["remove"] = users;
            nodisturb["group"] = group;
            String payload = ToString(nodisturb);
            ResponseWrapper result = sendPost(url, Authorization(), payload);
            return result;
        }



        public ResponseWrapper setGlobalNodisturbOpen(string username)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/nodisturb";
            Dictionary<String, Int16> nodisturb = new Dictionary<string, short> { };
            nodisturb["global"] = 1;
            String payload = ToString(nodisturb);
            ResponseWrapper result = sendPost(url, Authorization(), payload);
            return result;
        }

        public ResponseWrapper setGlobalNodisturbClose(string username)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/nodisturb";
            Dictionary<String, Int16> nodisturb = new Dictionary<string, short> { };
            nodisturb["global"] = 0;
            String payload = ToString(nodisturb);
            ResponseWrapper result = sendPost(url, Authorization(), payload);
            return result;
        }


        public string ToString(Dictionary<String, Int16> payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
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

        public string ToString(List<UserPayload> payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public string ToString(Dictionary<String, Dictionary<String, List<String>>> payload)
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
