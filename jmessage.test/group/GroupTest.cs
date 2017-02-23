using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmessage.common;
using jmessage.util;
using jmessage.group;
using jmessage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace jmessage.test.group
{
    [TestClass]
    public class GroupTest : UnitTestBase
        {
            GroupClient client;
            public GroupTest()
            {
                this.client = new GroupClient(app_key, master_secret);
            }

            [TestMethod]
            public void createGroupTest()
            {
            List<string> members_username = new List<string> { "xiaohuihui", "jintian" };
            GroupPayload payload = new GroupPayload("toms", "xiaohuihui", members_username, "jmessage");
            ResponseWrapper content = client.createGroup(payload);
            }

            [TestMethod]
            public void deleteGroupTest()
            {
                ResponseWrapper content = client.deleteGroup(20293553);
            }


            [TestMethod]
            public void updateGroupTest()
            {
            GroupPayload payload = new GroupPayload();
            payload.desc = "new desc";
            ResponseWrapper content = client.updateGroup(20292095, payload);
            }

            [TestMethod]
            public void updateGroupMembersTest()
            {
            List<string> add = new List<string> { "jmessage" };
            List<string> remove = new List<string> { "jmessage123" };
            MemberPayload payload = new MemberPayload(add, remove);
            ResponseWrapper content = client.updateGroupMembers(19749893, payload);
        }

        }
    }
