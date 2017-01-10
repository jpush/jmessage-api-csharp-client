using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.common;
using jmessage.util;
using jmessage.user;
using jmessage;

namespace example
{
    class RegistUserExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始注册用户******");
            JMessageClient client = new JMessageClient(app_key, master_secret);
            UserPayload user = new UserPayload("xiaohuihui", "password");
            List<UserPayload> users = new List<UserPayload> { user };
            client._messageClient.registUser(users);
            Console.ReadLine();
        }
    }
}
