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
using jmessage.corss.group;
using jmessage.cross.group;

namespace jmessage.test.cross
{
    [TestClass]
    public class CrossGroupTest : UnitTestBase
    {
        CrossGroupClient client;
        public CrossGroupTest()
        {
            this.client = new CrossGroupClient(app_key, master_secret);
        }

        [TestMethod]
        public void crossAddRemoveMembersTest()
        {
            String appkey = "6be9204c30b9473e87bad4dc";
            List<string> add = new List<string> { "jmessage123" };
            CrossMemberPayload payload = new CrossMemberPayload(appkey, add, null);
            List<CrossMemberPayload> payloads = new List<CrossMemberPayload> { };
            payloads.Add(payload);    
            ResponseWrapper content = client.crossAddRemoveMembers("19749893", payloads);
            Assert.AreEqual(content.responseCode, HttpStatusCode.BadRequest);
        }

        [TestMethod]
        public void crossGetMembersTest()
        {
            ResponseWrapper content = client.crossGetMembers("19749893");
            Assert.AreEqual(content.responseCode, HttpStatusCode.OK);
        }

    }
}
