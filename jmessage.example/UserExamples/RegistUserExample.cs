using System;
using System.Collections.Generic;
using jmessage.common;
using jmessage.user;

namespace example
{
    class RegistUserExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        //private static readonly ILog log = LogManager.GetLogger(typeof(RegistUserExample));

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始注册用户******");
            UserClient client = new UserClient(app_key, master_secret);
            UserPayload user = new UserPayload("jmessage123", "password");
            List<UserPayload> users = new List<UserPayload> { user };
            ResponseWrapper content = client.registerUser(users);
            Console.WriteLine(content.responseContent);
        }
    }
}
