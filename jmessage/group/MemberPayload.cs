using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.group
{
    public class MemberPayload
    {

        public List<string> add;
        public List<string> remove;
        public MemberPayload(List<string> add, List<string> remove)
        {
            this.add = add;
            this.remove = remove;
        }
    }
    

}
