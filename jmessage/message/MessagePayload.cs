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
    }

    public class ImageMessagePayload : MessagePayload
    {
        public ImageMessageBody msg_body;
        public ImageMessagePayload(string version, string target_type, string from_type, string msg_type,
            string target_id, string from_id, string from_name, string target_name, ImageMessageBody msg_body)
        {
            this.version = version;
            this.target_type = target_type;
            this.from_type = from_type;
            this.msg_type = msg_type;
            this.target_id = target_id;
            this.from_id = from_id;
            this.from_name = from_name;
            this.target_name = target_name;
            this.msg_body = msg_body;
        }

        public ImageMessagePayload(string version, string target_type, string from_type, string msg_type,
               string target_id, string from_id, ImageMessageBody msg_body)
        {
            this.version = version;
            this.target_type = target_type;
            this.from_type = from_type;
            this.msg_type = msg_type;
            this.target_id = target_id;
            this.from_id = from_id;
            this.msg_body = msg_body;
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
        public MessagePayload Check()
        {
            return this;
        }
    }



    public class ImageMessageBody
    {
        public string media_id;
        public long media_crc32;
        public int width;
        public int height;
        public string format;
        public int fsize;
        public string extras;

        public ImageMessageBody()
        {
            this.media_id = null;
            this.media_crc32 = 0;
            this.width = 0;
            this.height = 0;
            this.format = null;
            this.fsize = 0;
            this.extras = null;
        }

        public ImageMessageBody(string media_id, long media_crc32, int width, int height, string format, int fsize, string extras)
        {
            this.media_id = media_id;
            this.media_crc32 = media_crc32;
            this.width = width;
            this.height = height;
            this.format = format;
            this.fsize = fsize;
            this.extras = extras;
        }
    }


    public class TextMessagePayload : MessagePayload
    {
        public TextMessageBody msg_body;
        public TextMessagePayload(string version, string target_type, string from_type, string msg_type,
            string target_id, string from_id, string from_name, string target_name, TextMessageBody msg_body)
        {
            this.version = version;
            this.target_type = target_type;
            this.from_type = from_type;
            this.msg_type = msg_type;
            this.target_id = target_id;
            this.from_id = from_id;
            this.from_name = from_name;
            this.target_name = target_name;
            this.msg_body = msg_body;
        }

        public TextMessagePayload(string version, string target_type, string from_type, string msg_type,
               string target_id, string from_id, TextMessageBody msg_body)
        {
            this.version = version;
            this.target_type = target_type;
            this.from_type = from_type;
            this.msg_type = msg_type;
            this.target_id = target_id;
            this.from_id = from_id;
            this.msg_body = msg_body;
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


    public class TextMessageBody
    {
        public string text;
        public string extras;
        public TextMessageBody()
        {
            this.text = null;
            this.extras = null;
        }
        public TextMessageBody(string text, string extras)
        {
            this.text = text;
            this.extras = extras;
        }
    }
}
