using Newtonsoft.Json;
using System.ComponentModel;

namespace Jiguang.JMessage.Group
{
    /// <summary>
    /// 群组信息。
    /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#group"/></para>
    /// </summary>
    public class GroupInfo
    {
        /// <summary>
        /// 群组 Id。
        /// </summary>
        [JsonIgnore]
        public long Id { get; set; } = -1;

        /// <summary>
        /// 群组名称。
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 群组描述。
        /// </summary>
        [JsonProperty("desc", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// 群主的用户名。
        /// </summary>
        [JsonProperty("owner_username")]
        public string Owner { get; set; }

        /// <summary>
        /// 最大成员人数，默认 500 人。
        /// </summary>
        [JsonProperty("MaxMemberCount", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxMemberCount { get; set; }

        /// <summary>
        /// 创建时间。
        /// </summary>
        [JsonProperty("ctime", NullValueHandling = NullValueHandling.Ignore)]
        public string CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间。
        /// </summary>
        [JsonProperty("mtime", NullValueHandling = NullValueHandling.Ignore)]
        public string LastModifiedTime { get; set; }

        /// <summary>
        /// 群组头像。
        /// </summary>
        [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
        public string Avatar { get; set; }

        public override string ToString()
        {
            return GetJson();
        }

        private string GetJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
        }
    }
}
