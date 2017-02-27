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
            TextMessageBody msg_body = new TextMessageBody();
            msg_body.text= "text";
            msg_body.extras = null;
            TextMessagePayload payload = new TextMessagePayload("1", "single", "admin", "text", "xiaohuihui", "admin",msg_body);
            client.sendMessage(payload);
        }
    }
}
