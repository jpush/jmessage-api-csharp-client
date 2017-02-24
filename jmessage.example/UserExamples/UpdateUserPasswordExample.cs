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
    class UpdateUserPasswordExample: JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            UserClient client = new UserClient(app_key, master_secret);
            //需要修改的用户的用户名
            UserPayload user = new UserPayload("jintian");
            //需要修改的字段
            user.new_password = "newpassword";
            client.updateUserPassword(user);
            Console.ReadLine();
        }
    }
}
