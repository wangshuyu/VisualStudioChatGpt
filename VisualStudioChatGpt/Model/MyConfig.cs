using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vs_ChatGpt.Model;

namespace VisualStudioChatGpt.Model
{
    public class MyConfig
    {
        static string configFile = "config.json";

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <returns></returns>
        public static MyConfigModel Get()
        {
            var entity = new MyConfigModel()
            {
                model = "gpt-3.5-turbo",
                maxtoken = "500",
                temperature = "0.7",
                timeout = "60"
            };
            if (File.Exists(configFile))
            {
                string json = File.ReadAllText(configFile);
                entity = JsonConvert.DeserializeObject<MyConfigModel>(json);
            }
            return entity;
        }

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="entity"></param>
        public static void Set(MyConfigModel entity)
        {
            string updatedJson = JsonConvert.SerializeObject(entity);
            File.WriteAllText(configFile, updatedJson);
        }
    }
}
