using System;
using jmessage.user;

namespace example
{
    class GetUserExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a564b268ba23631a8a34e687";

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始获取用户信息******");

            UserClient client = new UserClient(app_key, master_secret);
            client.getUser("jintian");

            Console.ReadLine();
        }
    }
}
