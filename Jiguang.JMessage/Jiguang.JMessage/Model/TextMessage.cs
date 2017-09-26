using Newtonsoft.Json;
using System.Collections;

namespace Jiguang.JMessage.Model
{
    public class TextMessage : Message
    {
        [JsonProperty("msg_type")]
        public string Type { get; } = "text";

        [JsonProperty("msg_body")]
        public MessageBody Content { get; set; }

        public class MessageBody
        {
            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("extras")]
            public IDictionary Extras { get; set; }
        }
    }
}
