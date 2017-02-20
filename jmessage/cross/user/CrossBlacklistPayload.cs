using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.cross.user
{
    public class CrossBlacklistPayload
    {
        public string appkey;
        public List<string> usernames;
        public CrossBlacklistPayload(string appkey,List<string> usernames)
        {
            this.appkey = appkey;
            this.usernames = usernames;
        }
    }
}
