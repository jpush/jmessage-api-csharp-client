using jmessage.corss.group;
using jmessage.cross.group;
using System.Collections.Generic;

namespace example.CrossExamples
{
    class GroupExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            CrossGroupClient client = new CrossGroupClient(app_key, master_secret);

            string appkey = "6be9204c30b9473e87bad4dc";
            List<string> add = new List<string> { "jmessage123" };
            CrossMemberPayload payload = new CrossMemberPayload(appkey, add, null);

            List<CrossMemberPayload> payloads = new List<CrossMemberPayload> { };
            payloads.Add(payload);
            client.crossAddRemoveMembers("19749893", payloads);
            client.crossGetMembers("19749893");
        }
    }
}
