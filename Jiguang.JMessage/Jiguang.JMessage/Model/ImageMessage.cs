using Newtonsoft.Json;

namespace Jiguang.JMessage.Model
{
    class ImageMessage : Message
    {
        [JsonProperty("msg_type", Required = Required.Always)]
        public string Type { get; } = "image";

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
            /// 图片原始宽度（必填）。
            /// </summary>
            [JsonProperty("width")]
            public int Width { get; set; }

            /// <summary>
            /// 图片原始高度（必填）。
            /// </summary>
            [JsonProperty("height")]
            public int Height { get; set; }

            /// <summary>
            /// 图片格式（必填）。
            /// </summary>
            [JsonProperty("format")]
            public string Format { get; set; }

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
