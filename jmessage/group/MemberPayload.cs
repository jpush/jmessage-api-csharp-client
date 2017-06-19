using System.Collections.Generic;

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
