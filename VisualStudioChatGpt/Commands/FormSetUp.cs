using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisualStudioChatGpt.Model;
using Vs_ChatGpt.Model;

namespace VisualStudioChatGpt.Commands
{
    public partial class FormSetUp : Form
    {
        public FormSetUp()
        {
            InitializeComponent();

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            ServiceEnum[] enumValues = (ServiceEnum[])Enum.GetValues(typeof(ServiceEnum));
            comboBox1.DataSource = enumValues;
            comboBox1.SelectedIndex = 0; // 默认选中第一个选项

            var entity = MyConfig.Get();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.serviceName))
                {
                    comboBox1.Text = entity.serviceName;
                }
                if (!string.IsNullOrEmpty(entity.apikey))
                {
                    txt_apikey.Text = entity.apikey;
                }
                if (!string.IsNullOrEmpty(entity.proxy))
                {
                    txt_proxy.Text = entity.proxy;
                } 
                if (!string.IsNullOrEmpty(entity.model))
                {
                    txt_model.Text = entity.model;
                }
                if (!string.IsNullOrEmpty(entity.maxtoken))
                {
                    txt_mextoken.Text = entity.maxtoken;
                }
                if (!string.IsNullOrEmpty(entity.temperature))
                {
                    txt_temperature.Text = entity.temperature;
                }
                if (!string.IsNullOrEmpty(entity.timeout))
                {
                    txt_timeout.Text = entity.timeout;
                }
                if (!string.IsNullOrEmpty(entity.apiurl))
                {
                    txt_apiurl.Text = entity.apiurl;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var entity = new MyConfigModel()
            {
                serviceName = comboBox1.SelectedValue.ToString(),
                apiurl = txt_apiurl.Text.Trim(),
                apikey = txt_apikey.Text.Trim(),
                proxy = txt_proxy.Text.Trim(),
                model = txt_model.Text.Trim(),
                maxtoken = txt_mextoken.Text.Trim(),
                temperature = txt_temperature.Text.Trim(),
                timeout = txt_timeout.Text.Trim()
            };
            MyConfig.Set(entity);
            MessageBox.Show("保存配置文件成功!");
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/wangshuyu/VisualStudioChatGpt#%E8%AE%BE%E7%BD%AE%E5%8A%9F%E8%83%BD");
        }
    }
}
