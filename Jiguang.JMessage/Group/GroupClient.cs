using Jiguang.JMessage.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JMessage.Group
{
    /// <summary>
    /// 群组相关 API。
    /// </summary>
    public class GroupClient
    {
        /// <summary>
        /// <see cref="CreateGroup(GroupInfo, List{string})"/>
        /// </summary>
        public async Task<HttpResponse> CreateGroupAsync(GroupInfo groupInfo, List<string> members)
        {
            JObject bodyJsonObj = JObject.FromObject(groupInfo);
            bodyJsonObj.Add("item", JArray.FromObject(members));

            HttpContent httpContent = new StringContent(bodyJsonObj.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(
                "/v1/groups/", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 创建群组。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#group"/></para>
        /// </summary>
        /// <param name="groupInfo">群组信息。</param>
        /// <param name="members">群成员的用户名列表。</param>
        public HttpResponse CreateGroup(GroupInfo groupInfo, List<string> members)
        {
            Task<HttpResponse> task = Task.Run(() => CreateGroupAsync(groupInfo, members));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetGroupInfo(long)"/>
        /// </summary>
        public async Task<HttpResponse> GetGroupInfoAsync(long groupId)
        {
            var url = $"/v1/groups/{groupId}";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取群组详情。
        /// </summary>
        /// <param name="groupId">群组ID。由创建群组时分配。</param>
        public HttpResponse GetGroupInfo(long groupId)
        {
            Task<HttpResponse> task = Task.Run(() => GetGroupInfoAsync(groupId));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="UpdateGroupInfo(GroupInfo)"/>
        /// </summary>
        public async Task<HttpResponse> UpdateGroupInfoAsync(GroupInfo groupInfo)
        {
            if (groupInfo == null)
                throw new ArgumentNullException(nameof(groupInfo));

            if (groupInfo.Id == -1)
                throw new ArgumentException(nameof(groupInfo.Id));

            var url = $"/v1/groups/{groupInfo.Id}";
            var httpContent = new StringContent(groupInfo.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 更新群组信息。
        /// </summary>
        /// <param name="groupId">待修改的群组 Id，会在创建时返回。</param>
        /// <param name="groupInfo">群组信息对象，可只设置需要修改的属性。</param>
        public HttpResponse UpdateGroupInfo(GroupInfo groupInfo)
        {
            Task<HttpResponse> task = Task.Run(() => UpdateGroupInfoAsync(groupInfo));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="DeleteGroup(string)"/>
        /// </summary>
        public async Task<HttpResponse> DeleteGroupAsync(long groupId)
        {
            var url = $"/v1/groups/{groupId}";
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 删除群组。该群组的所有成员都会收到群组被解散通知。
        /// </summary>
        /// <param name="groupId">待删除的群组 Id。</param>
        public HttpResponse DeleteGroup(long groupId)
        {
            Task<HttpResponse> task = Task.Run(() => DeleteGroupAsync(groupId));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> AddMembersAsync(long groupId, List<string> usernameList)
        {
            var url = $"/v1/groups/{groupId}/members";

            JObject body = new JObject
            {
                { "add", JArray.FromObject(usernameList) }
            };

            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 向群组中增加成员。
        /// </summary>
        /// <param name="groupId">群组 Id。</param>
        /// <param name="usernameList">用户名列表。</param>
        /// <returns>204 No Content, if success.</returns>
        public HttpResponse AddMembers(long groupId, List<string> usernameList)
        {
            Task<HttpResponse> task = Task.Run(() => AddMembersAsync(groupId, usernameList));
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> RemoveMembersAsync(long groupId, List<string> usernameList)
        {
            var url = $"/v1/groups/{groupId}/members";

            JObject body = new JObject
            {
                { "remove", JArray.FromObject(usernameList) }
            };

            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 移除群组中的成员。
        /// </summary>
        /// <param name="groupId">群组 Id。</param>
        /// <param name="usernameList">用户名列表。</param>
        public HttpResponse RemoveMembers(long groupId, List<string> usernameList)
        {
            Task<HttpResponse> task = Task.Run(() => RemoveMembersAsync(groupId, usernameList));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetMembers(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetMembersAsync(long groupId)
        {
            var url = $"/v1/groups/{groupId}/members";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取群组成员列表。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#group"/></para>
        /// </summary>
        /// <param name="groupId">群组 Id。</param>
        public HttpResponse GetMembers(long groupId)
        {
            Task<HttpResponse> task = Task.Run(() => GetMembersAsync(groupId));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetGroupList(int, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetGroupListAsync(int start, int count)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (count < 0 || count > 500)
                throw new ArgumentOutOfRangeException(nameof(count));

            var url = $"/v1/groups/?start={start}&count={count}";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取当前应用的群组列表。
        /// </summary>
        /// <param name="start">开始的记录数。</param>
        /// <param name="count">本次读取的记录数量。最大值为 500。</param>
        public HttpResponse GetGroupList(int start, int count)
        {
            Task<HttpResponse> task = Task.Run(() => GetGroupListAsync(start, count));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetGroupList(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetGroupListAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            var url = $"/v1/users/{username}/groups";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取某用户所属的群组列表。
        /// </summary>
        /// <param name="username">目标用户的用户名。</param>
        public HttpResponse GetGroupList(string username)
        {
            Task<HttpResponse> task = Task.Run(() => GetGroupListAsync(username));
            task.Wait();
            return task.Result;
        }

        // Cross App API - start

        /// <summary>
        /// <see cref="AddMembersCrossApp(long, List{string}, string)"/>
        /// </summary>
        public async Task<HttpResponse> AddMembersCrossAppAsync(long groupId, List<string> usernameList, string appKey)
        {
            var url = $"/v1/cross/groups/{groupId}/members";

            JObject body = new JObject
            {
                { "add",  JArray.FromObject(usernameList)}
            };
            body.Add("appkey", appKey);

            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 跨应用添加群组成员。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_38"/></para>
        /// </summary>
        /// <param name="groupId">需要添加成员的群组 Id。</param>
        /// <param name="usernameList">待添加的用户用户名列表。</param>
        /// <param name="appKey">待添加用户所属的 AppKey。</param>
        public HttpResponse AddMembersCrossApp(long groupId, List<string> usernameList, string appKey)
        {
            Task<HttpResponse> task = Task.Run(() => AddMembersCrossAppAsync(groupId, usernameList, appKey));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="RemoveMembersCrossApp(long, List{string}, string)"/>
        /// </summary>
        public async Task<HttpResponse> RemoveMembersCrossAppAsync(long groupId, List<string> usernameList, string appKey)
        {
            var url = $"/v1/cross/groups/{groupId}/members";

            JObject body = new JObject
            {
                { "remove",  JArray.FromObject(usernameList)}
            };
            body.Add("appkey", appKey);

            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 跨应用移除群组成员。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_38"/></para>
        /// </summary>
        /// <param name="groupId">需要移除成员的群组 Id。</param>
        /// <param name="usernameList">待移除的用户用户名列表。</param>
        /// <param name="appKey">待移除用户所属的 AppKey。</param>
        public HttpResponse RemoveMembersCrossApp(long groupId, List<string> usernameList, string appKey)
        {
            Task<HttpResponse> task = Task.Run(() => RemoveMembersCrossAppAsync(groupId, usernameList, appKey));
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetMembersCrossApp(long)"/>
        /// </summary>
        public async Task<HttpResponse> GetMembersCrossAppAsync(long groupId)
        {
            var url = $"/v1/cross/groups/{groupId}/members";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent("", Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取跨应用群组成员列表。
        /// </summary>
        /// <param name="groupId">目标群组 Id。</param>
        public HttpResponse GetMembersCrossApp(long groupId)
        {
            Task<HttpResponse> task = Task.Run(() => GetMembersCrossAppAsync(groupId));
            task.Wait();
            return task.Result;
        }

        // Cross App API - end
    }
}
