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
    class PutUserPasswordExample:JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始修改用户信息******");
            JMessageClient client = new JMessageClient(app_key, master_secret);
            //需要修改的用户的用户名
            UserPayload user = new UserPayload("jintian");
            //需要修改的字段
            user.new_password = "newpassword";
            client._messageClient.updateUserPassword(user);
            Console.ReadLine();
        }
    }
}
