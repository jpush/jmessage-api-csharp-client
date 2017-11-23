using Jiguang.JMessage.Message;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Test
{
    [TestClass]
    public class MessageTest
    {
        private string sender = "Admin";
        private string target = "User1";

        private long messageId;

        [TestMethod]
        public void TestSendTextMessage()
        {
            var msg = new TextMessage
            {
                TargetType = "single",
                TargetId = target,
                FromId = sender,
                Content = new TextMessage.MessageBody
                {
                    Text = "Hello world",
                    Extras = new Dictionary<string, string>
                    {
                        { "key1", "value1" }
                    }
                }
            };

            var result = JMessageTest.Client.Message.Send(msg);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                JObject json = JObject.Parse(result.Content);
                messageId = (long)json.GetValue("msg_id");
            }
        }

        private string mediaId;

        [TestMethod]
        public void TestUploadImage()
        {
            var imgPath = "C:/Users/hasee/Pictures/201711181746489.jpg";
            var result = JMessageTest.Client.Message.FileUpload(imgPath, "image");
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                JObject json = JObject.Parse(result.Content);
                mediaId = (string)json.GetValue("media_id");
            }
        }

        [TestMethod]
        public void TestDownloadImage()
        {
            var result = JMessageTest.Client.Message.FileDownload("qiniu/image/j/E22D4F4A12C6FED95CF42DCC64569E69.jpg");
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestSendImageMessage()
        {
            var msg = new ImageMessage {
                TargetType = "single",
                TargetId = target,
                FromId = sender,
                Content = new ImageMessage.MessageBody
                {
                    MediaId = "qiniu/image/j/E22D4F4A12C6FED95CF42DCC64569E69.jpg",
                    MediaCrc32 = 4064239951,
                    Width = 3840,
                    Height = 2160,
                    Format = "jpg",
                    Size = 2695035
                }
            };

            var result = JMessageTest.Client.Message.Send(msg);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void TestRetractMessage()
        {
            var msg = new TextMessage
            {
                TargetType = "single",
                TargetId = target,
                FromId = sender,
                Content = new TextMessage.MessageBody
                {
                    Text = "Hello world",
                    Extras = new Dictionary<string, string>
                    {
                        { "key1", "value1" }
                    }
                }
            };

            var sendResult = JMessageTest.Client.Message.Send(msg);
            Console.WriteLine(sendResult.ToString());

            if (sendResult.StatusCode == HttpStatusCode.Created)
            {
                JObject json = JObject.Parse(sendResult.Content);
                var msgId = (long)json.GetValue("msg_id");

                var result = JMessageTest.Client.Message.Retract("Admin", msgId);
                Console.WriteLine(result.ToString());
                Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
            }

        }
    }
}
