using Newtonsoft.Json;

namespace Jiguang.JMessage.Model
{
    class VoiceMessage : Message
    {
        [JsonProperty("msg_type")]
        public string Type { get; } = "voice";

        [JsonProperty("msg_body")]
        public MessageBody Content { get; set; }

        public class MessageBody
        {
            /// <summary>
            /// 文件上传之后服务器端所返回的 key，用于之后生成下载的 URL（必填）。
            /// </summary>
            [JsonProperty("media_id")]
            public string MediaId { get; set; }

            /// <summary>
            /// 文件的 crc32 校验码，用于下载大图的校验（必填）。
            /// </summary>
            [JsonProperty("media_crc32")]
            public long MediaCrc32 { get; set; }

            /// <summary>
            /// 音频时长（必填）。
            /// </summary>
            [JsonProperty("duration")]
            public int Duration { get; set; }

            /// <summary>
            /// 文件大小（字节数）（必填）。
            /// </summary>
            [JsonProperty("fsize")]
            public int Size { get; set; }

            /// <summary>
            /// 图片 hash 值。
            /// </summary>
            [JsonProperty("hash")]
            public string Hash { get; set; }
        }

    }
}
