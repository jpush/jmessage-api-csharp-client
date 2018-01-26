using Jiguang.JMessage.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Jiguang.JMessage.User
{
    /// <summary>
    /// 用户相关 API。
    /// <<para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
    /// </summary>
    public class UserClient
    {
        /// <summary>
        /// <seealso cref="UserRegister(List{UserInfo})"/>
        /// </summary>
        public async Task<HttpResponse> RegisterAsync(List<UserInfo> userInfoList)
        {
            if (userInfoList == null)
                throw new ArgumentNullException(nameof(userInfoList));

            string json = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);
            HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/users", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 批量注册用户到极光 IM 服务器，一次批量注册最多支持 500 个用户。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_1"/></para>
        /// </summary>
        /// <param name="userInfoList">用户信息对象数组。</param>
        public HttpResponse Register(List<UserInfo> userInfoList)
        {
            Task<HttpResponse> task = RegisterAsync(userInfoList);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="AdminRegister(UserInfo)"/>
        /// </summary>
        public async Task<HttpResponse> RegisterAsAdminAsync(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentNullException(nameof(userInfo));

            HttpContent httpContent = new StringContent(userInfo.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/admins/", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 注册用户为管理员。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#admin"/></para>
        /// </summary>
        /// <param name="userInfo">待注册为管理员的用户信息对象。</param>
        public HttpResponse RegisterAsAdmin(UserInfo userInfo)
        {
            Task<HttpResponse> task = RegisterAsAdminAsync(userInfo);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="GetAdminList(int, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetAdminListAsync(int start, int count)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (count > 500)
                throw new ArgumentOutOfRangeException(nameof(count));

            var url = $"/v1/admins?start={start}&count={count}";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取管理员列表。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#admin"/></para>
        /// </summary>
        /// <param name="start">起始记录位置，从 0 开始。</param>
        /// <param name="count">查询条数，最多支持 500 条。</param>
        public HttpResponse GetAdminList(int start, int count)
        {
            Task<HttpResponse> task = GetAdminListAsync(start, count);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="GetUserList(int, int)"/>
        /// </summary>
        public async Task<HttpResponse> GetUserListAsync(int start, int count)
        {
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (count > 500)
                throw new ArgumentOutOfRangeException(nameof(count));

            var url = $"/v1/users?start={start}&count={count}";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取用户列表。
        /// </summary>
        /// <param name="start">起始记录位置，从 0 开始。</param>
        /// <param name="count">查询条数，最多支持 500 条。</param>
        public HttpResponse GetUserList(int start, int count)
        {
            Task<HttpResponse> task = GetUserListAsync(start, count);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="GetUserInfo(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetUserInfoAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException(nameof(username));

            var url = $"/v1/users/{username}";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取用户信息。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        public HttpResponse GetUserInfo(string username)
        {
            Task<HttpResponse> task = GetUserInfoAsync(username);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="UpdateUserInfo(UserInfo)"/>
        /// </summary>
        public async Task<HttpResponse> UpdateUserInfoAsync(UserInfo userInfo)
        {
            if (userInfo == null)
                throw new ArgumentNullException(nameof(userInfo));

            HttpContent httpContent = new StringContent(userInfo.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync($"/v1/users/{userInfo.Username}", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 更新用户信息。注意：该方法无法修改用户名和密码。
        /// <para>如果要修改密码，需要调用 <see cref="UpdatePassword(string, string)"/></para>
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="userInfo">更新后的用户信息对象。</param>
        public HttpResponse UpdateUserInfo(UserInfo userInfo)
        {
            Task<HttpResponse> task = UpdateUserInfoAsync(userInfo);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="CheckStatus(string)"/>
        /// </summary>
        public async Task<HttpResponse> CheckStatusAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException(username);

            var url = $"/v1/users/{username}/userstat";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 查询用户在线状态。该接口不适用于多端在线，多端在线请用批量状态接口。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="username">待查询用户的用户名。</param>
        public HttpResponse CheckStatus(string username)
        {
            Task<HttpResponse> task = CheckStatusAsync(username);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="CheckStatus(List{string})"/>
        /// </summary>
        public async Task<HttpResponse> CheckStatusAsync(List<string> usernameList)
        {
            if (usernameList == null)
                throw new ArgumentNullException(nameof(usernameList));

            string jsonStr = JsonConvert.SerializeObject(usernameList, Formatting.Indented);
            HttpContent httpContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync("/v1/users/userstat", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 批量查询用户在线状态。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="usernameList">待查询用户的用户名列表。</param>
        public HttpResponse CheckUserStatus(List<string> usernameList)
        {
            Task<HttpResponse> task = CheckStatusAsync(usernameList);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="UpdatePassword(string, string)"/>
        /// </summary>
        public async Task<HttpResponse> UpdatePasswordAsync(string username, string newPassword)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(username);

            if (string.IsNullOrEmpty(newPassword))
                throw new ArgumentNullException(newPassword);

            JObject jObject = new JObject
            {
                { "new_password", newPassword }
            };

            HttpContent httpContent = new StringContent(jObject.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync($"/v1/users/{username}/password", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 修改用户密码。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="username">待修改用户的用户名。</param>
        /// <param name="newPassword">新密码。</param>
        public HttpResponse UpdatePassword(string username, string newPassword)
        {
            Task<HttpResponse> task = UpdatePasswordAsync(username, newPassword);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="Delete(string)"/>
        /// </summary>
        public async Task<HttpResponse> DeleteAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(username);

            var url = $"/v1/users/{username}";
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 删除用户。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="username">待删除用户的用户名。</param>
        public HttpResponse Delete(string username)
        {
            Task<HttpResponse> task = DeleteAsync(username);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="Disable(string, bool)"/>
        /// </summary>
        public async Task<HttpResponse> DisableAsync(string username, bool isDisable)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            string url = $"/v1/users/{username}/forbidden?disable={isDisable}";
            HttpContent httpContent = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 禁用用户。
        /// </summary>
        /// <param name="username">目标用户用户名。</param>
        /// <param name="isDisable">true: 禁用；false：激活。</param>
        public HttpResponse Disable(string username, bool isDisable)
        {
            Task<HttpResponse> task = DisableAsync(username, isDisable);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="AddToBlackList(string, List{string})"/>
        /// </summary>
        public async Task<HttpResponse> AddToBlackListAsync(string username, List<string> targetUsernameList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (targetUsernameList == null)
                throw new ArgumentNullException(nameof(targetUsernameList));

            string jsonStr = JsonConvert.SerializeObject(targetUsernameList);
            HttpContent httpContent = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync($"/v1/users/{username}/blacklist", httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 添加用户到指定用户的黑名单。
        /// </summary>
        /// <param name="username">要添加黑名单的用户。</param>
        /// <param name="targetUsernameList">被添加到黑名单中的用户名列表。</param>
        public HttpResponse AddToBlackList(string username, List<string> targetUsernameList)
        {
            Task<HttpResponse> task = AddToBlackListAsync(username, targetUsernameList);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="RemoveFromBlackList(string, List{string})"/>
        /// </summary>
        public async Task<HttpResponse> RemoveFromBlackListAsync(string username, List<string> targetUsernameList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (targetUsernameList == null)
                throw new ArgumentNullException(nameof(targetUsernameList));

            string jsonStr = JsonConvert.SerializeObject(targetUsernameList, Formatting.Indented);

            var url = $"/v1/users/{username}/blacklist";
            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(jsonStr, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 将用户从黑名单中移除。
        /// </summary>
        /// <param name="username">需要移除黑名单用户的用户名。</param>
        /// <param name="targetUsernameList">被移除用户的用户名列表。</param>
        public HttpResponse RemoveFromBlackList(string username, List<string> targetUsernameList)
        {
            Task<HttpResponse> task = RemoveFromBlackListAsync(username, targetUsernameList);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="GetBlackList(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetBlackListAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            string url = $"/v1/users/{username}/blacklist";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取指定用户的黑名单列表。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_3"/></para>
        /// </summary>
        /// <param name="username">需要获取黑名单用户的用户名。</param>
        public HttpResponse GetBlackList(string username)
        {
            Task<HttpResponse> task = GetBlackListAsync(username);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="SetUserNoDisturbAsync(string, List{string}, bool)"/>
        /// </summary>
        public async Task<HttpResponse> SetUserNoDisturbAsync(string username, List<string> targetUsernameList, bool enable)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (targetUsernameList == null)
                throw new ArgumentNullException(nameof(targetUsernameList));

            JObject body = new JObject();
            JObject single = new JObject();

            if (enable)
            {
                single.Add("add", JArray.FromObject(targetUsernameList));
            }
            else
            {
                single.Add("remove", JArray.FromObject(targetUsernameList));
            }

            body.Add("single", single);

            string url = $"/v1/users/{username}/nodisturb";
            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 对指定用户设置免打扰。
        /// </summary>
        /// <param name="username">需要设置免打扰用户的用户名。</param>
        /// <param name="targetUsernameList">需要被设置为免打扰的目标用户用户名列表。</param>
        /// <param name="enable">true: 设置为免打扰；false: 解除免打扰。</param>
        public HttpResponse SetUserNoDisturb(string username, List<string> targetUsernameList, bool enable)
        {
            Task<HttpResponse> task = SetUserNoDisturbAsync(username, targetUsernameList, enable);
            task.Wait();
            return task.Result;
        }


        /// <summary>
        /// <seealso cref="SetGroupNoDisturbAsync(string, List{string}, bool)"/>
        /// </summary>
        public async Task<HttpResponse> SetGroupNoDisturbAsync(string username, List<int> targetGroupIdList, bool enable)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (targetGroupIdList == null)
                throw new ArgumentNullException(nameof(targetGroupIdList));

            JObject body = new JObject();
            JObject group = new JObject();

            if (enable)
            {
                group.Add("add", JArray.FromObject(targetGroupIdList));
            }
            else
            {
                group.Add("remove", JArray.FromObject(targetGroupIdList));
            }

            body.Add("single", group);

            string url = $"/v1/users/{username}/nodisturb";
            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 对指定群组设置免打扰。
        /// </summary>
        /// <param name="username">需要设置免打扰的用户用户名。</param>
        /// <param name="targetGroupIdList">需要被设置为免打扰的群组 Id 列表。</param>
        /// <param name="enable">true: 设置为免打扰；false: 解除免打扰。</param>
        public HttpResponse SetGroupNoDisturb(string username, List<int> targetGroupIdList, bool enable)
        {
            Task<HttpResponse> task = SetGroupNoDisturbAsync(username, targetGroupIdList, enable);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <seealso cref="SetGlobalNoDisturb(string, bool)"/>
        /// </summary>
        public async Task<HttpResponse> SetGlobalNoDisturbAsync(string username, bool enable)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            JObject body = new JObject();
            int global = enable ? 1 : 0;
            body.Add("global", global);

            string url = $"/v1/users/{username}/nodisturb";
            HttpContent httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 为指定用户设置全局免打扰。
        /// </summary>
        /// <param name="username">需要设置免打扰用户的用户名。</param>
        /// <param name="enable">true: 开启免打扰；false: 关闭免打扰。</param>
        public HttpResponse SetGlobalNoDisturb(string username, bool enable)
        {
            Task<HttpResponse> task = SetGlobalNoDisturbAsync(username, enable);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="BlockGroup(string, List{long})"/>
        /// </summary>
        public async Task<HttpResponse> BlockGroupAsync(string username, List<long> groupIdList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (groupIdList == null)
                throw new ArgumentNullException(nameof(groupIdList));

            var url = $"/v1/users/{username}/groupsShield";

            JObject body = new JObject
            {
                { "add", JArray.FromObject(groupIdList) }
            };

            var httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 群消息屏蔽。
        /// </summary>
        /// <param name="username">需要屏蔽群消息的用户。</param>
        /// <param name="groupIdList">被屏蔽的群组 Id 列表。</param>
        public HttpResponse BlockGroup(string username, List<long> groupIdList)
        {
            Task<HttpResponse> task = BlockGroupAsync(username, groupIdList);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="UnblockGroup(string, List{long})"/>
        /// </summary>
        public async Task<HttpResponse> UnblockGroupAsync(string username, List<long> groupIdList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (groupIdList == null)
                throw new ArgumentNullException(nameof(groupIdList));

            var url = $"/v1/users/{username}/groupsShield";

            JObject body = new JObject
            {
                { "remove", JArray.FromObject(groupIdList) }
            };

            var httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 解除群消息屏蔽。
        /// </summary>
        /// <param name="username">需要解除屏蔽群消息的用户。</param>
        /// <param name="groupIdList">需要解除被屏蔽的群组 Id 列表。</param>
        public HttpResponse UnblockGroup(string username, List<long> groupIdList)
        {
            Task<HttpResponse> task = UnblockGroupAsync(username, groupIdList);
            task.Wait();
            return task.Result;
        }

        // Friend API - start

        public async Task<HttpResponse> AddFriendsAsync(string username, List<string> friendUsernameList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (friendUsernameList == null)
                throw new ArgumentNullException(nameof(friendUsernameList));

            var url = $"/v1/users/{username}/friends";

            var body = JArray.FromObject(friendUsernameList);
            var httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 添加好友。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_33"/></para>
        /// </summary>
        /// <param name="username">需要添加好友的用户用户名。</param>
        /// <param name="friendUsernameList">待添加的用户名列表（最多 500 个）。</param>
        public HttpResponse AddFriends(string username, List<string> friendUsernameList)
        {
            Task<HttpResponse> task = AddFriendsAsync(username, friendUsernameList);
            task.Wait();
            return task.Result;
        }

        public async Task<HttpResponse> RemoveFriendsAsync(string username, List<string> friendUsernameList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (friendUsernameList == null)
                throw new ArgumentNullException(nameof(friendUsernameList));

            var body = JArray.FromObject(friendUsernameList);
            var url = $"/v1/users/{username}/friends";

            var request = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 删除好友。
        /// <para><see cref="https://docs.jiguang.cn/jmessage/server/rest_api_im/#_33"/></para>
        /// </summary>
        /// <param name="username">需要删除好友的用户用户名。</param>
        /// <param name="friendUsernameList">待删除的用户名列表（最多 500 个）。</param>
        public HttpResponse RemoveFriends(string username, List<string> friendUsernameList)
        {
            Task<HttpResponse> task = RemoveFriendsAsync(username, friendUsernameList);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetFriendList(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetFriendListAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            var url = $"/v1/users/{username}/friends";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取好友列表。
        /// </summary>
        /// <param name="username">待获取好友列表的用户名。</param>
        public HttpResponse GetFriendList(string username)
        {
            Task<HttpResponse> task = GetFriendListAsync(username);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="UpdateFriendNoteInfo(string, string)"/>
        /// </summary>
        public async Task<HttpResponse> UpdateFriendNoteInfoAsync(string username, string noteInfoJsonStr)
        {
            if (string.IsNullOrEmpty(nameof(username)))
                throw new ArgumentNullException(nameof(username));

            if (string.IsNullOrEmpty(noteInfoJsonStr))
                throw new ArgumentNullException(nameof(noteInfoJsonStr));

            var url = $"/v1/users/{username}/friends";

            var httpContent = new StringContent(noteInfoJsonStr, Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 更新好友备注信息。
        /// </summary>
        /// <param name="username">待更新好友的用户用户名。</param>
        /// <param name="noteInfoJsonStr">
        ///     好友备注信息的 Json 字符串。格式为：
        ///     <para>
        ///     [
        ///         {
        ///             "username": "好友用户名",
        ///             "note_name": "好友新备注名",
        ///             "others": "好友新备注信息"
        ///         }
        ///     ]    
        /// </para>
        /// </param>
        public HttpResponse UpdateFriendNoteInfo(string username, string noteInfoJsonStr)
        {
            Task<HttpResponse> task = UpdateFriendNoteInfoAsync(username, noteInfoJsonStr);
            task.Wait();
            return task.Result;
        }

        // Friend API - end

        // Cross API - start

        public async Task<HttpResponse> GetGroupsCrossAppAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            var url = $"/v1/cross/users/{username}/groups";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// <see cref="AddToBlackListCrossApp(string, List{string})"/>
        /// </summary>
        public async Task<HttpResponse> AddToBlackListCrossAppAsync(string username, List<string> usernameList)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (usernameList == null)
                throw new ArgumentNullException(nameof(usernameList));

            var url = $"/v1/cross/users/{username}/blacklist";

            JArray body = JArray.FromObject(usernameList);
            var httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PutAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 添加跨应用用户到黑名单。
        /// </summary>
        /// <param name="username">需要添加用户到黑名单的用户用户名。</param>
        /// <param name="usernameList">被添加的用户用户名列表。</param>
        public HttpResponse AddToBlackListCrossApp(string username, List<string> usernameList)
        {
            Task<HttpResponse> task = AddToBlackListCrossAppAsync(username, usernameList);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="RemoveFromBlackListCrossApp(string, List{string}, string)"/>
        /// </summary>
        public async Task<HttpResponse> RemoveFromBlackListCrossAppAsync(string username, List<string> usernameList, string appKey)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (usernameList == null)
                throw new ArgumentNullException(nameof(usernameList));

            JObject body = new JObject
            {
                { "appkey", appKey },
                { "usernames", JArray.FromObject(usernameList) }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"/v1/cross/users/{username}/blacklist"),
                Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 从黑名单中移除跨应用用户。
        /// </summary>
        /// <param name="username">需要移除黑名单用户的用户名。</param>
        /// <param name="usernameList">待移除的用户用户名列表。</param>
        /// <param name="appKey">待移除用户所属应用的 AppKey。</param>
        public HttpResponse RemoveFromBlackListCrossApp(string username, List<string> usernameList, string appKey)
        {
            Task<HttpResponse> task = RemoveFromBlackListCrossAppAsync(username, usernameList, appKey);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="GetBlackListCrossApp(string)"/>
        /// </summary>
        public async Task<HttpResponse> GetBlackListCrossAppAsync(string username)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            var url = $"/v1/cross/users/{username}/blacklist";
            var request = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 获取跨应用黑名单用户列表。
        /// </summary>
        /// <param name="username">待查询跨应用黑名单的用户用户名。</param>
        public HttpResponse GetBlackListCrossApp(string username)
        {
            Task<HttpResponse> task = GetBlackListCrossAppAsync(username);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="SetUserNoDisturbCrossApp(string, List{string}, string, bool)"/>
        /// </summary>
        public async Task<HttpResponse> SetUserNoDisturbCrossAppAsync(string username, List<string> usernameList, string appKey, bool isNoDisturb)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (usernameList == null)
                throw new ArgumentNullException(nameof(usernameList));

            if (string.IsNullOrEmpty(appKey))
                throw new ArgumentNullException(nameof(appKey));

            var url = $"/v1/cross/users/{username}/nodisturb";

            JArray body = new JArray
            {
                new JObject
                {
                    { "appkey", appKey },
                    { "single", new JObject
                        {
                            new JArray{ "add", JArray.FromObject(usernameList) }
                        }
                    }
                }
            };

            var httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 设置跨应用用户免打扰。
        /// </summary>
        /// <param name="username">需要设置免打扰的用户。</param>
        /// <param name="usernameList">被设置为免打扰的用户用户名列表。</param>
        /// <param name="appKey">被设置为免打扰用户所属应用的 AppKey。</param>
        /// <param name="isNoDisturb">是否设置为免打扰。true: 设置免打扰；false: 取消免打扰。</param>
        public HttpResponse SetUserNoDisturbCrossApp(string username, List<string> usernameList, string appKey, bool isNoDisturb)
        {
            Task<HttpResponse> task = SetUserNoDisturbCrossAppAsync(username, usernameList, appKey, isNoDisturb);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="AddFriendsCrossApp(string, List{string}, string)"/>
        /// </summary>
        public async Task<HttpResponse> AddFriendsCrossAppAsync(string username, List<string> usernameList, string appKey)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (usernameList == null)
                throw new ArgumentNullException(nameof(usernameList));

            if (string.IsNullOrEmpty(appKey))
                throw new ArgumentNullException(nameof(appKey));

            var url = $"/v1/cross/users/{username}/friends";

            JObject body = new JObject
            {
                { "appkey", appKey },
                { "users", JArray.FromObject(usernameList) }
            };

            var httpContent = new StringContent(body.ToString(), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.PostAsync(url, httpContent).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 跨应用添加好友。
        /// </summary>
        /// <param name="username">需要添加好友的用户用户名。</param>
        /// <param name="usernameList">待添加的用户用户名列表。</param>
        /// <param name="appKey">待添加用户所属应用的 AppKey。</param>
        public HttpResponse AddFriendsCrossApp(string username, List<string> usernameList, string appKey)
        {
            Task<HttpResponse> task = AddFriendsCrossAppAsync(username, usernameList, appKey);
            task.Wait();
            return task.Result;
        }

        /// <summary>
        /// <see cref="RemoveFriendsCrossApp(string, List{string}, string)"/>
        /// </summary>
        public async Task<HttpResponse> RemovedFriendsCrossAppAsync(string username, List<string> usernameList, string appKey)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (usernameList == null)
                throw new ArgumentNullException(nameof(usernameList));

            if (string.IsNullOrEmpty(appKey))
                throw new ArgumentNullException(nameof(appKey));

            JObject body = new JObject
            {
                { "appkey", appKey },
                { "users", JArray.FromObject(usernameList) }
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"/v1/cross/users/{username}/friends"),
                Content = new StringContent(body.ToString(), Encoding.UTF8, "application/json")
            };

            HttpResponseMessage httpResponseMessage = await JMessageClient.HttpClient.SendAsync(request).ConfigureAwait(false);
            string httpResponseContent = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            return new HttpResponse(httpResponseMessage.StatusCode, httpResponseMessage.Headers, httpResponseContent);
        }

        /// <summary>
        /// 跨应用删除好友。
        /// </summary>
        /// <param name="username">需要删除好友的用户用户名。</param>
        /// <param name="usernameList">待删除用户的用户名列表。</param>
        /// <param name="appKey">待删除用户所属应用的 AppKey。</param>
        public HttpResponse RemoveFriendsCrossApp(string username, List<string> usernameList, string appKey)
        {
            Task<HttpResponse> task = AddFriendsCrossAppAsync(username, usernameList, appKey);
            task.Wait();
            return task.Result;
        }

        // Cross API - end
    }
}
