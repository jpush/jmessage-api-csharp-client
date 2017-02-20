using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.cross.group
{
    public class CrossMemberPayload
    {
        public string appkey;
        public List<string> add;
        public List<string> remove;
        public CrossMemberPayload(string appkey,List<string> add, List<string> remove)
        {
            this.appkey = appkey;
            this.add = add;
            this.remove = remove;
        }
    }
    

}
