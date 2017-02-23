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
    class DeleteUserBlacklistExample:JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始修改用户信息******");
            UserClient client = new UserClient(app_key, master_secret);
            string user = "jintian";
            List<string> users = new List<string> { "jintian1234" };
            client.deleteUserBlacklist(user, users);
            Console.ReadLine();
        }
    }
}
