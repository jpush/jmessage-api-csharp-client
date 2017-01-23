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
    class UploadImageExample
    {
        public static String app_key = "6be9204c30b9473e87bad4dc";
        public static String master_secret = "a19bef7870c55d7e51f4c4f0";
        public static void Main(string[] args)
        {
            FileClient client = new FileClient(app_key, master_secret);
            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("","");
            string url = "https://api.im.jpush.cn/v1/resource?type=image";
            string filePath = @"C:\Users\fendouai\Desktop\book.jpg";
            String content=client.HttpPostData(url,600, "book.jpg",filePath, nvc);
            Console.WriteLine(content);
            Console.ReadLine();
        }
    }
}


