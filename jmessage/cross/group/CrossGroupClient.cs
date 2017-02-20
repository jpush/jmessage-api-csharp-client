using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.cross.group;

namespace jmessage.corss.group
{
    public class CrossGroupClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String CROSS_GROUP_PATH = "/v1/cross/groups/";
        private const String CROSS_USER_PATH = "/v1/cross/users/";

        private String appKey;
        private String masterSecret;
        public CrossGroupClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper crossAddRemoveMembers(string gid, List<CrossMemberPayload> payload)
        {
            Preconditions.checkArgument(payload != null, "Payload should not be empty");
            String payloadJson = this.ToString(payload);
            return crossAddRemoveMembers(gid, payloadJson);
        }

        public ResponseWrapper crossAddRemoveMembers(string gid, string payloadString)
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


     

        public string ToString(List<CrossMemberPayload> payload)
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
