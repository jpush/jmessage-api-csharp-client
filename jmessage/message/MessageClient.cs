using System;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;

namespace jmessage.message
{
    public class MessageClient : BaseHttpClient
    {
        private const string HOST_NAME_SSL = "https://api.im.jpush.cn";
        private const string MESSAGE_PATH = "/v1/messages/";

        private string appKey;
        private string masterSecret;

        public MessageClient(string appKey, string masterSecret)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(appKey), "appKey should be set");
            Preconditions.checkArgument(!string.IsNullOrEmpty(masterSecret), "masterSecret should be set");
            this.appKey = appKey;
            this.masterSecret = masterSecret;
        }

        public ResponseWrapper sendMessage(MessagePayload payload)
        {
            Preconditions.checkArgument(payload != null, "message Payload should not be empty");
            string payloadString = ToString(payload);
            Preconditions.checkArgument(!string.IsNullOrEmpty(payloadString), "payloadString should not be empty");
            string url = HOST_NAME_SSL + MESSAGE_PATH;
            ResponseWrapper result = sendPost(url, Authorization(), payloadString);
            return result;
        }

        /// <summary>
        /// 消息撤回
        /// </summary>
        /// <param name="msgId">消息 ID</param>
        /// <param name="username">发送该消息的用户名</param>
        /// <returns></returns>
        public ResponseWrapper retractMessage(string msgId, string username)
        {
            Preconditions.checkArgument(!string.IsNullOrEmpty(msgId), "msgId shouldn't be empty.");
            Preconditions.checkArgument(!string.IsNullOrEmpty(username), "username shouldn't be empty");

            string url = HOST_NAME_SSL + MESSAGE_PATH + "/" + username + "/" + msgId + "/retract";
            return sendGet(url, Authorization(), null);
        }

        public string ToString(MessagePayload payload)
        {
            return JsonConvert.SerializeObject(payload, Formatting.None, new JsonSerializerSettings
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
