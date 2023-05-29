using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualStudioChatGpt.Model;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// 为当前代码写说明
    /// </summary>
    internal class Explain : MyBase
    {
        public override async void VirHandler(object sender, EventArgs e)
        {
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            {
                // 在主线程执行异步操作完成后的代码逻辑
                var selectedTex = await GetSelectedTextAsync();
                if (!string.IsNullOrEmpty(selectedTex))
                {
                    await InsertChatGptAsync($"{TypeModel.Explain}{selectedTex}", InsertPointEnum.Before);
                }
            });
        }

        public override void VirStart()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.insertPoint.Insert("\r\n//");
        }

        public override void VirEnd()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            this.insertPoint.Insert("\r\n");
            _ = SimulateCtrlKCtrlDAsync();
        }
    }
}
