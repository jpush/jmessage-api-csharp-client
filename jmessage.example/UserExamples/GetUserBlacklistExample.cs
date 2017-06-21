using System;
using jmessage.user;

namespace example
{
    class GetUserBlackListExample : JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始修改用户信息******");

            UserClient client = new UserClient(app_key, master_secret);
            string user = "jintian";    // 需要修改的用户的用户名
            client.getUserBlacklist(user);

            Console.ReadLine();
        }
    }
}
