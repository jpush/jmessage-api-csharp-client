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
    class SendTextMessageExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            MessageClient client = new MessageClient(app_key, master_secret);
            //ImageMessagePayload(string version, string target_type, string from_type, string msg_type,string target_id, string from_id)

            TextMsg_body msg_body = new TextMsg_body();
            msg_body.text= "qiniu/image/CE0ACD035CBF71F8";
            msg_body.extras = null;
            TextMessagePayload payload = new TextMessagePayload("1", "single", "admin", "text", "xiaohuihui", "admin",msg_body);
            string payloadstring = payload.ToString(payload);
            client.sendMessage(payload);


            Console.WriteLine(payloadstring);
            Console.ReadLine();
        }
    }
}
