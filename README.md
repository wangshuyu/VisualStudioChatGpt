# Visual Studio ChatGPT

这是一个直接在Visual Studio中添加chatGPT功能的扩展。

您将能够直接通过文本编辑器或通过新的特定工具窗口查阅 chatGPT。
 

- Visual Studio 2022: [点击跳转vs2022扩展插件](https://www.baidu.com/) 

## 文本编辑器中的功能

选择一种方法并右键单击文本编辑器，您将看到这些新的chatGPT命令：


![image](https://cdn.jsdelivr.net/gh/wangshuyu/Images001@main/2023/0529/121447749.png)

- **完成代码:** 开始编写方法，选择它并要求完成。
- **查找Bug:** 查找所选方法或代码片段的错误bugs。
- **修复bug:** 修改所选方法或代码片段的错误。
- **优化代码:** 优化所选的代码的方法。
- **为当前代码写说明:** 编写所选方法的说明。
- **添加注释:** 为所选方法添加注释。
- **添加方法摘要Summary:** 添加 C# 方法的摘要Summary。- 
- **添加单元测试:** 为所选方法创建单元测试。
- **对话提问:** 直接跳转到chat.openai.com。
- **设置:** 设置OpenAI相关配置信息。

## 设置功能
![image](https://cdn.jsdelivr.net/gh/wangshuyu/Images001@main/2023/0529/122145895.png)

## 认证
- 要使用此工具，必须注册OpenAI API, 并申请可以发者key。
- 您需要创建并设置一个OpenAI API密钥。 
- 您可以在此处执行此操作： [https://beta.openai.com/account/api-keys](https://beta.openai.com/account/api-keys)

## 已知问题
- 不幸的是，OpenAI 提供的用于与 chatGPT 交互的 API 对问题的大小和给定的答案有限制。
- 如果发送的问题太长（例如，具有多行的方法）和/或生成的响应太长，API 可能会中断响应甚至根本不响应。
- 对于这些情况，我建议您通过工具窗口提出请求，以 chatGPT 不会拒绝回答的方式自定义问题，或尝试修改模型选项以改进响应

## 已知问题
- 由于此扩展取决于 OpenAI 提供的 API，因此它们可能会进行一些更改，从而影响此扩展的操作，恕不另行通知。
- 由于此扩展取决于 OpenAI 提供的 API，因此生成的响应可能不是预期的。
- 响应的速度和可用性直接取决于OpenAI提供的API。
- 如果您发现任何错误或意外行为，请发表评论，以便我提供修复。

## 参考
- 非常感谢作者的无私奉献： [https://github.com/jeffdapaz/VisualChatGPTStudio](https://github.com/jeffdapaz/VisualChatGPTStudio)

## 历史版本
### 0.1
- 初始化版本 基本功能支持
- 完成代码
- 查找bugs
- 修复bugs
- 优化代码
- 写说明
- 添加注释
- 添加方法柴窑
- 添加单元测试