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
    class PutUserExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a564b268ba23631a8a34e687";

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始修改用户信息******");
            JMessageClient client = new JMessageClient(app_key, master_secret);
            //需要修改的用户的用户名
            UserPayload user = new UserPayload("jintian");
            //需要修改的字段
            user.nickname = "nickname";
            client._messageClient.putUser(user);
            Console.ReadLine();
        }
    }
}
