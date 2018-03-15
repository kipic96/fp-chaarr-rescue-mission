using ChaarrRescueMission.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaarrRescueMission.Model
{
    class Cargo
    {
        private string _login = Resources.CaptionLogin;
        private string _token = Resources.CaptionToken;
        public string Command { get; set; }
        //public string Parameter { get; set; }
        //public string Value { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
