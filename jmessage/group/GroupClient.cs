using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.group
{
    public class GroupClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String GROUP_PATH = "/v1/groups/";
        private const String USER_PATH = "/v1/users/";
        
        private const String GET_GROUPS = "/v1/groups/?start=";
        private const String GET_USERS_PATH = "/v1/users/?start=";

        private String appKey;
        private String masterSecret;
        public GroupClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper createGroup(GroupPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            String payloadJson = this.ToString(payload);
            return createGroup(payloadJson);
        }
        public ResponseWrapper createGroup(string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += GROUP_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }


        public ResponseWrapper getGroup(int groupId)
        {
            String url = HOST_NAME_SSL;
            url += GROUP_PATH;
            url += groupId.ToString();
            ResponseWrapper result = sendGet(url, Authorization(),null);
            return result;
        }

        public ResponseWrapper updateGroup(int groupId,GroupPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            String payloadJson = payload.ToString(payload);
            return updateGroup(groupId, payloadJson);
        }

        public ResponseWrapper updateGroup(int groupId,string payloadJson)
        {
            String url = HOST_NAME_SSL;
            url += GROUP_PATH;
            url += groupId.ToString();
            ResponseWrapper result = sendPut(url, Authorization(), payloadJson);
            return result;
        }

        public ResponseWrapper deleteGroup(int groupId)
        {
            String url = HOST_NAME_SSL;
            url += GROUP_PATH;
            url += groupId.ToString();
            ResponseWrapper result = sendDelete(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper addGroupMembers(int groupId, Dictionary<string, List<string>>  payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            String payloadJson = this.ToString(payload);
            return addGroupMembers(groupId,payloadJson);
        }

        public ResponseWrapper addGroupMembers(int groupId,string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += GROUP_PATH;
            url += groupId.ToString();
            url += "/members";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper removeGroupMembers(int groupId, Dictionary<string, List<string>> payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            String payloadJson = this.ToString(payload);
            return removeGroupMembers(groupId, payloadJson);
        }

        public ResponseWrapper removeGroupMembers(int groupId, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            Console.WriteLine(payloadString);
            String url = HOST_NAME_SSL;
            url += GROUP_PATH;
            url += groupId.ToString();
            url += "/members";
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper getGroupMembers(int groupId)
        {
            String url = HOST_NAME_SSL;
            url += GROUP_PATH;
            url += groupId.ToString();
            url += "members/";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper getMemberGroups(string username)
        {
            String url = HOST_NAME_SSL;
            url += USER_PATH;
            url += username;
            url += "/groups/";
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public ResponseWrapper getGroupsList(int start,int count)
        {
            String url = HOST_NAME_SSL;
            url += GET_GROUPS;
            url += start.ToString();
            url += "&count=";
            url += count.ToString();
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public string ToString(GroupPayload payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public string ToString(Dictionary<string, List<string>> payload)
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
