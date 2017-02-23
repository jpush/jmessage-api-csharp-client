using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.common;
using jmessage.util;
using jmessage.user;
using jmessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jmessage.test.user
{
    /// <summary>
    /// UserTest 的摘要说明
    /// </summary>
    [TestClass]
    public class UserTest: UnitTestBase
    {
        UserClient client;
        public UserTest()
        {
            this.client = new UserClient(app_key, master_secret);
        }

        [TestMethod]
        public void registUserTest()
        {  
            UserPayload user = new UserPayload("jmessage123", "password");
            List<UserPayload> users = new List<UserPayload> { user };
            ResponseWrapper content = client.registUser(users);
        }

        [TestMethod]
        public void registAdminTest()
        {
            UserPayload user = new UserPayload("jmessage123", "password");
            UserPayload admin = new UserPayload("jmessage", "password");
            ResponseWrapper content = client.registAdmin(admin);
        }


        [TestMethod]
        public void getUserTest()
        {
            ResponseWrapper content = client.getUser("jintian");
        }

        [TestMethod]
        public void getAdminTest()
        {
            ResponseWrapper content = client.getAdmin(1, 2);
        }



    }
}
