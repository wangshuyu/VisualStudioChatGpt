using EnvDTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualStudioChatGpt.Model
{
    public class TypeHelper
    {
        Dictionary<TyperEnum, TypeModel> dict = new Dictionary<TyperEnum, TypeModel>();

        public TypeHelper()
        {
            dict = new Dictionary<TyperEnum, TypeModel>();
            dict.Add(TyperEnum.Complete, new TypeModel() { Problem = "", Determiner = "" });
            dict.Add(TyperEnum.FindBug, new TypeModel() { Problem = "", Determiner = "" });
            dict.Add(TyperEnum.RepairBug, new TypeModel() { Problem = "", Determiner = "" });
            dict.Add(TyperEnum.Complete, new TypeModel() { Problem = "", Determiner = "" });
            dict.Add(TyperEnum.Complete, new TypeModel() { Problem = "", Determiner = "" });
            dict.Add(TyperEnum.Complete, new TypeModel() { Problem = "", Determiner = "" });
            dict.Add(TyperEnum.Complete, new TypeModel() { Problem = "", Determiner = "" });
            dict.Add(TyperEnum.Complete, new TypeModel() { Problem = "", Determiner = "" });
            dict.Add(TyperEnum.Complete, new TypeModel() { Problem = "", Determiner = "" });
        }

    }

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
        internal static string AddSummary = $"请提供方法的注释信息，而不是代码实现, 只要注释信息,不要其它任何多余的信息,c#格式:summary格式。{common}";
        internal static string AddTest = $"写一个单元测试,不要前后说明文字,{common}";

        /// <summary>
        /// 问题
        /// 对应openai的user的content
        /// </summary>
        public string Problem { get; set; }

        /// <summary>
        /// openai system的限定词
        /// </summary>
        public string Determiner { get; set; }
    }

    /// <summary>
    /// 类型
    /// </summary>
    internal enum TyperEnum
    {
        /// <summary>
        /// 完善代码
        /// </summary>
        Complete,

        /// <summary>
        /// 查找bug
        /// </summary>
        FindBug,

        /// <summary>
        /// 修复bug
        /// </summary>
        RepairBug,

        /// <summary>
        /// 优化代码
        /// </summary>
        Optimize,

        /// <summary>
        /// 添加代码解释说明
        /// </summary>
        Explain,

        /// <summary>
        /// 添加说明
        /// </summary>
        AddComment,

        /// <summary>
        /// 添加注释
        /// </summary>
        AddSummary,

        /// <summary>
        /// 添加单元测试
        /// </summary>
        AddTest,
    }

    /// <summary>
    /// 插入位置
    /// </summary>
    internal enum InsertPointEnum
    {
        /// <summary>
        /// 选择代码前插入
        /// </summary>
        Before = 0,

        /// <summary>
        /// 选择代码后插入
        /// </summary>
        After = 1,

        /// <summary>
        /// 替换选择代码
        /// </summary>
        Replace = 2
    }
}
