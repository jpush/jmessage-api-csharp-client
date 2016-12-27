using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using jmessage.common;
using jmessage.util;

namespace jmessage.user
{
    public class UserPayload
    {
        public string username;
        public string password;
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
            this.password = null;
            this.appkey = null;
            this.nickname = null;
            this.birthday = null;
            this.gender = null;
            this.signature = null;
            this.region = null;
            this.address = null;
            this.ctime = null;
            this.mtime = null;
        }


        public string ToString(UserPayload user)
        {
            return JsonConvert.SerializeObject(user,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
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
