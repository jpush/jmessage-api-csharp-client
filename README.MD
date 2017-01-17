# JMessage API client library for CSharp

## 概述
JMessage's officially supported CSharp client library for accessing JMessage APIs. 极光IM官方支持的 CSharp 版本服务器端 SDK。

## 安装
* 本地安装

  项目引用添加 jmessage.dll

## 使用

以用户注册为例

>一下代码节选自项目目录  /example/UserExamples/RegistUserExample.cs

```
            JMessageClient client = new JMessageClient(app_key, master_secret);
            UserPayload user = new UserPayload("jmessage123", "password");
            List<UserPayload> users = new List<UserPayload> { user };
            client._messageClient.registUser(users);
```

## REST 文档
>文档地址：https://docs.jiguang.cn/jmessage/server/rest_api_im/

## ErrorCode
> 文档地址：https://docs.jiguang.cn/jmessage/client/im_errorcode_server/
