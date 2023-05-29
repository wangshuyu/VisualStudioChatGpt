using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// 设置
    /// </summary>
    internal class SetUp : MyBase
    {
        public override void VirHandler(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            FormSetUp window = new FormSetUp();
            window.Show(); 
        }
    }
}
