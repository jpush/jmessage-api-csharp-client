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
    class Program
    {

        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a564b268ba23631a8a34e687";

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始发送******");
            JMessageClient client = new JMessageClient(app_key, master_secret);
            UserPayload user = new UserPayload("jintianshigehaorizi", "password");
            List<UserPayload> users = new List<UserPayload> {user};
            client._messageClient.registUser(users);
            Console.ReadLine();
        }
    }
}
