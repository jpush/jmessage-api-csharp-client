using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.group
{
    public class MemberPayload
    {
        public string appkey;
        public List<string> add;
        public List<string> remove;
        public MemberPayload(string appkey,List<string> add, List<string> remove)
        {
            this.appkey = appkey;
            this.add = add;
            this.remove = remove;
        }
    }
    

}
