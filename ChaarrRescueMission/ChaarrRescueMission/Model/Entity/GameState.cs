using System.Collections.Generic;
using System.ComponentModel;

namespace ChaarrRescueMission.Model.Entity
{
    class GameState : INotifyPropertyChanged
    {
        public string Turn { get; set; }
        public string Location { get; set; }
        public IList<string> Events { get; set; }
        public IList<string> LastTurnEvents { get; set; }
        public IList<string> Equipments { get; set; }
        public IList<string> LogBook { get; set; }
        private Scores _scores;
        public Scores Scores
        {
            get { return _scores; }
            set
            {
                _scores = value;
                RaisePropertyChanged(nameof(Scores));
            }
        }
        private Parameters _parameters;
        public Parameters Parameters    
        {
            get { return _parameters; }
            set
            {
                _parameters = value;
                RaisePropertyChanged(nameof(Parameters));
            }
        }
        public string IsTerminated { get; set; }


        public virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
