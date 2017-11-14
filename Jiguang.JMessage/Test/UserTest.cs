using Jiguang.JMessage;
using Jiguang.JMessage.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class UserTest
    {
        JMessageClient client = new JMessageClient("b99f062ffc07bc9b3a4e92d7", "5a30a306ea8096212dc52b30");

        UserInfo userInfo = new UserInfo
        {
            Username = "TestUser1",
            Password = "1234",
            Nickname = "TestNickName1",
            Gender = 1,
            Birthday = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
            Region = "Guangdong",
            Address = "Shenzhenxxx",
            Signature = "Still waters run deep",
            Extras = new Dictionary<string, string>
            {
                { "key1", "value1" }
            }
        };

        [TestMethod]
        public void TestRegister()
        {
            var result = client.User.Register(new List<UserInfo>() { userInfo });
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.Created);
        }

        [TestMethod]
        public void TestGetUserInfo()
        {
            var result = client.User.GetUserInfo("test00");
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, System.Net.HttpStatusCode.Created);
        }

    }
}
