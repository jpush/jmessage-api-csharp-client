using System;
using System.Collections.Generic;
using jmessage.group;

namespace example.GroupExamples
{
    class CreateGroupExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            List<string> members_username = new List<string> { "xiaohuihui", "jintian" };
            GroupPayload payload = new GroupPayload("toms", "xiaohuihui", members_username, "jmessage");

            GroupClient client = new GroupClient(app_key, master_secret);
            client.createGroup(payload);

            Console.ReadLine();
        }
    }
}
