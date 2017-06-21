using System;
using System.Collections.Generic;
using jmessage.user;

namespace example.UserExamples
{
    class NodisturbExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            UserClient client = new UserClient(app_key, master_secret);
            List<string> sadd = new List<string> { "jmessage" };
            List<string> sremove = new List<string> { "jmessage123" };
            SingleNodisturb spayload = new SingleNodisturb(sadd, sremove);

            List<string> gadd = new List<string> { "21742703" };
            List<string> gremove = new List<string> { "19749893" };
            GroupNodisturb gpayload = new GroupNodisturb(gadd, gremove);

            NodisturbPayload no = new NodisturbPayload( spayload, gpayload,1);
            client.setNodisturb("xiaohuihui", no);
        }
    }
}
