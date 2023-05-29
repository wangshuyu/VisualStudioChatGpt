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
using VisualStudioChatGpt.Tools;
using Newtonsoft.Json.Linq;

namespace VisualStudioChatGpt
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
                _ = ChatGptHelper.RestSharpPostAsync(content, VirShowMessage, VirStart, VirEnd);
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
