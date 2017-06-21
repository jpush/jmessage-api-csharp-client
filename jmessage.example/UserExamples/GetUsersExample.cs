using System;
using jmessage.user;

namespace example
{
    class GetUsersExample : JMessageExampleBase
    {
        public static void Main(string[] args)
        {
            UserClient client = new UserClient(app_key, master_secret);
            client.getUsers(1, 1);

            Console.ReadLine();
        }
    }
}
