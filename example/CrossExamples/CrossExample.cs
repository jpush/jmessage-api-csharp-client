using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.message;
using jmessage.group;
using jmessage.cross;
using jmessage;
namespace example.CrossExamples
{
    class CrossExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            CrossClient client = new CrossClient(app_key, master_secret);
            CrossMembersPayload payload = new CrossMembersPayload();
            payload.appkey= "6be9204c30b9473e87bad4dc";
            List<string> add = new List<string> { "jmessage" };
            payload.add = add;
            List<string> remove = new List<string> { "xiaohuihui" };
            payload.remove = remove;
            List<CrossMembersPayload> payloads = new List<CrossMembersPayload> { };
            payloads.Add(payload);
            string payloadstring = client.ToString(payloads);
            Console.WriteLine(payloadstring);
            client.crossAddMembers("6be9204c30b9473e87bad4dc", "19749893", payloadstring);
        }
    }
}
