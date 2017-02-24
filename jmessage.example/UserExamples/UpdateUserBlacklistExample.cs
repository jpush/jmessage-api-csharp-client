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
            UserClient client = new UserClient(app_key, master_secret);
            string user = "jintian";
            List<string> users = new List<string> { "jintian1234" };
            client.updateUserBlacklist(user, users);
            Console.ReadLine();
        }
    }
}
