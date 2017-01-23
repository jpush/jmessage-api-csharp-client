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
using jmessage;

namespace example.GroupExamples
{
    class RemoveGroupMembersExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            GroupClient client = new GroupClient(app_key, master_secret);
            List<string> members_username = new List<string> { "jmessage" };
            Dictionary<string, List<string>> payload = new Dictionary<string, List<string>> { };
            payload["remove"] = members_username;
            client.removeGroupMembers(19150325,payload);
            Console.ReadLine();
        }
    }
}
