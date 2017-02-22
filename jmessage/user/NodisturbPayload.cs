using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.user
{
    public class NodisturbPayload
    {
        public int global;
        public SingleNodisturb single;
        public GroupNodisturb group;
        public NodisturbPayload(SingleNodisturb single, GroupNodisturb group, int global)
        {
            this.global = global;
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
