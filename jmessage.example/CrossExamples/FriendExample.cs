using jmessage.cross.friend;
using System.Collections.Generic;

namespace example.CrossExamples
{
    class FriendExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            CrossFriendClient client = new CrossFriendClient(app_key, master_secret);
            List<string> users = new List<string> { "jmessage123", "jmessage" };
            string appkey = "6be9204c30b9473e87bad4dc";
            CrossFriendPayload payload = new CrossFriendPayload(appkey, users);
            client.crossAddFriends("xiaohuihui", payload);

            List<CrossFriendInfoPayload> infos = new List<CrossFriendInfoPayload> { };
            infos.Add(new CrossFriendInfoPayload(app_key, "jmessage", "note", "other"));
            client.crossUpdateFriends("xiaohuihui", infos);
        }
    }
}
