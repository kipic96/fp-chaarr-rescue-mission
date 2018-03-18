using ChaarrRescueMission.ViewModel.Base;
using ChaarrRescueMission.Model;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ChaarrRescueMission.Model.Entity;

namespace ChaarrRescueMission.ViewModel
{
    class RescueMissionViewModel : BaseViewModel
    {
        private CommunicationManager _communicationManager = new CommunicationManager();

        private GameState _gameState;
        public GameState GameState
        {
            get
            {
                if (_gameState == null)
                    _gameState = new GameState();
                return _gameState;
            }
            set
            {
                _gameState = value;
                RaisePropertyChanged(nameof(GameState));
            }
        }

        private string _json = string.Empty;
        public string Json
        {
            get
            {
                return _json;
            }
            set
            {
                _json = value;
                RaisePropertyChanged(nameof(Json));
            }
        }       

        #region Commands

        private ICommand _command;
        public ICommand Command
        {
            get
            {
                if (_command == null)
                {
                    _command = new NoParameterCommand(
                        () =>
                        {
                            Json = _communicationManager.Send(new Cargo()
                            {
                                Command = "RestartSimulation",
                            });
                            JToken gameStatusToken = JObject.Parse(Json);
                            GameState = JsonConvert.DeserializeObject<GameState>(Json);
                        });
                }
                return _command;
            }
        }


        #endregion Commands
    }
}
