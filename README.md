# Visual Studio ChatGPT
- 这是一个直接在Visual Studio中添加chatGPT功能的扩展。
- 您将能够直接通过文本编辑器或通过新的特定工具窗口查阅 chatGPT。
 

- Visual Studio 2022: [插件地址](https://marketplace.visualstudio.com/items?itemName=MogoVsixRightMenu1.VisualStudioChatGpt) 

## 文本编辑器中的功能
- 选择一种方法并右键单击文本编辑器，您将看到这些新的chatGPT命令：
  
  ![image](https://images001.wangshuyu.top/Images001/2023/0531/165116479.png)

- **补全代码:** 开始编写方法，选择它并要求完成。
- **查找Bug:** 查找所选方法或代码片段的错误bugs。
- **修复bug:** 修改所选方法或代码片段的错误。
- **优化压缩代码:** 优化所选的代码的方法。
- **注释说明:** 编写所选方法的说明。 
- **添加方法Summary:** 添加 C# 方法的摘要Summary。- 
- **添加单元测试:** 为所选方法创建单元测试。
- **对话提问:** 直接跳转到chat.openai.com。
- **设置:** 设置OpenAI相关配置信息。

## 设置功能 
![image](https://images001.wangshuyu.top/Images001/2023/0531/164701930.png)
- 后端服务: 支持OpenAI和Azure云
- Api Url: 接口地址, **这个要和后端服务对应上, 因为OpenAI和Azure云验签方式有点小区别**
- Api Key: 验签的Key,如何申请 [OpenAI](https://beta.openai.com/account/api-keys)  [Azure云](https://learn.microsoft.com/azure/cognitive-services/openai/overview) 
- Proxy代理: 代理地址,格式:http://ip:port 例如: http://127.0.0.1:10809
- MaxToken: 做大返回的tokens数量,不用太大
- temperature: 热度,模型随机性,建议别超过0.3
- model: 模型,这个模型比达芬奇便宜,能力足够用, $0.002/750单词
- TimeOut(秒):超时时长


## 认证
### OpenAI申请Key
- 要使用此工具，必须注册OpenAI API, 并申请可以发者key。
- 您需要创建并设置一个OpenAI API密钥。 
- OpenAI申请key地址 [https://beta.openai.com/account/api-keys](https://beta.openai.com/account/api-keys)
- OpenAI官方提供了一个[状态页](https://status.openai.com/)，虽然小故障不怎么显示，但大面积宕机时能看到公告。
- 如果没有代理,可以考虑使用阿里云函数计算方式,透传OpenAI接口地址哦,[参考项目](https://github.com/dyc87112/OpenAIProxy)

### 微软Azure云申请Key
- 微软Azure云申请key地址: [https://learn.microsoft.com/azure/cognitive-services/openai/overview](https://learn.microsoft.com/azure/cognitive-services/openai/overview)
- 优点: 速度比OpenAI原生快至少一倍,不用搭建梯子
- 缺点: 申请比较麻烦,需要企业账户

### 说明
- OpenAI和微软Azure云申请一个即可

## 如何避免 OpenAI 封禁账号 API权限
- 不要使用 OpenAI 不服务地区的代理
- 虚拟海外手机号更可能导致账号被封
- 绑定信用卡可以大幅提升账号存活率

## 已知问题
- 不幸的是，OpenAI 提供的用于与 chatGPT 交互的 API 对问题的大小和给定的答案有限制。
- 如果发送的问题太长（例如，具有多行的方法）和/或生成的响应太长，API 可能会中断响应甚至根本不响应。
- 对于这些情况，我建议您通过工具窗口提出请求，以 chatGPT 不会拒绝回答的方式自定义问题，或尝试修改模型选项以改进响应

## 免责声明
- 由于此扩展取决于 OpenAI 提供的 API，因此它们可能会进行一些更改，从而影响此扩展的操作，恕不另行通知。
- 由于此扩展取决于 OpenAI 提供的 API，因此生成的响应可能不是预期的。
- 响应的速度和可用性直接取决于OpenAI提供的API。
- 如果您发现任何错误或意外行为，请发表评论，以便我提供修复。

## 参考
- 非常感谢作者的无私奉献： [https://github.com/jeffdapaz/VisualChatGPTStudio](https://github.com/jeffdapaz/VisualChatGPTStudio)

## 历史版本
### v0.2.1
- 更换插件唯一GUID,和其他插件重复了
- 新增微软Azure云
- 热度temperature参数,默认值调整成0
- 重构所有方法OpenAI前置话术和定语词,使返回结果更符合预期
- 去掉AddSummary(添加注释)功能,这个和Explain(注释说明)结果非常相似

### v0.1.3
- 初始化版本 基本功能支持
- 完成代码
- 查找bugs
- 修复bugs
- 优化代码
- 写说明
- 添加注释
- 添加方法柴窑
- 添加单元测试