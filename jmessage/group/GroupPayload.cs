using System.Collections.Generic;
using Newtonsoft.Json;

namespace jmessage.group
{
    public class GroupPayload
    {
        public string name;
        public string desc;
        public string owner_username;
        public List<string> members_username;
        public int? MaxMemberCount;
        public string ctime;
        public string mtime;

        public GroupPayload()
        {
            name = null;
            desc = null;
            owner_username = null;
            members_username = null;
            MaxMemberCount = null;
            ctime = null;
            mtime = null;
        }

        public GroupPayload(string name, string owner_username, List<string> members_username, string desc)
        {
            this.name = name;
            this.desc = desc;
            this.owner_username = owner_username;
            this.members_username = members_username;
            this.MaxMemberCount = null;
            this.ctime = null;
            this.mtime = null;
        }

        public string ToString(GroupPayload group)
        {
            return JsonConvert.SerializeObject(group, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public GroupPayload Check()
        {
            return this;
        }
    }
}
