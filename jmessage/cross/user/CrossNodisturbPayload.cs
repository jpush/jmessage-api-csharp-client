using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace jmessage.cross.user
{
    public class CrossNodisturbPayload
    {
        string appkey;
        Single single;
        Group group;
    }
    public class Single
    {
        List<string> add;
        List<string> remove;
    }
    public class Group
    {
        List<string> add;
        List<string> remove;
    }
}
