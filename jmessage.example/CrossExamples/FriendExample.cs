using jmessage.cross.friend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace example.CrossExamples
{
    class FriendExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            CrossFriendClient client = new CrossFriendClient(app_key, master_secret);
            String appkey = "6be9204c30b9473e87bad4dc";
            List<string> users = new List<string> { "jmessage123", "jmessage" };
            CrossFriendPayload payload = new CrossFriendPayload(appkey, users);
            client.crossAddFriends("xiaohuihui", payload);
            //client.crossDeleteFriends("xiaohuihui", payload);

            //crossUpdateFriends
            CrossFriendInfoPayload info = new CrossFriendInfoPayload(app_key, "jmessage", "note","other");
            List<CrossFriendInfoPayload> infos = new List<CrossFriendInfoPayload> { };
            infos.Add(info);
            client.crossUpdateFriends("xiaohuihui", infos);

        }
    }
}
