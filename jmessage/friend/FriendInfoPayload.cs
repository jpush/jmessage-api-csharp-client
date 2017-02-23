using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.friend
{
    public class FriendInfoPayload
    {
        public string username;
        public string note_name;
        public string others;
        public FriendInfoPayload(string username,string note_name,string others)
        {
            this.username = username;
            this.note_name = note_name;
            this.others = others;
        }
    }
}
