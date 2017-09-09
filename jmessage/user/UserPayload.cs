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

        /// <summary>
        /// 生日。
        /// 格式要求为 yyyy-MM-dd HH:mm:ss
        /// </summary>
        public string birthday;

        /// <summary>
        /// 用户性别。
        /// 0：未知；1：男；2：女。
        /// </summary>
        public int? gender;

        /// <summary>
        /// 用户个性签名。
        /// </summary>
        public string signature;

        /// <summary>
        /// 用户所属地区。
        /// </summary>
        public string region;
        
        /// <summary>
        /// 用户详细地址。
        /// </summary>
        public string address;

        /// <summary>
        /// 用户创建时间。
        /// </summary>
        public string ctime;

        /// <summary>
        /// 用户最后修改时间。
        /// </summary>
        public string mtime;

        /// <summary>
        /// 需要填从文件上传接口获得的 media_id。
        /// </summary>
        public string avatar; 

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
