using System;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.group
{
    public class GroupClient : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const string GROUP_PATH = "/v1/groups/";
        private const string USER_PATH = "/v1/users/";
        private const string GET_GROUPS = "/v1/groups/?start=";
        private const string GET_USERS_PATH = "/v1/users/?start=";

        private string appKey;
        private string masterSecret;

        public GroupClient(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public ResponseWrapper createGroup(GroupPayload payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return createGroup(payloadJson);
        }

        public ResponseWrapper createGroup(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadstring should not be empty");
            Console.WriteLine(payloadString);
            string url = HOST_NAME_SSL;
            url += GROUP_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper getGroup(int groupId)
        {
            string url = HOST_NAME_SSL;
            url += GROUP_PATH;
            url += groupId.ToString();
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper updateGroup(int groupId, GroupPayload payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = payload.ToString(payload);
            return updateGroup(groupId, payloadJson);
        }

        public ResponseWrapper updateGroup(int groupId, string payloadJson)
        {
            string url = HOST_NAME_SSL + GROUP_PATH + groupId.ToString();
            ResponseWrapper result = sendPut(url, Authorization(), payloadJson);
            return result;
        }

        public ResponseWrapper deleteGroup(int groupId)
        {
            string url = HOST_NAME_SSL + GROUP_PATH + groupId.ToString();
            ResponseWrapper result = sendDelete(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper updateGroupMembers(int groupId, MemberPayload payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            string payloadJson = ToString(payload);
            return updateGroupMembers(groupId, payloadJson);
        }

        public ResponseWrapper updateGroupMembers(int groupId, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadstring should not be empty");
            Console.WriteLine(payloadString);
            string url = HOST_NAME_SSL + GROUP_PATH + groupId.ToString() + "/members";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper getGroupMembers(int groupId)
        {
            string url = HOST_NAME_SSL + GROUP_PATH + groupId.ToString() + "/members/";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper getMemberGroups(string username)
        {
            string url = HOST_NAME_SSL + USER_PATH + username + "/groups/";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper getGroupsList(int start, int count)
        {
            string url = HOST_NAME_SSL + GET_GROUPS + start.ToString() + "&count=" + count.ToString();
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public string ToString(GroupPayload payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public string ToString(MemberPayload payload)
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

            string origin = appKey + ":" + masterSecret;
            return Base64.getBase64Encode(origin);
        }
    }
}
