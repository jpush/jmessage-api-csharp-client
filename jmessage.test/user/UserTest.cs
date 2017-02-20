using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.common;
using jmessage.util;
using jmessage.user;
using jmessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test.user
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
        public void TestMethod1()
        {  
            UserPayload user = new UserPayload("jmessage123", "password");
            List<UserPayload> users = new List<UserPayload> { user };
            ResponseWrapper content = client.registUser(users);
        }
    }
}
