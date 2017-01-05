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
        private string media_id;
        private long media_crc32;
        private int width;
        private int height;
        private string format;
        private int fsize;
        private string extras;

        public string Media_id
        {
            get
            {
                return media_id;
            }

            set
            {
                media_id = value;
            }
        }

        public long Media_crc32
        {
            get
            {
                return media_crc32;
            }

            set
            {
                media_crc32 = value;
            }
        }

        public int Width
        {
            get
            {
                return width;
            }

            set
            {
                width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public string Format
        {
            get
            {
                return format;
            }

            set
            {
                format = value;
            }
        }

        public int Fsize
        {
            get
            {
                return fsize;
            }

            set
            {
                fsize = value;
            }
        }

        public string Extras
        {
            get
            {
                return extras;
            }

            set
            {
                extras = value;
            }
        }

        public ImageMessagePayload(string version, string target_type, string from_type, string msg_type,
            string target_id, string from_id, string from_name, string target_name)
        {
            this.version = version;
            this.target_type = target_type;
            this.from_type = from_type;
            this.msg_type = msg_type;
            this.target_id = target_id;
            this.from_id = from_id;
            this.from_name = from_name;
            this.target_name = target_name;
        }

        public ImageMessagePayload()
        {
            this.Media_id = null;
            this.Media_crc32 = 0;
            this.Width = 0;
            this.Height = 0;
            this.Format = null;
            this.Fsize = 0;
            this.Extras = null;
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

        public TextMessagePayload(string version, string target_type, string from_type, string msg_type,
            string target_id, string from_id, string from_name, string target_name)
        {
            this.version = version;
            this.target_type = target_type;
            this.from_type = from_type;
            this.msg_type = msg_type;
            this.target_id = target_id;
            this.from_id = from_id;
            this.from_name = from_name;
            this.target_name = target_name;
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
