using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

using jmessage.common;
using jmessage.util;
using jmessage.user;
using jmessage.message;
using jmessage;

namespace jmessage.test.message
{
    /// <summary>
    /// MessageTest 的摘要说明
    /// </summary>
    [TestClass]
    public class MessageTest : UnitTestBase
    {
        MessageClient client;
        public MessageTest()
        {
            this.client = new MessageClient(app_key, master_secret);
    }

        [TestMethod]
        public void sendTextMessageTest()
        {
            TextMsg_body msg_body = new TextMsg_body();
            msg_body.text = "msg_type text";
            msg_body.extras = null;
            TextMessagePayload payload = new TextMessagePayload("1", "single", "admin", "text", "xiaohuihui", "admin", msg_body);           
            ResponseWrapper content = client.sendMessage(payload);
            Assert.AreEqual(content.responseCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void sendImageMessageTest()
        {
            ImageMsg_body msg_body = new ImageMsg_body();
            msg_body.media_id = "qiniu/image/CE0ACD035CBF71F8";
            msg_body.media_crc32 = 2778919613;
            msg_body.width = 3840;
            msg_body.height = 2160;
            msg_body.fsize = 3328738;
            msg_body.format = "jpg";
            msg_body.extras = null;
            ImageMessagePayload payload = new ImageMessagePayload("1", "single", "admin", "image", "xiaohuihui", "admin", msg_body);
            string payloadstring = payload.ToString(payload);
            ResponseWrapper content = client.sendMessage(payload);
            Assert.AreEqual(content.responseCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void sendMessageTest()
        {
            FileClient client = new FileClient(app_key, master_secret);  
            ResponseWrapper content = client.getMedia("qiniu/image/j/D5E5B73186ED3C10BFD9590B5BE2A821");
            Assert.AreEqual(content.responseCode, HttpStatusCode.OK);
        }
    }
}
