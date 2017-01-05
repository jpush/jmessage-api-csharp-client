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
    public class MessageClient : BaseHttpClient
    {
        private const String HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const String MESSAGE_PATH = "/v1/messages/";

        private String appKey;
        private String masterSecret;
        public MessageClient(String appKey, String masterSecret)
        {
            Preconditions.checkArgument(!String.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!String.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }
        public ResponseWrapper sendImageMessage(ImageMessagePayload payload)
        {
            Preconditions.checkArgument(payload != null, "message Payload should not be empty");
            String payloadString = this.ToString(payload);
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            String url = HOST_NAME_SSL;
            url += MESSAGE_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        public ResponseWrapper sendTextMessage(TextMessagePayload payload)
        {
            Preconditions.checkArgument(payload != null, "message Payload should not be empty");
            String payloadString = this.ToString(payload);
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payload String should not be empty");
            String url = HOST_NAME_SSL;
            url += MESSAGE_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        
        public string ToString(ImageMessagePayload payload)
        {
            return JsonConvert.SerializeObject(payload,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }

        public string ToString(TextMessagePayload payload)
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
