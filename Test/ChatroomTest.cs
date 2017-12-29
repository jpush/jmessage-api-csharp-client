using Jiguang.JMessage.Chatroom;
using Jiguang.JMessage.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Test
{
    [TestClass]
    public class ChatroomTest
    {

        ChatroomInfo chatroomInfo = new ChatroomInfo()
        {
            Id = 10001654,
            Name = "TestChatroom",
            Owner = "Admin",
            Description = "TestTest!!",
            Members = new List<string> { "User1" }
        };

        [TestMethod]
        public void TestCreateChatroom()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.CreateChatroom(chatroomInfo);
            Console.WriteLine(result);

            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                JObject json = JObject.Parse(result.Content);
                chatroomInfo.Id = (long)json.GetValue("chatroom_id");
            }
        }

        [TestMethod]
        public void TestGetChatroomInfoById()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.GetChatroomInfo(new List<long> { 10001654 });
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetChatroomInfoOfUser()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.GetChatroomInfo(chatroomInfo.Owner);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetChatroomInfo()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.GetChatroomInfo(0, 1);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestUpdateChatroomInfo()
        {
            ChatroomInfo chatroomInfo = new ChatroomInfo()
            {
                Id = 10001654,
                Name = "TestChatroom2",
                Owner = "Admin",
                Description = "TestChatroomDescription2!!",
            };
            HttpResponse result = JMessageTest.Client.Chatroom.UpdateChatroomInfo(chatroomInfo);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestAddMembers()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.AddMembers(chatroomInfo.Id, new List<string> { "User2" });
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestGetMembers()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.GetMembers(chatroomInfo.Id, 0, 5);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestRemoveMembers()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.RemoveMembers(chatroomInfo.Id, new List<string> { "User2" });
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestUpdateUserForbiddenStatus()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.UpdateUserForbiddenStatus(chatroomInfo.Id, "User1", true);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestDeleteChatroom()
        {
            HttpResponse result = JMessageTest.Client.Chatroom.DeleteChatroom(chatroomInfo.Id);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }
    }
}
