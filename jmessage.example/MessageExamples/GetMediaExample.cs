using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.message;
using jmessage;

namespace example.MessageExamples
{
    class GetMediaExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            FileClient client = new FileClient(app_key, master_secret);
            client.getMedia("qiniu/image/j/D5E5B73186ED3C10BFD9590B5BE2A821");
            Console.ReadLine();
        }
    }
}
