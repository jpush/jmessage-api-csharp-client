using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.message
{
    public class FileClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String MESSAGE_PATH = "/v1/resource";

        private String appKey;
        private String masterSecret;
        public FileClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper getMedia(string mediaId)
        {
            Preconditions.checkArgument(mediaId != null, "mediaId should not be empty");
            String url = HOST_NAME_SSL;
            url += MESSAGE_PATH;
            url += "?mediaId=";
            url += mediaId;
            ResponseWrapper result = sendGet(url, Authorization(), null);
            return result;
        }

        public string ToString(MessagePayload payload)
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
