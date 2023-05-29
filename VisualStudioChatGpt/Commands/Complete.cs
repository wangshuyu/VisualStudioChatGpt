using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudioChatGpt.Model;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// 完成代码
    /// </summary>
    internal class Complete : MyBase
    {
        internal override async void VirHandler(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                // 在主线程执行异步操作完成后的代码逻辑
                var selectedTex = await GetSelectedTextAsync();
                if (!string.IsNullOrEmpty(selectedTex))
                {
                    await InsertChatGptAsync($"{TypeModel.Complete}{selectedTex}", InsertPointEnum.After);
                }
            });
        }

        internal override void VirStart()
        {
            //ThreadHelper.ThrowIfNotOnUIThread();
            //this.insertPoint.Insert("");
        }

        internal override void VirEnd()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.insertPoint.Insert("\r\n");
            _ = SimulateCtrlKCtrlDAsync();
        }
    }
}
