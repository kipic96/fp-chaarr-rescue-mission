using ChaarrRescueMission.Properties;
using Newtonsoft.Json;

namespace ChaarrRescueMission.Model.Entity
{
    class Cargo
    {
        public string Command { get; set; }
        public string Login = Resources.CaptionLogin;
        public string Token = Resources.CaptionToken;        
        public string Parameter { get; set; }
        public string Value { get; set; }
    }
}
