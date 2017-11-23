using Jiguang.JMessage.Group;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Test
{
    [TestClass]
    public class GroupTest
    {

        private long groupId;

        [TestMethod]
        public void TestCreateGroup()
        {
            GroupInfo groupInfo = new GroupInfo
            {
                Name = "TestGroup",
                Owner = "Admin",
            };

            List<string> members = new List<string> { JMessageTest.User1, JMessageTest.User2 };

            var result = JMessageTest.Client.Group.CreateGroup(groupInfo, members);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);

            if (result.StatusCode == HttpStatusCode.Created)
            {
                JObject groupInfoJsonObj = JObject.Parse(result.Content);
                groupId = (long)groupInfoJsonObj.GetValue("gid");
            }
        }

        [TestMethod]
        public void TestGetGroupInfo()
        {
            Console.WriteLine(groupId);

            var result = JMessageTest.Client.Group.GetGroupInfo(groupId);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestUpdateGroupInfo()
        {
            GroupInfo groupInfo = new GroupInfo
            {
                Id = groupId,
                Description = "TestGroupDescription"
            };

            var result = JMessageTest.Client.Group.UpdateGroupInfo(groupInfo);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestAddMembers()
        {
            var result = JMessageTest.Client.Group.AddMembers(groupId,
                new List<string> { JMessageTest.User3, JMessageTest.User4 });
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestRemoveMemebers()
        {
            var result = JMessageTest.Client.Group.RemoveMembers(groupId,
                new List<string> { JMessageTest.User3, JMessageTest.User4 });
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestGetMembers()
        {
            var result = JMessageTest.Client.Group.GetMembers(groupId);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetGroupListOfApp()
        {
            var result = JMessageTest.Client.Group.GetGroupList(0, 1);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetGroupListOfUser()
        {
            var result = JMessageTest.Client.Group.GetGroupList("Admin");
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestDeleteGroup()
        {
            var result = JMessageTest.Client.Group.DeleteGroup(groupId);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }
    }
}
