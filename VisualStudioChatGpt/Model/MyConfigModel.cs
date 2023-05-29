using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vs_ChatGpt.Model
{

    public class MyConfigModel
    {
        public string apikey { get; set; }
        public string proxy { get; set; }
        public string maxtoken { get; set; }
        public string temperature { get; set; }
        public string model { get; set; }
        public string timeout { get; set; }
    }
}
