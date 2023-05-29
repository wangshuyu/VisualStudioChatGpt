using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualStudioChatGpt.Commands
{
    public partial class FormSetUp : Form
    {
        public FormSetUp()
        {
            InitializeComponent();

            var entity = MyConfig.Get();
            if (entity != null)
            {
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
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var entity = new MyConfigModel()
            {
                apikey = txt_apikey.Text.Trim(),
                proxy = txt_proxy.Text.Trim(),
                model = txt_model.Text.Trim(),
                maxtoken = txt_mextoken.Text.Trim(),
                temperature = txt_temperature.Text.Trim(),
                timeout = txt_timeout.Text.Trim()
            };
            MyConfig.Set(entity);
        }
    }
}
