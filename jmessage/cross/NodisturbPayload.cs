using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.cross
{
    public class NodisturbPayload
    {
        public string appkey;
        public SingleNodisturbPayload single;
        public GroupNodisturbPayload group;
        public NodisturbPayload()
        {
            this.single = null;
            this.group = null;
         }
    }

    public class SingleNodisturbPayload : NodisturbPayload
    {
        public LinkedList<string> add;
        public LinkedList<string> remove;
        public SingleNodisturbPayload(string appkey,LinkedList<string> add, LinkedList<string> remove)
        {
            this.appkey = appkey;
            this.add = add;
            this.remove = remove;
        }
    }
    public class GroupNodisturbPayload : NodisturbPayload
    {
        public LinkedList<int> add;
        public LinkedList<int> remove;
        public GroupNodisturbPayload(string appkey, LinkedList<int> add, LinkedList<int> remove)
        {
            this.appkey = appkey;
            this.add = add;
            this.remove = remove;
        }
    }
}
