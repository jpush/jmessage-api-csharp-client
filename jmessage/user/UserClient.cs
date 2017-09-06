using System;
using System.Collections.Generic;
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

        public ResponseWrapper registerUser(List<UserPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            //payload.Check();
            String payloadJson = this.ToString(payload);
            return registerUser(payloadJson);
        }

        public ResponseWrapper registerUser(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper registerAdmin(UserPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            payload.Check();
            String payloadJson = payload.ToString(payload);
            return registerAdmin(payloadJson);
        }

        public ResponseWrapper registerAdmin(string payloadString)
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

        public ResponseWrapper updateUserBlacklist(string username, List<string> users)
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

        public ResponseWrapper setNodisturb(string username, NodisturbPayload payload)
        {
            String url = HOST_NAME_SSL + USER_PATH + username + "/nodisturb";
            string payloadString = ToString(payload);
            return sendPost(url, Authorization(), payloadString);
        }

        /// <summary>
        /// 禁用用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="isForbid">是否禁用用户。true: 禁用；false: 激活</param>
        public ResponseWrapper forbidUser(string username, bool isForbid)
        {
            string url = HOST_NAME_SSL + USER_PATH + username + "/forbidden?disable=" + isForbid;
            return sendPut(url, Authorization(), null);
        }

        public string ToString(List<string> payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string ToString(List<UserPayload> payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string ToString(NodisturbPayload payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public String Authorization()
        {
            Debug.Assert(!string.IsNullOrEmpty(appKey));
            Debug.Assert(!string.IsNullOrEmpty(masterSecret));

            String origin = appKey + ":" + masterSecret;
            return Base64.getBase64Encode(origin);
        }
    }
}
