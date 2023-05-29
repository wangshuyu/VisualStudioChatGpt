using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.ComponentModel.Design;
using System.Globalization;
using Task = System.Threading.Tasks.Task;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.ComponentModelHost;
using System.Windows.Forms;
using System.Collections.Generic;
using VisualStudioChatGpt.Commands;
using Newtonsoft.Json.Linq;
using VisualStudioChatGpt.Model;
using Newtonsoft.Json;
using RestSharp;
using System.IO;
using System.Net;

namespace VisualStudioChatGpt.Commands
{
    public class MyBase
    {

        // 定义事件处理器的委托类型
        public delegate void MyShowEventHandler(string message);
        public delegate void MyStartEventHandler();
        public delegate void MyEndEventHandler();

        /// <summary>
        /// 插入节点对象
        /// </summary>
        public EditPoint insertPoint;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("a8eb0343-b35c-4175-95d8-50549e609ff3");


        public Dictionary<string, string> dict = new Dictionary<string, string>();
        public MyBase()
        {

        }

        /// <summary>
        /// 添加自定义事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="handler"></param>
        /// <param name="commandService"></param>
        public void AddEvent(int id, EventHandler handler, OleMenuCommandService commandService)
        {
            var _obj = new CommandID(CommandSet, id);
            var menuItem = new MenuCommand(handler, _obj);
            commandService.AddCommand(menuItem);

        }

        #region 获取选中内容

        /// <summary>
        /// 获取选中内容
        /// </summary>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<string> GetSelectedTextAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            var ret = "";

            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            TextSelection selection = dte.ActiveDocument.Selection as TextSelection;

            if (selection != null && !selection.IsEmpty)
            {
                ret = selection.Text;
            }
            if (string.IsNullOrEmpty(ret))
            {
                MessageBox.Show(TypeModel.message);
            }
            return ret;
        }
        #endregion 

        #region 选中代码后面插入内容

        /// <summary>
        /// 选中代码后面插入内容
        /// </summary>
        /// <param name="preValue">预先插入值</param>
        /// <param name="content">chatgpt搜索文本内容</param>
        /// <param name="position">插入位置</param>
        /// <returns></returns>
        public async Task InsertChatGptAsync(string content, InsertPointEnum position)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            TextSelection selection = dte.ActiveDocument.Selection as TextSelection;

            if (selection != null && !selection.IsEmpty)
            {
                if (position == InsertPointEnum.Before)//代码前面
                {
                    this.insertPoint = selection.TopPoint.CreateEditPoint();
                }
                else if (position == InsertPointEnum.After)//代码后面
                {
                    this.insertPoint = selection.BottomPoint.CreateEditPoint();
                }
                else if (position == InsertPointEnum.Replace)//注释当前代码 并在后面插入
                {
                    var _insertPoint = selection.TopPoint.CreateEditPoint();
                    _insertPoint.Insert("/*\r\n");

                    _insertPoint = selection.BottomPoint.CreateEditPoint();
                    _insertPoint.Insert("\r\n*/\r\n");

                    this.insertPoint = selection.BottomPoint.CreateEditPoint();
                }
                // 添加事件处理器
                _ = OpenAiAsync(content, VirShowMessage, VirStart, VirEnd);
            }
        }
        #endregion

        #region 插入常量

        /// <summary>
        /// 插入常量
        /// </summary>
        /// <param name="content"></param>
        public async Task InsertConstAsync(string content, InsertPointEnum position)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            TextSelection selection = dte.ActiveDocument.Selection as TextSelection;

            if (selection != null && !selection.IsEmpty)
            {
                if (position == InsertPointEnum.Before)//代码前面
                {
                    var _insertPoint = selection.TopPoint.CreateEditPoint();
                    _insertPoint.Insert(content);
                }
                else if (position == InsertPointEnum.After)//代码后面
                {
                    var _insertPoint = selection.BottomPoint.CreateEditPoint();
                    _insertPoint.Insert(content);
                }
                else if (position == InsertPointEnum.Replace)//注释当前代码 并在后面插入
                {
                    var _insertPoint = selection.TopPoint.CreateEditPoint();
                    _insertPoint.Insert("/*\n");

                    _insertPoint = selection.BottomPoint.CreateEditPoint();
                    _insertPoint.Insert("\n*/\n");

                    _insertPoint = selection.BottomPoint.CreateEditPoint();
                    _insertPoint.Insert(content);
                }
            }
        }
        #endregion

        #region 格式化代码

        /// <summary>
        /// 格式化代码
        /// </summary>
        public async Task SimulateCtrlKCtrlDAsync()
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();
            DTE dte = (DTE)Package.GetGlobalService(typeof(DTE));
            dte.ExecuteCommand("Edit.FormatDocument");
        }
        #endregion

        #region 请求OpenAI 获取数据

        /// <summary>
        /// 请求OpenAI 获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="ltcid"></param> 
        /// <param name="timeout">默认超时 5秒</param> 
        /// <returns></returns>
        public static async Task OpenAiAsync(string word, MyShowEventHandler showEvent, MyStartEventHandler startEvent, MyEndEventHandler endEvent)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                var config = MyConfig.Get();
                if (string.IsNullOrEmpty(config.apikey))
                {
                    MessageBox.Show("请设置OpenAI key");

                    var form = new FormSetUp();
                    form.StartPosition = FormStartPosition.CenterScreen;
                    form.Show();
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
        #endregion

        #region 虚方法

        /// <summary>
        /// 自定义处理事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual async void VirHandler(object sender, EventArgs e)
        {
            MessageBox.Show("未实现此功能!");
        }

        /// <summary>
        /// 插入代码
        /// </summary>
        /// <param name="message"></param>
        public virtual void VirShowMessage(string message)
        {
            if (this.insertPoint != null)
            {
                this.insertPoint.Insert(message);
            }
        }

        /// <summary>
        /// 执行开始
        /// </summary>
        /// <param name="message"></param>
        public virtual void VirStart()
        {
        }

        /// <summary>
        /// 执行完成
        /// </summary>
        /// <param name="message"></param>
        public virtual void VirEnd()
        {
        }
        #endregion
    }
}
