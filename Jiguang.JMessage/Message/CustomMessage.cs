using Newtonsoft.Json;
using System.Collections;

namespace Jiguang.JMessage.Message
{
    class CustomMessage
    {
        [JsonProperty("msg_type")]
        public string Type { get; } = "custom";

        [JsonProperty("msg_body")]
        public MessageBody Content { get; set; }

        public class MessageBody
        {
            public IDictionary Extras { get; set; }
        }
    }
}
