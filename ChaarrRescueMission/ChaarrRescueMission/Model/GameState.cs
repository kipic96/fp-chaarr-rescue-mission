using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaarrRescueMission.Model
{
    class GameState
    {
        public string Turn { get; set; } = "1";
        public string Location { get; set; }
        public IList<string> Events { get; set; }
        public string LastTurnEvents { get; set; }
        public IList<string> Equipments { get; set; }
        public IList<string> LogBook { get; set; }

    }
}
