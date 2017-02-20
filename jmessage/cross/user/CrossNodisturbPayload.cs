using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.cross.user
{
    public class CrossNodisturbPayload
    {
        public string appkey;
        public SingleNodisturb single;
        public GroupNodisturb group;
        public CrossNodisturbPayload(string appkey, SingleNodisturb single, GroupNodisturb group)
        {
            this.appkey = appkey;
            this.single = single;
            this.group = group;
        }
    }
    public class SingleNodisturb
    {
        public List<string> add;
        public List<string> remove;
        public SingleNodisturb(List<string> add, List<string> remove)
        {
            this.add = add;
            this.remove = remove;
        }
    }
    public class GroupNodisturb
    {
        public List<string> add;
        public List<string> remove;
        public GroupNodisturb(List<string> add, List<string> remove)
        {
            this.add = add;
            this.remove = remove;
        }
    }
}
