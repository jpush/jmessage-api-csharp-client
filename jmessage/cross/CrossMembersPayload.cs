using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.cross
{
    public class CrossMembersPayload
    {
        public String appkey;
        public List<string> add;
        public List<string> remove;
        public CrossMembersPayload()
        {
            this.appkey = null;
            this.add = null;
            this.remove = null;
        }
    }
}
