using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioChatGpt.Model
{
    internal class TypeModel
    {
        private static string common = "中文回复,尽量回复简短一点,C#\r\n";

        internal static string Message = "请选择代码块后在执行此操作!";

        internal static string Complete = $"请完善代码,不要前后说明,{common}";
        internal static string RepairBug = $"修复bugs,只需要返回代码部分,{common}";
        internal static string FindBug = $"查找bugs,{common}";
        internal static string Optimize = $"请优化压缩代码,只返回优化后的代码实现,不需要返回其他任何多余信息,{common}";
        internal static string Explain = $"请帮我写个简短的解释说明 ,{common}";
        internal static string AddComment = $"请帮我添加方法注释说明 ,{common}";
        internal static string AddSummary = $"请提供方法的注释信息，而不是代码实现, 只要注释信息,不要其它任何多余的信息,格式:summary形式。{common}";
        internal static string AddTest = $"写一个单元测试,不要前后说明文字,{common}";
    }

    /// <summary>
    /// 插入位置
    /// </summary>
    internal enum InsertPointEnum
    {
        Before = 0,
        After = 1,
        Replace = 2
    }
}
