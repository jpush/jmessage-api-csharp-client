using System;
using jmessage.user;

namespace example
{
    class DeleteUserExample : JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始修改用户信息******");

            UserClient client = new UserClient(app_key, master_secret);
            UserPayload user = new UserPayload("jintian");
            client.deleteUser(user);

            Console.ReadLine();
        }
    }
}
