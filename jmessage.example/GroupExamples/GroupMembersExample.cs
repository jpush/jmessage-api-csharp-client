using System;
using System.Collections.Generic;
using jmessage.group;

namespace example.GroupExamples
{
    class GroupMembersExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            List<string> add = new List<string> { "jmessage" };
            List<string> remove = new List<string> { "jmessage123" };
            MemberPayload payload = new MemberPayload(add, remove);

            GroupClient client = new GroupClient(app_key, master_secret);
            client.updateGroupMembers(19749893, payload);

            Console.ReadLine();
        }
    }
}
