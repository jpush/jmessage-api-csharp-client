using System;
using jmessage.user;

namespace example
{
    class GetUserStatExample : JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            UserClient client = new UserClient(app_key, master_secret);
            UserPayload user = new UserPayload("jintian");
            client.getUserStat(user);

            Console.ReadLine();
        }
    }
}
