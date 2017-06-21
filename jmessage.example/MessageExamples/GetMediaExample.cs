using System;
using jmessage.message;

namespace example.MessageExamples
{
    class GetMediaExample
    {
        public static string app_key = "6be9204c30b9473e87bad4dc";
        public static string master_secret = "a19bef7870c55d7e51f4c4f0";

        public static void Main(string[] args)
        {
            FileClient client = new FileClient(app_key, master_secret);
            client.getMedia("qiniu/image/j/D5E5B73186ED3C10BFD9590B5BE2A821");

            Console.ReadLine();
        }
    }
}
