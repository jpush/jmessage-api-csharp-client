using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using jmessage.common;
using jmessage.util;

namespace jmessage.message
{
    public class MessagePayload
    {
        public string version;
        public string target_type;
        public string from_type;
        public string msg_type;
        public string target_id;
        public string from_id;
        public string from_name;
        public string target_name;

        public MessagePayload()
        {
            this.version = null;
            this.target_type = null;
            this.from_type = null;
            this.msg_type = null;
            this.target_id = null;
            this.from_id = null;
            this.from_name = null;
            this.target_name = null;
        }


        public string ToString(MessagePayload message)
        {
            return JsonConvert.SerializeObject(message,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }
        public MessagePayload Check()
        {
            return this;
        }
    }


    public class ImageMessagePayload : MessagePayload
    {
        public string media_id;
        public long media_crc32;
        public int width;
        public int height;
        public string format;
        public int fsize;
        public string extras;

        public ImageMessagePayload()
        {
            this.media_id = null;
            this.media_crc32 = 0;
            this.width = 0;
            this.height = 0;
            this.format = null;
            this.fsize = 0;
            this.extras = null;

        }


        public string ToString(ImageMessagePayload message)
        {
            return JsonConvert.SerializeObject(message,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }
        public ImageMessagePayload Check()
        {
            return this;
        }
    }


    public class TextMessagePayload : MessagePayload
    {
        public string text;
        public string extras;

        public TextMessagePayload()
        {
            this.text = null;
            this.extras = null;

        }


        public string ToString(TextMessagePayload message)
        {
            return JsonConvert.SerializeObject(message,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
        }
        public TextMessagePayload Check()
        {
            return this;
        }
    }
}
