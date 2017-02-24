using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

using jmessage.common;
using jmessage.util;
using jmessage.friend;
using jmessage;

namespace jmessage.test.friend
{
    /// <summary>
    /// FriendTest 的摘要说明
    /// </summary>
    [TestClass]
    public class FriendTest : UnitTestBase
    {
        FriendClient client;

        public FriendTest()
        {
            this.client = new FriendClient(app_key, master_secret);
        }

        [TestMethod]
        public void addFriendsTest()
        {
            List<string> members_username = new List<string> {"jintian" };       
            ResponseWrapper content = client.addFriends("xiaohuihui", members_username);
            Assert.AreEqual(content.responseCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void updateFriendsTest()
        {
            String username = "jintian";
            String note_name = "new note name";
            String others = "others";
            FriendInfoPayload newinfo = new FriendInfoPayload(username, note_name, others);
            List<FriendInfoPayload> payload = new List<FriendInfoPayload> { newinfo };
            ResponseWrapper content = client.updateFriends("xiaohuihui", payload);
            Assert.AreEqual(content.responseCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void deleteFriendsTest()
        {
            List<string> members_username = new List<string> { "jintian" };
            ResponseWrapper content = client.deleteFriends("xiaohuihui", members_username);
            Assert.AreEqual(content.responseCode, HttpStatusCode.NoContent);
        }


        [TestMethod]
        public void getFriendsTest()
        {     
            ResponseWrapper content = client.getFriends("xiaohuihui");
            Assert.AreEqual(content.responseCode, HttpStatusCode.OK);
        }

    }
}
