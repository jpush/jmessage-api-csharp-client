using System.Collections.Generic;

namespace jmessage.cross.group
{
    public class CrossMemberPayload
    {
        public string appkey;
        public List<string> add;
        public List<string> remove;

        public CrossMemberPayload(string appkey, List<string> add, List<string> remove)
        {
            this.appkey = appkey;
            this.add = add;
            this.remove = remove;
        }
    }
}
