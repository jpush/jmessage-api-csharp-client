using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using jmessage.util;
using jmessage.common;
using System.Diagnostics;
using Newtonsoft.Json;
using jmessage.message;
using jmessage.group;
using jmessage.cross;
using jmessage;

namespace example.CrossExamples
{
    class CrossAppsSetNodisturbExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            CrossClient client = new CrossClient(app_key, master_secret);
            List<Hashtable> payloads = new List<Hashtable>();
            Hashtable payload = new Hashtable();
            payload["appkey"] = app_key;
            Hashtable single = new Hashtable();          
            List<string> usernames = new List<string> { "jmessage123" };
            single["add"] = usernames;
            payload["single"] = single;
            Hashtable group = new Hashtable();
            List<string> groups = new List<string> { "20292095" };
            group["add"] = groups;
            payload["group"] = group;

            payloads.Add(payload);
            client.crossAppsSetNodisturb("xiaohuihui",payloads);
        }
    }
}
