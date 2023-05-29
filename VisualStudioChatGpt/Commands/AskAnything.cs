using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// 对话提问
    /// </summary>
    internal class AskAnything : MyBase
    {
        public override void VirHandler(object sender, EventArgs e)
        {
            Process.Start("https://chat.openai.com/");
            //base.VirHandler(sender, e);
        }
    }
}
