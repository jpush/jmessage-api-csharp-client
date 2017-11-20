using Jiguang.JMessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;

namespace Test
{
    [TestClass]
    public class JMessageTest
    {
        public static JMessageClient Client = new JMessageClient("e94e7f48f35698db22997434", "dee9d5286e85465ae639bb61");

        public static string Admin = "Admin";
        public static string User1 = "User1";
        public static string User2 = "User2";
        public static string User3 = "User3";
        public static string User4 = "User4";

        private List<string> sensitiveWordList = new List<string> { "funk", "操" };

        [TestMethod]
        public void TestAddSensitiveWords()
        {
            var result = Client.AddSensitiveWords(sensitiveWordList);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void TestUpdateSensitiveWord()
        {
            var result = Client.UpdateSensitiveWord("funk", "fuck");
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void TestRemoveSensitiveWord()
        {
            var result = Client.RemoveSensitiveWord("操");
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void TestGetSensitiveWords()
        {
            var result = Client.GetSensitiveWords(0, 10);

            Console.WriteLine(result.ToString());

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void TestSetSensitiveWordsStatus()
        {
            var result = Client.SetSensitiveWordsStatus(false);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public void TestGetSensitiveWordsStatus()
        {
            var result = Client.GetSensitiveWordsStatus();
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
