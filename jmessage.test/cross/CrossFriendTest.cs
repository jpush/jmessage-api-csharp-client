using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using jmessage.cross.user;
using jmessage.user;
using jmessage.common;
using jmessage.test;
using jmessage.cross.friend;

namespace jmessage.test.cross
{
    [TestClass]
    public class CrossFriendTest : UnitTestBase
    {
        CrossFriendClient client;
        public CrossFriendTest()
        {
            this.client = new CrossFriendClient(app_key, master_secret);
        }

        [TestMethod]
        public void crossAddFriendsTest()
        {
            String appkey = "6be9204c30b9473e87bad4dc";
            List<string> users = new List<string> { "jmessage123", "jmessage" };
            CrossFriendPayload payload = new CrossFriendPayload(appkey, users);
            
            ResponseWrapper content = client.crossAddFriends("xiaohuihui", payload);
            Assert.AreEqual(content.responseCode, HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public void crossUpdateFriendsTest()
        {
            CrossFriendInfoPayload info = new CrossFriendInfoPayload(app_key, "jmessage", "note", "other");
            List<CrossFriendInfoPayload> infos = new List<CrossFriendInfoPayload> { };
            infos.Add(info);     
            ResponseWrapper content = client.crossUpdateFriends("xiaohuihui", infos);
            Assert.AreEqual(content.responseCode, HttpStatusCode.NoContent);
        }

    }
}
