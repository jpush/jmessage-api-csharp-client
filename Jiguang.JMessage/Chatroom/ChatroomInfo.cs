using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jiguang.JMessage.Chatroom
{
    /// <summary>
    /// 聊天室信息。
    /// </summary>
    public class ChatroomInfo
    {
        public long? Id { get; set; } 

        /// <summary>
        /// 聊天室名称（必填）。
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 聊天室创建者用户名（必填）。
        /// </summary>
        [JsonProperty("owner_username")]
        public string Owner { get; set; }

        /// <summary>
        /// 聊天室描述。
        /// </summary>
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        /// <summary>
        /// 成员用户名列表。
        /// </summary>
        [JsonProperty("members_username", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MemberList { get; set; }

        /// <summary>
        /// 创建时间。形式为 "yyyy-MM-dd hh:mm:ss"
        /// </summary>
        [JsonProperty("ctime", NullValueHandling = NullValueHandling.Ignore)]
        public string CreateTime { get; set; }

        /// <summary>
        /// 禁言标识。0 - 不禁言；1 - 禁言。
        /// </summary>
        [JsonProperty("flag", NullValueHandling = NullValueHandling.Ignore)]
        public int? Flag { get; set; }

        /// <summary>
        /// 最大成员数。
        /// </summary>
        [JsonProperty("max_member_count", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxMemberCount { get; set; }

        /// <summary>
        /// 当前成员数。
        /// </summary>
        [JsonProperty("total_member_count", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentMemberCount { get; set; }

        public override string ToString()
        {
            return GetJson();
        }

        private string GetJson()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
