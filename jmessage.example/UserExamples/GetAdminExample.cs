using System;
using jmessage.user;

namespace example
{
    class GetAdminExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a564b268ba23631a8a34e687";

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始获取管理员******");

            UserClient client = new UserClient(app_key, master_secret);
            client.getAdmin(1, 2);

            Console.ReadLine();
        }
    }
}
