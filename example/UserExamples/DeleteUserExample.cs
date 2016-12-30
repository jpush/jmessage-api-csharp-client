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
    class DeleteUserExample:JMessageExampleBase
    {

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始修改用户信息******");
            JMessageClient client = new JMessageClient(app_key, master_secret);
            UserPayload user = new UserPayload("jintian");
            client._messageClient.deleteUser(user);
            Console.ReadLine();
        }
    }
}
