using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.message;
using jmessage;

namespace example.MessageExamples
{
    class SendImageMessageExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            MessageClient client = new MessageClient(app_key, master_secret);
            //ImageMessagePayload(string version, string target_type, string from_type, string msg_type,string target_id, string from_id)

            ImageMessageBody msg_body = new ImageMessageBody();
            msg_body.media_id= "qiniu/image/CE0ACD035CBF71F8";
            msg_body.media_crc32 = 2778919613;
            msg_body.width = 3840;
            msg_body.height = 2160;
            msg_body.fsize = 3328738;
            msg_body.format = "jpg";
            msg_body.extras =null;
            ImageMessagePayload payload = new ImageMessagePayload("1", "single", "admin", "image", "xiaohuihui", "admin",msg_body);
            string payloadstring = payload.ToString(payload);
            client.sendMessage(payload);


            Console.WriteLine(payloadstring);
            Console.ReadLine();
        }
    }
}
