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
    class GetUserExample
    {

        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a564b268ba23631a8a34e687";

        public static void Main(string[] args)
        {
            Console.WriteLine("*****开始注册管理员******");
            JMessageClient client = new JMessageClient(app_key, master_secret);
            Console.ReadLine();
        }
    }
}
