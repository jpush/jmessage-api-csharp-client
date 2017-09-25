using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel;

namespace Jiguang.JMessage.Model
{
    public class UserInfo
    {
        /// <summary>
        /// 用户名。
        /// <para>开头必须为字母或者数字。</para>
        /// <para>剩下支持字母、数字、下划线、英文点、减号、@</para>
        /// <para></para>
        /// </summary>
        [JsonProperty("username"]
        public string Username { get; set; }

        /// <summary>
        /// 密码。极光服务器会 MD5 加密后再保存。
        /// </summary>
        [JsonProperty("password"]
        public string Password { get; set; }

        /// <summary>
        /// 用户昵称。不支持 \n, \r。
        /// </summary>
        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        /// <summary>
        /// 用户头像。需要填上从文件上传接口返回的 media_id。
        /// </summary>
        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// 生日。格式为：yyyy-MM-dd。
        /// </summary>
        [JsonProperty("birthday")]
        public string Birthday { get; set; }

        /// <summary>
        /// 个性签名。
        /// </summary>
        [JsonProperty("signature")]
        public string Signature { get; set; }

        /// <summary>
        /// 性别。
        /// <para>0: 未知；1: 男；2: 女。</para>
        /// </summary>
        [JsonProperty("gender")]
        [DefaultValue(-1)]
        public int Gender { get; set; }

        /// <summary>
        /// 地区。
        /// </summary>
        [JsonProperty("region")]
        public string Region { get; set; }

        /// <summary>
        /// 详细地址。
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("extras")]
        public IDictionary Extras { get; set; }

        public override string ToString()
        {
            return GetJson();
        }

        private string GetJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
