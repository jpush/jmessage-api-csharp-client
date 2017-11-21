using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Net;

namespace Test
{
    [TestClass]
    public class ReportTest
    {

        private string beginTime = "2017-11-20 00:00:00";
        private string endTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

        private string cursor;
        private string userCursor;
        private string groupCursor;

        [TestMethod]
        public void TestGetMessageHistory()
        {
            var result = JMessageTest.Client.Report.GetMessageHistory(10, beginTime, endTime);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                JObject json = JObject.Parse(result.Content);
                cursor = (string)json.GetValue("cursor");
            }
        }

        [TestMethod]
        public void TestGetMessaegHistoryByCursor()
        {
            var result = JMessageTest.Client.Report.GetMessageHistory(cursor);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetUserMessageHistory()
        {
            var result = JMessageTest.Client.Report.GetUserMessageHistory("Admin", 10, beginTime, endTime);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                JObject json = JObject.Parse(result.Content);
                userCursor = (string)json.GetValue("cursor");
            }
        }

        [TestMethod]
        public void TestGetUserMessageHistoryByCursor()
        {
            var result = JMessageTest.Client.Report.GetUserMessageHistory("Admin", userCursor);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetGroupMessageHistory()
        {
            var result = JMessageTest.Client.Report.GetGroupMessageHistory(10256738, 10, beginTime, endTime);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                JObject json = JObject.Parse(result.Content);
                groupCursor = (string)json.GetValue("cursor");
            }
        }

        [TestMethod]
        public void TestGetGroupMessageHistoryByCursor()
        {
            var result = JMessageTest.Client.Report.GetGroupMessageHistory(10256738, groupCursor);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetUserStatistic()
        {
            var result = JMessageTest.Client.Report.GetUserStatistic(beginTime, 30);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

        [TestMethod]
        public void TestGetGroupStatistic()
        {
            var result = JMessageTest.Client.Report.GetGroupStatistic(beginTime, 30);
            Console.WriteLine(result);
            Assert.AreEqual(result.StatusCode, HttpStatusCode.OK);
        }

    }
}
