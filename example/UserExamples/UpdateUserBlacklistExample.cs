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
    class UpdateUserBlackListExample : JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始修改用户信息******");
            JMessageClient client = new JMessageClient(app_key, master_secret);
            //需要修改的用户的用户名
            string user = "jintian";
            List<string> users = new List<string> { "jintian1234" };
            client._messageClient.putUserBlacklist(user, users);
            Console.ReadLine();
        }
    }
}
