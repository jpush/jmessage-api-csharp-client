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
using System.Collections.Specialized;

namespace example.MessageExamples
{
    class UploadFileExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            FileClient client = new FileClient(app_key, master_secret);
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("id", "TTR");
            nvc.Add("btn-submit-photo", "Upload");
            String content=client.HttpPostData("https://api.im.jpush.cn/v1/resource?type=image",600000, "touch-icon-iphone.png",
                 @"C:\Users\fendouai\Desktop\touch-icon-iphone.png", nvc);
            Console.WriteLine(content);
            Console.ReadLine();
        }
    }
}


