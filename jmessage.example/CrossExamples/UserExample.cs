using jmessage.cross.user;
using jmessage.user;
using System.Collections.Generic;

namespace example.CrossExamples
{
    class UserExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            List<string> users = new List<string> { "jmessage123" };
            string targetAppKey = "6be9204c30b9473e87bad4dc";

            List<CrossBlacklistPayload> payloads = new List<CrossBlacklistPayload> { };

            CrossBlacklistPayload crossBlacklistPayload = new CrossBlacklistPayload(targetAppKey, users);
            payloads.Add(crossBlacklistPayload);

            List<string> sadd = new List<string> { "jmessage123" };
            List<string> sremove = new List<string> { "jmessage123" };
            CrossSingleNodisturb crossSingleNodistrubPayload = new CrossSingleNodisturb(sadd, sremove);

            List<string> gadd = new List<string> { "19749893" };
            List<string> gremove = new List<string> { "19749893" };
            CrossGroupNodisturb crossGroupNodistrubPayload = new CrossGroupNodisturb(gadd, gremove);

            List<CrossNodisturbPayload> nopayloads = new List<CrossNodisturbPayload> { };
            nopayloads.Add(new CrossNodisturbPayload(targetAppKey, crossSingleNodistrubPayload, crossGroupNodistrubPayload));

            CrossUserClient client = new CrossUserClient(app_key, master_secret);
            client.crossAddBlacklist("xiaohuihui", payloads);
            client.crossRemoveBlacklist("xiaohuihui", payloads);
            client.crossGetBlacklist("xiaohuihui");
            client.crossSetNodisturb("xiaohuihui", nopayloads);
        }
    }
}
