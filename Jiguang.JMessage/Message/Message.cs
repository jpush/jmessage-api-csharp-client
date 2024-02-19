using Newtonsoft.Json;

namespace Jiguang.JMessage.Message
{
    /// <summary>
    /// 消息基类。
    /// <para>未声明必填的属性皆为选填。</para>
    /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_17"/></para>
    /// </summary>
    public class Message
    {
        [JsonIgnore]
        public long Id { get; set; }

        [JsonProperty("version")]
        public int Version { get; } = 1;

        /// <summary>
        /// 发送目标类型：single - 个人；group - 群组。
        /// <para>必填</para>
        /// </summary>
        [JsonProperty("target_type", Required = Required.Always)]
        public string TargetType { get; set; }

        /// <summary>
        /// 目标 Id。当 TargetType 为 single 时填 username， 为 group 时填 Group Id。
        /// <para>必填</para>
        /// </summary>
        [JsonProperty("target_id", Required = Required.Always)]
        public string TargetId { get; set; }

        /// <summary>
        /// 跨应用目标所属应用的 AppKey。
        /// </summary>
        [JsonProperty("target_appkey")]
        public string TargetAppKey { get; set; }

        /// <summary>
        /// 消息接收方将展示的名称。
        /// </summary>
        [JsonProperty("target_name")]
        public string TargetName { get; set; }

        /// <summary>
        /// 发送消息者的身份，可为“admin”，“user” 
        ///<para>必填</para>
        /// </summary>
        [JsonProperty("from_type", Required = Required.Always)]
        public string FromType { get;set; } = "admin";

        /// <summary>
        /// 发送方的 username。
        /// <para>必填</para>
        /// </summary>
        [JsonProperty("from_id")]
        public string FromId { get; set; }

        /// <summary>
        /// 发送方将展示的名称。
        /// </summary>
        [JsonProperty("from_name")]
        public string FromName { get; set; }

        /// <summary>
        /// 消息是否需要离线存储。true: 不需要；false: 需要。
        /// </summary>
        [JsonProperty("no_offline")]
        public bool IsNoOffline { get; set; }

        /// <summary>
        /// 消息是否在通知栏展示。true: 不会展示；false: 会展示。
        /// </summary>
        [JsonProperty("no_notification")]
        public bool IsNoNotification { get; set; }

        public override string ToString()
        {
            return GetJson();
        }

        private string GetJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
