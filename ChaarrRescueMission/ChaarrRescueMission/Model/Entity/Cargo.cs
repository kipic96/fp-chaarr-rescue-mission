using ChaarrRescueMission.Properties;
using Newtonsoft.Json;

namespace ChaarrRescueMission.Model.Entity
{
    class Cargo
    {
        [JsonProperty("Login")]
        private string _login = Resources.CaptionLogin;
        [JsonProperty("Token")]
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
