using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VisualStudioChatGpt.Commands
{
    /// <summary>
    /// 设置
    /// </summary>
    internal class SetUp : MyBase
    {
        internal override void VirHandler(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            var form = new FormSetUp();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.Show();
        }
    }
}
