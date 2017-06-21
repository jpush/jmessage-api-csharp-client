using System.Collections.Generic;

namespace jmessage.cross.friend
{
    public class CrossFriendPayload
    {
        public string appkey;
        public List<string> users;

        public CrossFriendPayload(string appkey, List<string> users)
        {
            this.appkey = appkey;
            this.users = users;
        }
    }
}
