using System.Collections.Generic;

namespace jmessage.cross.user
{
    public class CrossNodisturbPayload
    {
        public string appkey;
        public CrossSingleNodisturb single;
        public CrossGroupNodisturb group;

        public CrossNodisturbPayload(string appkey, CrossSingleNodisturb single, CrossGroupNodisturb group)
        {
            this.appkey = appkey;
            this.single = single;
            this.group = group;
        }
    }

    public class CrossSingleNodisturb
    {
        public List<string> add;
        public List<string> remove;

        public CrossSingleNodisturb(List<string> add, List<string> remove)
        {
            this.add = add;
            this.remove = remove;
        }
    }

    public class CrossGroupNodisturb
    {
        public List<string> add;
        public List<string> remove;

        public CrossGroupNodisturb(List<string> add, List<string> remove)
        {
            this.add = add;
            this.remove = remove;
        }
    }
}
