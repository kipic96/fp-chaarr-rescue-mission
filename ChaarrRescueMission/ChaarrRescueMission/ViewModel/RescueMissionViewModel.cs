using ChaarrRescueMission.ViewModel.Base;
using ChaarrRescueMission.Model;
using System.Windows.Input;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ChaarrRescueMission.Model.Entity;
using System.Collections.Generic;
using ChaarrRescueMission.Properties;
using ChaarrRescueMission.Model.Factory;
using ChaarrRescueMission.Output;

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

        public IList<string> PosibleActions { get; set; } = new List<string>()
        {
            string.Empty,
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
            string.Empty,
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
            string.Empty,
            Resources.CaptionChaarr,
            Resources.CaptionEsthajnalcsillag,
            Resources.CaptionShuttle,
            Resources.CaptionAsteroids,
            Resources.CaptionPołudnica,
        };

        public string CurrentPlace { get; set; }

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
            string.Empty,
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

        public int SuppliesValue { get; set; } = int.Parse(Resources.CaptionSuppliesDefault);

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
            string.Empty,
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

        private ICommand _executeSendnd;
        public ICommand ExecuteSend
        {
            get
            {
                if (_executeSendnd == null)
                {
                    _executeSendnd = new NoParameterCommand(
                        () =>
                        {
                            Json = _communicationManager.Send(
                                CargoFactory.Create(CurrentAction, CurrentPlace, 
                                CurrentRepairing, CurrentProduction, 
                                CurrentOrderType, SuppliesValue.ToString()));
                            JToken gameStatusToken = JObject.Parse(Json);
                            GameState = JsonConvert.DeserializeObject<GameState>(Json);
                        },
                        () =>
                        {
                            if (GameState.IsTerminated != null &&
                                bool.Parse(GameState.IsTerminated) == true &&
                                CurrentAction != null && 
                                CurrentAction != Resources.CaptionRestart)
                                return false;
                            if ((CurrentAction == string.Empty) ||
                                 (OrdersEnabled && string.IsNullOrEmpty(CurrentOrderType)) ||
                                 (PlacesEnabled && string.IsNullOrEmpty(CurrentPlace)) ||
                                 (ProductionsEnabled && string.IsNullOrEmpty(CurrentProduction)) ||
                                 (RepairingEnabled && string.IsNullOrEmpty(CurrentRepairing)))
                                 return false;
                            return true;                                
                        }
                        );
                }
                return _executeSendnd;
            }
        }

        private ICommand _saveJson;
        public ICommand SaveJson
        {
            get
            {
                if (_saveJson == null)
                {
                    _saveJson = new NoParameterCommand(
                        () =>
                        {
                            FileManager.SaveToFile(Json);
                        });
                }
                return _saveJson;
            }
        }

        private ICommand _clear;
        public ICommand Clear
        {
            get
            {
                if (_clear == null)
                {
                    _clear = new NoParameterCommand(
                        () =>
                        {
                            CurrentAction = PosibleActions[0];
                            CurrentOrderType = PosibleOrders[0];
                            CurrentPlace = PosiblePlaces[0];
                            CurrentProduction = PosibleProductions[0];
                            CurrentRepairing = PosibleRepairing[0];
                            SuppliesValue = int.Parse(Resources.CaptionSuppliesDefault);
                        });
                }
                return _clear;
            }
        }

        #endregion Commands
    }
}
