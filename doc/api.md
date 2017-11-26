# API 列表

所有 API 的返回值均为 Jiguang.JMessage.Model.HttpResponse 对象，其中包含三个属性：

- StatusCode: System.Net.HttpStatusCode 类型，包含 Http 状态码。
- Headers: System.Net.Headers.HttpResponseHeaders 类型。
- Content: string 类型，当调用成功时为请求的返回结果，错误时为错误信息，格式均为 Json 字符串。

其中当 API 调用出错时，Content 可能会以如下格式返回 JMessage 业务错误码和错误信息，业务错误码的定义可以参考[官方文档](https://docs.jiguang.cn/jmessage/client/im_errorcode_server/)：

```json
{
  "error": {
    "code": 899008,
    "message": "Basic authentication failed"
  }
}
```

更多关于 API 的说明，还可参考[官网 REST API](https://docs.jiguang.cn/jmessage/server/rest_api_im/)。

## 敏感词相关 API

### AddSensitiveWords(List<string> wordList)

为当前应用添加敏感词。

#### 参数说明

- wordList: 敏感词字符串列表。每个词长度最多为 10 个字符，默认支持 100 个敏感词，有更多需求请访问官网联系商务。

#### 成功返回结果

调用成功返回时，状态码为 204 No Content。

#### 代码示例

```csharp
HttpResponse result = jmessageClient.AddSensitiveWords(new List<string>() { "xxx" });

if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
{
    // success
}
```

### UpdateSensitiveWord(string oldWord, string newWord)

更新已经设置的敏感词。

#### 参数说明

- oldWord: 已设置的敏感词。
- newWord: 将要替换旧敏感词的新词。

#### 成功返回结果

调用成功返回时，状态码为 204 No Content。

#### 代码示例

```csharp
HttpResponse result = jmessageClient.UpdateSensitiveWord("hello", "fxxk");;

if (result.StatusCode == System.Net.HttpStatusCode.NoContent)
{
    // success
}
```

### RemoveSensitiveWord(string word)

移除已设置的敏感词。

#### 参数说明。

- word: 需要被删除的敏感词。

### GetSensitiveWordsAsync(int start, int count)

获取已设置的敏感词列表。

#### 参数说明

- start: 开始序号，从 0 开始。
- count: 查询条数，最大为 2000。

#### 成功返回结果

- StatusCode: 200
- Content:

  ```json
  {
    "start": 2, // 从第三条开始查询
    "count": 1, // 查询一条
    "total": 3, // 敏感词总数
    "words": [
      {
        "name": "fuck",                 // 敏感词内容
        "itime": "2017-01-17 16:49:11"  // 添加日期
      }
    ]
  }
  ```

### SetSensitiveWordsStatus(bool enable)

设置是否开启敏感词功能。

#### 参数说明

- enable: true - 启用；false - 禁用。

#### 成功返回结果

- StatusCode: 204。

### GetSensitiveWordsStatus()

获取当前敏感词功能状态。

#### 成功返回结果

- StatusCode: 200
- Content:

  ```json
  {
    "status": 1 // 1: 启用；0：禁用。
  }
  ```