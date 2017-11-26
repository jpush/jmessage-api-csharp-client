using Jiguang.JMessage.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Test
{
    [TestClass]
    public class UserTest
    {

        // Admin Test Start

        public static UserInfo Admin = new UserInfo
        {
            Username = "TestAdmin1",
            Password = "1234",
            Nickname = "TestAdminNickName1",
            Gender = 2,
            Birthday = DateTime.Now.ToString("yyyy-MM-dd"),
            Region = "Guangdong",
            Address = "Shenzhenxxx",
            Signature = "Hello world",
            Extras = new Dictionary<string, string>
            {
                { "key1", "value1" }
            }
        };

        [TestMethod]
        public void TestRegisterAsAdmin()
        {
            var result = JMessageTest.Client.User.RegisterAsAdmin(Admin);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void TestGetAdminList()
        {
            var result = JMessageTest.Client.User.GetAdminList(0, 1);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        // Admin Test End

        // User Test Start

        public static UserInfo User1 = new UserInfo
        {
            Username = "TestUser1",
            Password = "1234",
            Nickname = "TestNickName1",
            Gender = 1,
            Birthday = DateTime.Now.ToString("yyyy-MM-dd"),
            Region = "Guangdong",
            Address = "Shenzhenxxx",
            Signature = "Still waters run deep",
            Extras = new Dictionary<string, string>
            {
                { "key1", "value1" }
            }
        };

        UserInfo user2 = new UserInfo
        {
            Username = "TestUser2",
            Password = "1234",
        };

        UserInfo user3 = new UserInfo
        {
            Username = "TestUser3",
            Password = "1234",
        };

        UserInfo user4 = new UserInfo
        {
            Username = "TestUser4",
            Password = "1234",
        };

        [TestMethod]
        public void TestRegister()
        {
            var result = JMessageTest.Client.User.Register(new List<UserInfo>() { User1, user2, user3, user4 });
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void TestGetUserInfo()
        {
            var result = JMessageTest.Client.User.GetUserInfo(User1.Username);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestUpdateUserInfo()
        {
            User1.Nickname = "TestNickname11";
            var result = JMessageTest.Client.User.UpdateUserInfo(User1);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestUpdatePassword()
        {
            var newPassword = "4321";
            var result = JMessageTest.Client.User.UpdatePassword(User1.Username, newPassword);
            Console.WriteLine(result.ToString());

            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);

            if (result.StatusCode == HttpStatusCode.NoContent)
            {
                User1.Password = newPassword;
            }
        }

        [TestMethod]
        public void TestCheckStatus()
        {
            var result = JMessageTest.Client.User.CheckStatus(User1.Username);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestAddToBlackList()
        {
            var result = JMessageTest.Client.User.AddToBlackList(User1.Username, new List<string> { user2.Username });
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestRemoveFromBlackList()
        {
            var result = JMessageTest.Client.User.RemoveFromBlackList(User1.Username, new List<string> { user2.Username });
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestGetBlackList()
        {
            var result = JMessageTest.Client.User.GetBlackList(User1.Username);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestSetUserNoDisturb()
        {
            var result = JMessageTest.Client.User.SetUserNoDisturb(User1.Username,
                new List<string> { user3.Username }, true);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestSetGlobalNoDisturb()
        {
            var result = JMessageTest.Client.User.SetGlobalNoDisturb(User1.Username, false);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestAddFriends()
        {
            var result = JMessageTest.Client.User.AddFriends(User1.Username, new List<string> { user3.Username });
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void TestGetFriendList()
        {
            var result = JMessageTest.Client.User.GetFriendList(User1.Username);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void UpdateFriendsNoteInfo()
        {
            JArray friends = new JArray();

            JObject json = new JObject
            {
                { "username", user3.Username },
                { "note_name", "TestFriendsNotename" },
                { "others", "hello :)" }
            };
            friends.Add(json);

            var result = JMessageTest.Client.User.UpdateFriendNoteInfo(User1.Username, json.ToString());
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestRemoveFriends()
        {
            var result = JMessageTest.Client.User.RemoveFriends(User1.Username, new List<string> { user3.Username });
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void TestDisable()
        {
            var result = JMessageTest.Client.User.Disable(user4.Username, true);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);

            JMessageTest.Client.User.Disable(user4.Username, false);
        }


        [TestMethod]
        public void TestDelete()
        {
            var result = JMessageTest.Client.User.Delete(user4.Username);
            Console.WriteLine(result.ToString());
            Assert.AreEqual(result.StatusCode, HttpStatusCode.NoContent);
        }

        // User Test End
    }
}
