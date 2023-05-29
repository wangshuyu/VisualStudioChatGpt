using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioChatGpt.Commands
{
    public class TypeModel
    {
        private static string common = "中文回复,尽量回复简短一点,C#\r\n";
        public static string Complete = $"请完善代码,不要前后说明,{common}";
        public static string RepairBug = $"修复bugs,只需要返回代码部分,{common}";
        public static string FindBug = $"查找bugs,中文回复,尽量回复简短一点";
        public static string Optimize = $"请优化代码 , 不要说明,{common}";
        public static string Explain = $"请帮我写个简短的解释说明 ,{common}";
        public static string AddComment = $"请帮我添加方法注释说明 ,{common}";
        public static string AddSummary = $"请提供方法的注释信息，而不是代码实现, 只要注释信息,不要其它任何多余的信息 中文回复,格式:summary形式。{common}";
        public static string AddTest = $"写一个单元测试,不要前后说明文字,,{common}";
    }

    /// <summary>
    /// 插入位置
    /// </summary>
    public enum InsertPointEnum
    {
        Before = 0,
        After = 1,
        Replace = 2
    }
}
