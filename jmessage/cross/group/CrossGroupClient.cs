using System;
using System.Collections.Generic;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.cross.group;

namespace jmessage.corss.group
{
    public class CrossGroupClient : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const string CROSS_GROUP_PATH = "/v1/cross/groups/";
        private const string CROSS_USER_PATH = "/v1/cross/users/";

        private string appKey;
        private string masterSecret;

        public CrossGroupClient(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!string.IsNullOrEmpty(masterSecret), "masterSecret should be set");

            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper crossAddRemoveMembers(string gid, List<CrossMemberPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");

            string payloadJson = ToString(payload);
            return crossAddRemoveMembers(gid, payloadJson);
        }

        public ResponseWrapper crossAddRemoveMembers(string gid, string payloadString)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload string should not be empty");

            Console.WriteLine(payloadString);

            string url = HOST_NAME_SSL + CROSS_GROUP_PATH + gid + "/members";
            return sendPost(url, Authorization(), payloadString);
        }

        public ResponseWrapper crossGetMembers(string gid)
        {
            string url = HOST_NAME_SSL + CROSS_GROUP_PATH + gid + "/members/";
            return sendGet(url, Authorization(), null);
        }

        public string ToString(List<CrossMemberPayload> payload)
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
