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


namespace jmessage.test.cross
{
    [TestClass]
    public class CrossUserTest : UnitTestBase
    {
        CrossUserClient client;
        public CrossUserTest()
        {
            this.client = new CrossUserClient(app_key, master_secret);
        }

        [TestMethod]
        public void crossAddBlacklistTest()
        {
            String appkey = "6be9204c30b9473e87bad4dc";
            List<string> users = new List<string> { "jmessage123" };
            CrossBlacklistPayload payload = new CrossBlacklistPayload(appkey, users);
            List<CrossBlacklistPayload> payloads = new List<CrossBlacklistPayload> { };
            payloads.Add(payload);      
            ResponseWrapper content = client.crossAddBlacklist("xiaohuihui", payloads);
            Assert.AreEqual(content.responseCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void crossRemoveBlacklistTest()
        {
            String appkey = "6be9204c30b9473e87bad4dc";
            List<string> users = new List<string> { "jmessage123" };
            CrossBlacklistPayload payload = new CrossBlacklistPayload(appkey, users);
            List<CrossBlacklistPayload> payloads = new List<CrossBlacklistPayload> { };
            payloads.Add(payload);
            ResponseWrapper content = client.crossRemoveBlacklist("xiaohuihui", payloads);
            Assert.AreEqual(content.responseCode, HttpStatusCode.NoContent);
        }

        [TestMethod]
        public void crossGetBlacklistTest()
        {
            ResponseWrapper content = client.crossGetBlacklist("xiaohuihui");
            Assert.AreEqual(content.responseCode, HttpStatusCode.OK);
        }


        [TestMethod]
        public void crossSetNodisturbTest()
        {
            String appkey = "6be9204c30b9473e87bad4dc";
            List<string> sadd = new List<string> { "jmessage123" };
            List<string> sremove = new List<string> { "jmessage123" };
            CrossSingleNodisturb spayload = new CrossSingleNodisturb(sadd, sremove);
            List<string> gadd = new List<string> { "19749893" };
            List<string> gremove = new List<string> { "19749893" };
            CrossGroupNodisturb gpayload = new CrossGroupNodisturb(gadd, gremove);
            CrossNodisturbPayload no = new CrossNodisturbPayload(appkey, spayload, gpayload);
            List<CrossNodisturbPayload> nopayloads = new List<CrossNodisturbPayload> { };
            nopayloads.Add(no);
            ResponseWrapper content = client.crossSetNodisturb("xiaohuihui", nopayloads);
            Assert.AreEqual(content.responseCode, HttpStatusCode.NoContent);
        }
    }
}
