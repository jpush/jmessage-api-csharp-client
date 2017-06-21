using System;
using jmessage.user;

namespace example
{
    class UpdateUserExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a564b268ba23631a8a34e687";

        public static void Main(string[] args)
        {
            UserClient client = new UserClient(app_key, master_secret);
            //需要修改的用户的用户名
            UserPayload user = new UserPayload("jintian");
            //需要修改的字段
            user.nickname = "nickname";
            client.updateUser(user);
            Console.ReadLine();
        }
    }
}
