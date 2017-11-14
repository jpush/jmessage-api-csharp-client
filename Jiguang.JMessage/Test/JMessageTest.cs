using Jiguang.JMessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net;

namespace Test
{
    [TestClass]
    public class JMessageTest
    {
        public JMessageClient client = new JMessageClient("b99f062ffc07bc9b3a4e92d7", "5a30a306ea8096212dc52b30");

        private List<string> sensitiveWordList = new List<string> { "funk", "操" };

        [TestMethod]
        public void TestAddSensitiveWords()
        {
            var result = client.AddSensitiveWords(sensitiveWordList);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void TestUpdateSensitiveWord()
        {
            var result = client.UpdateSensitiveWord("funk", "fuck");
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void TestRemoveSensitiveWord()
        {
            var result = client.RemoveSensitiveWord("操");
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void TestGetSensitiveWords()
        {
            var result = client.GetSensitiveWords(0, 10);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestSetSensitiveWordsStatus()
        {
            var result = client.SetSensitiveWordsStatus(false);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void TestGetSensitiveWordsStatus()
        {
            var result = client.GetSensitiveWordsStatus();
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
