namespace jmessage.cross.friend
{
    public class CrossFriendInfoPayload
    {
        public string appkey;
        public string username;
        public string note_name;
        public string others;

        public CrossFriendInfoPayload(string appkey, string username, string note_name, string others)
        {
            this.appkey = appkey;
            this.username = username;
            this.note_name = note_name;
            this.others = others;
        }
    }
}
