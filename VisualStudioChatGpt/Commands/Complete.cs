using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// 完成代码
    /// </summary>
    internal class Complete : MyBase
    {
        public override async void VirHandler(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                // 在主线程执行异步操作完成后的代码逻辑
                var selectedTex = await GetSelectedTextAsync();
                await InsertChatGptAsync($"{TypeModel.Complete}{selectedTex}", InsertPointEnum.After);
            });
        }

        public override void VirStart()
        {
            //ThreadHelper.ThrowIfNotOnUIThread();
            //this.insertPoint.Insert("");
        }

        public override void VirEnd()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.insertPoint.Insert("\r\n");
            _ = SimulateCtrlKCtrlDAsync();
        }
    }
}
