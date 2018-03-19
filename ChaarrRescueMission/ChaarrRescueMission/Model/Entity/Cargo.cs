using ChaarrRescueMission.Properties;
using Newtonsoft.Json;

namespace ChaarrRescueMission.Model.Entity
{
    class Cargo
    {
        [JsonProperty("Command", Order = 1)]
        public string Command { get; set; }
        [JsonProperty("Login", Order = 2)]
        private string _login = Resources.CaptionLogin;
        [JsonProperty("Token", Order = 3)]
        private string _token = Resources.CaptionToken;  
        [JsonProperty("Parameter", Order = 4)]      
        public string Parameter { get; set; }
        [JsonProperty("Value", Order = 5)]
        public string Value { get; set; }
    }
}
