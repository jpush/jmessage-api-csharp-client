using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.cross.friend
{
    public class CrossFriendPayload
    {
        public string appkey;
        public List<string> users;
        public CrossFriendPayload(string appkey,List<string> users)
        {
            this.appkey = appkey;
            this.users = users;
        }
    }
}
