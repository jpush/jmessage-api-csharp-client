# 用户相关 API 列表

以下 API 均通过 JMessageClient.User 进行调用。

## 注册与属性维护

### Register(List<UserInfo> userInfoList)

批量注册用户到极光服务器，一次批量注册最多支持 500 个用户。

#### 参数说明

- userInfoList：UserInfo 对象数组。其中每个 UserInfo 对象的 Username 和 Password 属性必须设置，其余为选填。

#### 成功返回结果

- StatusCode: 201 Created。
- Content: Json Array 字符串。

  ```json
  [
    {"username": "user1"}
  ]
  ```

### RegisterAsAdmin(UserInfo userInfo)

注册用户为管理员。

#### 参数说明

- userInfo：UserInfo 对象。其中 Username 和 Password 属性为必填。

#### 成功返回结果

- StatusCode: 201 Created。

#### GetAdminList(int start, int count)

获得管理员列表。

#### 参数说明

- start: 记录起始位置 从 0 开始。
- count: 查询条数，最多支持 500 条。

#### 成功返回结果

- StatusCode: 200 OK。
- Content: Json 对象字符串：
  ```json
  {
    "total":1,
    "start":0,
    "count":1,
    "users":
      [
        {
          "username" : "cai",
          "nickname" : "hello",
          "mtime" : "2015-01-01 00:00:00",  // 最后修改时间
          "ctime" : "2015-01-01 00:00:00"   // 创建时间
        }
      ]
  }
  ```

### GetUserList(int start, int count)

获取用户列表。

#### 参数说明

- start: 记录起始位置 从 0 开始。
- count: 查询条数，最多支持 500 条。

#### 成功返回结果

- StatusCode: 200 OK。
- Content: Json 对象字符串：

### GetUserInfo(string username)

获取用户信息。

#### 参数说明

- username: 待获取信息的用户用户名。

#### 成功返回结果

- StatusCode: 200 OK。
- Content: 其中除了 `username`, `mtime` 和 `ctime` 三个属性必定存在意外，其余属性如果未设置就没有相应的 key。
  ```json
  {
    "username" : "user1",
    "nickname" : "hello",
    "avatar" : "/avatar",
    "birthday" : "1990-01-24 00:00:00",
    "gender" : 0,
    "signature" : "orz",
    "region" : "shenzhen",
    "address" : "shenzhen",
    "mtime" : "2015-01-01 00:00:00",
    "ctime" : "2015-01-01 00:00:00"
  }
  ```

### 黑名单