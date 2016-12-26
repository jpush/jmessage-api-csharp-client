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
        public UserPayload(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public string ToJson(UserPayload user)
        {
            return JsonConvert.SerializeObject(user);
        }
        public UserPayload Check()
        {
            return this;
        }
    }
}
