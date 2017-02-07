using System;
using System.Collections.Generic;
using System.Collections;
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
    class crossGetMembersExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            CrossClient client = new CrossClient(app_key, master_secret);

            List<string> add = new List<string> { "jmessage123" };

            client.crossGetMembers("19749893");
        }
    }
}
