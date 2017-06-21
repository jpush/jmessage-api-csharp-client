using System;
using System.Collections.Generic;
using jmessage.friend;

namespace example.FriendExamples
{
    class UpdateFriendsExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            string username = "jintian";
            string note_name = "new note name";
            string others = "others";
            FriendInfoPayload newinfo = new FriendInfoPayload(username, note_name, others);
            List<FriendInfoPayload> payload = new List<FriendInfoPayload> { newinfo };

            FriendClient client = new FriendClient(app_key, master_secret);
            client.updateFriends("xiaohuihui", payload);
            Console.ReadLine();
        }
    }
}
