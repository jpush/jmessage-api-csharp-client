# Models

## UserInfo

- Username: 用户名。开头必须为字母或者数字，剩下的支持字母、数字、下划线、英文点、减号和 @。长度限制：Byte (4~128)。
- Password: 密码。极光服务器会进行 MD5 加密后再保存。长度限制：Byte (4~128)。
- Nickname: 昵称。不支持 "\n" 和 "\r" 字符，长度限制：Byte (0~64)。
- Avatar: 用户头像。从文件上传接口获得的 media_id。
- Birthday: 生日。格式为：yyyy-MM-dd。
- Signature: 个性签名。支持包括 Emoji 的全部字符。长度限制：Byte (0~250)。
- Gender: 性别。0 - 未知，1 - 男性，2 - 女性。
- Region: 地区。支持包括 Emoji 的全部字符。长度限制：Byte (0~250)。
- Address: 详细地址。支持包括 Emoji 的全部字符。长度限制：Byte (0~250)。
- Extras: 用户自定义键值对。长度限制：Byte (0~512)。

## GroupInfo

- Id: 群组 Id。可在调用创建群组 API 后获得。
- Name: 群组名称。支持包括 Emoji 在内的全部字符。长度限制：Byte (0~64)。
- Description: 群组描述。支持包括 Emoji 在内的全部字符。长度限制：Byte (0~250)。
- Owner: 群主的用户名。
- MaxMemberCount: 最大成员人数，默认为 500。
- CreateTime: 创建时间。格式为：yyyy-MM-dd hh:mm:ss。
- LastModifyiedTime: 最后修改时间。格式为：yyyy-MM-dd hh:mm:ss。
- Avatar: 群组头像。为通过文件上传接口获得的 media_id。

## Message

以下为所有类型消息共有的属性：

- TargetType（必填）: 发送目标类型。目前支持 "single"（个人）和 "group"（"群组"）。
- TargetId（必填）：目标唯一标识。当 TargetType 为 "single" 时填 username，为 "group" 时填 Group Id。
- TargetAppKey（选填）: 跨应用目标所属应用的 AppKey。
- FromId（必填）：消息发送者的 username。
- TargetName（选填）: 消息接收方将被展示的名称。
- FromName（选填）: 消息发送者将被展示的名称。
- isNoOffline（选填）: 消息是否需要离线存储。true - 不需要；false - 需要。
- isNoNotification（选填）: 消息是否会在通知栏展示。true - 不会；false - 会。

### TextMessage

- MessageBody: 消息体
  - Text（必填）: 消息内容。
  - Extras（选填）: 自定义键值对。

### ImageMessage

- MessageBody: 消息体
  - MediaId（必填）: 由调用文件上传接口之后服务器端所返回，用于之后生成下载的 URL。
  - MediaCrc32（必填）: 文件的crc32校验码，用于下载文件的校验。
  - Width（必填）：图片原始宽度。
  - Height（必填）：图片原始高度。
  - Format（必填）：图片格式。
  - Size（必填）：文件的字节数。
  - Hash（选填）：图片的 Hash 值。

### VoiceMessage

- MessageBody:
  - MediaId（必填）: 由调用文件上传接口之后服务器端所返回，用于之后生成下载的 URL。
  - MediaCrc32（必填）: 文件的crc32校验码，用于下载文件的校验。
  - Duration（必填）：音频时长，单位秒。
  - Size（必填）：文件的字节数。
  - Hash（选填）：文件的 Hash 值。

### CustomMessage

收到自定义消息不会不会有通知栏展示。

- MessageBody:
  - Extras: 自定义键值对。