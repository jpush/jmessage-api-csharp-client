using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.common;
using jmessage.util;
using jmessage.user;
using jmessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace jmessage.test.user
{
    /// <summary>
    /// UserTest 的摘要说明
    /// </summary>
    [TestClass]
    public class UserTest: UnitTestBase
    {
        UserClient client;
        readonly String username;
        public UserTest()
        {
            Random ran = new Random();
            int RandKey = ran.Next(100, 99999999);
            String randString = RandKey.ToString();
            this.username= "jmessage" + randString;
            this.client = new UserClient(app_key, master_secret);
        }

        [TestMethod]
        public void registUserTest()
        {
            String username = this.username;
            UserPayload user = new UserPayload(username, "password");
            List<UserPayload> users = new List<UserPayload> { user };
            ResponseWrapper content = client.registUser(users);
            //repeat add user
            Assert.AreEqual(content.responseCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public void registAdminTest()
        {
            Random ran = new Random();
            int RandKey = ran.Next(100, 99999999);
            String randString = RandKey.ToString();
            UserPayload admin = new UserPayload("jmessage" + randString, "password");
            ResponseWrapper content = client.registAdmin(admin);
            //repeat add user
            Assert.AreEqual(content.responseCode, HttpStatusCode.Created);
        }


        [TestMethod]
        public void getUserTest()
        {
            ResponseWrapper content = client.getUser("jintian");
            Assert.AreEqual(content.responseCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void getAdminTest()
        {
            ResponseWrapper content = client.getAdmin(1, 2);
            Assert.AreEqual(content.responseCode, HttpStatusCode.OK);
        }


        [TestMethod]
        public void getUsersTest()
        {
            ResponseWrapper content = client.getUsers(1, 1);
            Assert.AreEqual(content.responseCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void updateUserTest()
        {
            UserPayload user = new UserPayload("jintian");
            //需要修改的字段
            user.nickname = "nickname";
            ResponseWrapper content = client.updateUser(user);
            Assert.AreEqual(content.responseCode, HttpStatusCode.NoContent);
        }


        [TestMethod]
        public void getUserStatTest()
        {
            UserPayload user = new UserPayload("jintian");
            ResponseWrapper content = client.getUserStat(user);
            Assert.AreEqual(content.responseCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void updateUserPasswordTest()
        {
            UserPayload user = new UserPayload("jintian");
            //需要修改的字段
            user.new_password = "newpassword"; 
            ResponseWrapper content = client.updateUserPassword(user);
            Assert.AreEqual(content.responseCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void deleteUserTest()
        {
            String username = this.username;
            UserPayload user = new UserPayload(username);            
            ResponseWrapper content = client.deleteUser(user);
            Assert.AreEqual(content.responseCode, HttpStatusCode.Forbidden);
        }

    }
}
