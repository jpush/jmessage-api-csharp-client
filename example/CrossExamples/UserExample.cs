using jmessage.cross.user;
using jmessage.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace example.CrossExamples
{
    class UserExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            CrossUserClient client = new CrossUserClient(app_key, master_secret);
            String appkey = "6be9204c30b9473e87bad4dc";
            List<string> users = new List<string> { "jmessage123" };
            CrossBlacklistPayload payload = new CrossBlacklistPayload(appkey, users);
            List<CrossBlacklistPayload> payloads = new List<CrossBlacklistPayload> { };
            payloads.Add(payload);
            client.crossAddBlacklist("xiaohuihui", payloads);

            client.crossRemoveBlacklist("xiaohuihui", payloads);

            client.crossGetBlacklist("xiaohuihui");

            //Nodisturb
            List<string> sadd = new List<string> { "jmessage123" };
            List<string> sremove = new List<string> { "jmessage123" };
            CrossSingleNodisturb spayload = new CrossSingleNodisturb(sadd, sremove);
            List<string> gadd = new List<string> { "19749893" };
            List<string> gremove = new List<string> { "19749893" };
            CrossGroupNodisturb gpayload = new CrossGroupNodisturb(gadd, gremove);
            CrossNodisturbPayload no = new CrossNodisturbPayload(appkey, spayload, gpayload);
            List<CrossNodisturbPayload> nopayloads = new List<CrossNodisturbPayload> { };
            nopayloads.Add(no);
            client.crossSetNodisturb("xiaohuihui", nopayloads);
        }
    }
}
