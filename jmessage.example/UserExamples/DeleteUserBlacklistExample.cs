using System;
using System.Collections.Generic;
using jmessage.user;

namespace example
{
    class DeleteUserBlacklistExample : JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始修改用户信息******");

            string user = "jintian";
            List<string> users = new List<string> { "jintian1234" };

            UserClient client = new UserClient(app_key, master_secret);
            client.deleteUserBlacklist(user, users);

            Console.ReadLine();
        }
    }
}
