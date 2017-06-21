using Newtonsoft.Json;

namespace jmessage.user
{
    public class UserPayload
    {
        public string username;
        public string password;
        public string new_password;
        public string appkey;
        public string nickname;
        public string birthday;
        public int? gender;
        public string signature;
        public string region;
        public string address;
        public string ctime;
        public string mtime;

        public UserPayload(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public UserPayload(string username)
        {
            this.username = username;
            password = null;
            new_password = null;
            appkey = null;
            nickname = null;
            birthday = null;
            gender = null;
            signature = null;
            region = null;
            address = null;
            ctime = null;
            mtime = null;
        }

        public string ToString(UserPayload user)
        {
            return JsonConvert.SerializeObject(user, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public UserPayload Check()
        {
            return this;
        }
    }
}
