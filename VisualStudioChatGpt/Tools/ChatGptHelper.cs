using Microsoft.VisualStudio.Package;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Collections;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using static VisualStudioChatGpt.MyBase;
using VisualStudioChatGpt.Commands;
using System.Windows.Forms;

namespace VisualStudioChatGpt.Tools
{
    internal class ChatGptHelper
    {
        /// <summary>
        /// 发起POST异步请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="ltcid"></param> 
        /// <param name="timeout">默认超时 5秒</param> 
        /// <returns></returns>
        public static async Task RestSharpPostAsync(string word, MyShowEventHandler showEvent, MyStartEventHandler startEvent, MyEndEventHandler endEvent)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                var config = MyConfig.Get();
                if (string.IsNullOrEmpty(config.apikey))
                {
                    MessageBox.Show("请设置OpenAI key");
                    FormSetUp window = new FormSetUp();
                    window.Show();
                    return;
                }

                var url = "https://api.openai.com/v1/chat/completions";
                var client = new RestClient(url);
                client.Proxy = new WebProxy(config.proxy);

                // 创建请求对象
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {config.apikey}");
                request.AddHeader("Content-Type", "application/json");
                request.Timeout = Convert.ToInt32(config.timeout) * 1000;

                request.AddJsonBody(new
                {
                    model = config.model,
                    temperature = Convert.ToDouble(config.temperature),
                    stream = true,
                    max_tokens = Convert.ToInt32(config.maxtoken),
                    messages = new List<object> { new { role = "user", content = word } }
                });

                // 执行请求
                var response = client.Execute(request);

                // 检查响应是否成功
                if (response.IsSuccessful)
                {
                    var responseStream = new MemoryStream(response.RawBytes);//获取响应内容流                
                    using (var streamReader = new StreamReader(responseStream))//创建流式读取器
                    {
                        startEvent.Invoke();//插入gpt结果
                        string line;
                        while ((line = streamReader.ReadLine()) != null)
                        {
                            if (!string.IsNullOrEmpty(line) && line.Contains("content"))
                            {
                                line = line.Remove(0, 5);
                                var obj = JsonConvert.DeserializeObject<dynamic>(line);
                                var temp = obj["choices"][0]["delta"]["content"].ToString();
                                await Task.Delay(1);
                                showEvent.Invoke(temp);//插入gpt结果
                            }
                        }
                        endEvent.Invoke();//插入gpt结果
                    }
                }
                else
                {
                    Console.WriteLine($"请求失败：{response.StatusCode} {response.ErrorMessage}");
                }
            });
        }
    }
}
