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
        private const String GET_ADMIN_PATH = "/v1/admins?start=";
        private const String GET_USERS_PATH = "/v1/users/?start=";
        private const String USER_PATH = "/v1/users/";
        private const String PUSH_PATH = "/v3/push";

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

        public ResponseWrapper putGroup(int groupId,GroupPayload payload)
        {
            Preconditions.checkArgument(payload != null, "pushPayload should not be empty");
            String payloadJson = payload.ToString(payload);
            return putGroup(groupId, payloadJson);
        }

        public ResponseWrapper putGroup(int groupId,string payloadJson)
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


        public string ToString(GroupPayload payload)
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
