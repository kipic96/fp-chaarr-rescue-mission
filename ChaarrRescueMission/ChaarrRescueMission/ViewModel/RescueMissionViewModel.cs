using ChaarrRescueMission.ViewModel.Base;
using ChaarrRescueMission.Model;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ChaarrRescueMission.Model.Entity;
using System.Collections.Generic;
using ChaarrRescueMission.Properties;

namespace ChaarrRescueMission.ViewModel
{
    class RescueMissionViewModel : BaseViewModel
    {
        private CommunicationManager _communicationManager = new CommunicationManager();

        public IList<string> PosibleActions { get; set; } = new List<string>()
        {
            Resources.CaptionScan,
            Resources.CaptionMove,
            Resources.CaptionProduce,
            Resources.CaptionHarvest,
            Resources.CaptionRepair,
            Resources.CaptionOrder,
            Resources.CaptionRestart,
        };

        private string _currentAction;
        public string CurrentAction
        {
            get
            {
                return _currentAction;
            }
            set
            {
                _currentAction = value;
                RaisePropertyChanged(nameof(RepairingEnabled));
                RaisePropertyChanged(nameof(PlacesEnabled));
                RaisePropertyChanged(nameof(ProductionsEnabled));
                RaisePropertyChanged(nameof(OrdersEnabled));
                RaisePropertyChanged(nameof(SuppliesEnabled));
            }
        }
        public IList<string> PosibleRepairing { get; set; } = new List<string>()
        {
            Resources.CaptionChaarr,
            Resources.CaptionEsthajnalcsillag,
            Resources.CaptionShuttle,
            Resources.CaptionAsteroids,
            Resources.CaptionPołudnica,
            Resources.CaptionPartialshuttle,
            Resources.CaptionCommunications,
        };

        public string CurrentRepairing { get; set; }

        public bool RepairingEnabled
        {
            get
            {
                if (CurrentAction == Resources.CaptionRepair)
                    return true;
                return false;
            }
        }

        public IList<string> PosiblePlaces { get; set; } = new List<string>()
        {
            Resources.CaptionChaarr,
            Resources.CaptionEsthajnalcsillag,
            Resources.CaptionShuttle,
            Resources.CaptionAsteroids,
            Resources.CaptionPołudnica,
        };

        public string CurrentPlaces { get; set; }

        public bool PlacesEnabled
        {
            get
            {
                if (CurrentAction == Resources.CaptionHarvest ||
                    CurrentAction == Resources.CaptionMove || 
                    CurrentAction == Resources.CaptionScan ||
                    CurrentAction == Resources.CaptionOrder)
                    return true;
                return false;
            }
        }

        public IList<string> PosibleProductions { get; set; } = new List<string>()
        {
            Resources.CaptionDecoy,
            Resources.CaptionWeapons,
            Resources.CaptionSupplies,
            Resources.CaptionTools,
            Resources.CaptionEnergy,
            Resources.CaptionShuttlewrench,
        };

        private string _currentProduction;
        public string CurrentProduction
        {
            get
            {
                return _currentProduction;
            }
            set
            {
                _currentProduction = value;
                RaisePropertyChanged(nameof(SuppliesEnabled));
            }
        }

        public bool SuppliesEnabled
        {
            get
            {
                if (CurrentAction == Resources.CaptionProduce &&
                    CurrentProduction == Resources.CaptionSupplies)
                    return true;
                return false;
            }
        }

        

        public bool ProductionsEnabled
        {
            get
            {
                if (CurrentAction == Resources.CaptionProduce)
                    return true;
                return false;
            }
        }

        public IList<string> PosibleOrders { get; set; } = new List<string>()
        {
            Resources.CaptionHelp,
            Resources.CaptionFinalWar,
            Resources.CaptionEvacScience,
            Resources.CaptionEvacSurvivors,
            Resources.CaptionRetreat
        };

        public string CurrentOrderType { get; set; }

        public bool OrdersEnabled
        {
            get
            {
                if (CurrentAction == Resources.CaptionOrder)
                    return true;
                return false;
            }
        }

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
