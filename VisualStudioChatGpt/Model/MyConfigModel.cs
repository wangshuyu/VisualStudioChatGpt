using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vs_ChatGpt.Model
{

    public class MyConfigModel
    {
        /// <summary>
        /// 服务名称: OpenAI/Azure云
        /// </summary>
        public string serviceName { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string apiurl { get; set; }

        /// <summary>
        /// 验签key
        /// </summary>
        public string apikey { get; set; }

        /// <summary>
        /// 代理地址
        /// Azure云直接删除不添加就行
        /// </summary>
        public string proxy { get; set; }

        /// <summary>
        /// 最大的tokens
        /// 一般500基本够用了
        /// </summary>
        public string maxtoken { get; set; }

        /// <summary>
        /// 热度随机性
        /// 一般给0,越小越严谨,建议别超过0.3
        /// </summary>
        public string temperature { get; set; }

        /// <summary>
        /// 对话模型
        /// </summary>
        public string model { get; set; }

        /// <summary>
        /// 默认超时时间
        /// 30秒一般足够用了 除非特别大的一段代码
        /// </summary>
        public string timeout { get; set; }
    }
}
