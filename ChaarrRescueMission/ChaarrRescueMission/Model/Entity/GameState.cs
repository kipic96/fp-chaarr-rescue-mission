using ChaarrRescueMission.Model.Entity.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ChaarrRescueMission.Model.Entity
{
    public class GameState 
    {
        public string Turn { get; set; }
        public string Location { get; set; }
        private IEnumerable<string> _events;
        public IEnumerable<string> Events
        {
            get { return _events; }
            set { _events = EventFiltering.Filter(value); }
        }
        private IEnumerable<string> _lastEvents;
        public IEnumerable<string> LastTurnEvents
        {
            get { return _lastEvents; }
            set { _lastEvents = EventFiltering.Filter(value); }
        }

        private IEnumerable<string> _logBook;
        public IEnumerable<string> LogBook
        {
            get { return _logBook; }
            set { _logBook = value.Reverse(); }
        }        
        public IEnumerable<string> Equipments { get; set; }
        public Scores Scores { get; set; }
        public Parameters Parameters { get; set; }
        public string IsTerminated { get; set; }
    }
}
