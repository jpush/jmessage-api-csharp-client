using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.cross.user
{
    public class CrossNodisturbPayload
    {
        public string appkey;
        public Single single;
        public Group group;
        public CrossNodisturbPayload(string appkey, Single single,Group group)
        {
            this.appkey = appkey;
            this.single = single;
            this.group = group;
        }
    }
    public class Single
    {
        public List<string> add;
        public List<string> remove;
        public Single(List<string> add, List<string> remove)
        {
            this.add = add;
            this.remove = remove;
        }
    }
    public class Group
    {
        public List<string> add;
        public List<string> remove;
        public Group(List<string> add, List<string> remove)
        {
            this.add = add;
            this.remove = remove;
        }
    }
}
