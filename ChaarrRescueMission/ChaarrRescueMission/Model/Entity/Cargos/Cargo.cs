using ChaarrRescueMission.Properties;

namespace ChaarrRescueMission.Model.Entity.Cargos
{
    public class Cargo
    {
        public string Login = Resources.CaptionLogin;
        public string Token = Resources.CaptionToken;
        public string Command { get; set; }
        public string Parameter { get; set; }
        public string Value { get; set; }
    }
}
