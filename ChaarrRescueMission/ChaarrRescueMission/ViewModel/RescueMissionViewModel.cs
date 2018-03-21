﻿using ChaarrRescueMission.Enum;
using ChaarrRescueMission.Model;
using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Model.Factory;
using ChaarrRescueMission.Model.Json;
using ChaarrRescueMission.Output;
using ChaarrRescueMission.Properties;
using ChaarrRescueMission.ViewModel.Base;
using System.Collections.Generic;
using System.Windows.Input;

namespace ChaarrRescueMission.ViewModel
{
    class RescueMissionViewModel : BaseViewModel
    {
        #region Constructor

        public RescueMissionViewModel()
        {
            _communicationManager = new CommunicationManager(GameType.Simulation);
            Json = _communicationManager.Restart();
            GameState = JsonConverter.Parse(Json);
        }

        #endregion Constructor

        #region Model

        private CommunicationManager _communicationManager;

        #endregion Model

        #region CurrentGameStates

        public string Json { get; set; }

        public string CurrentRepairing { get; set; } = string.Empty;
        public string CurrentPlace { get; set; } = string.Empty;
        public string CurrentOrderType { get; set; } = string.Empty;

        private string _currentAction = string.Empty;
        public string CurrentAction
        {
            get { return _currentAction; }
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

        private string _currentProduction = string.Empty;
        public string CurrentProduction
        {
            get { return _currentProduction; }
            set
            {
                _currentProduction = value;
                RaisePropertyChanged(nameof(SuppliesEnabled));
            }
        }        

        public int _suppliesValue = int.Parse(Resources.CaptionSuppliesDefault);
        public int SuppliesValue
        {
            get { return _suppliesValue; }
            set
            {
                _suppliesValue = value;
                RaisePropertyChanged(nameof(SuppliesValue));
            }
        }

        public string CurrentGameType
        {
            get
            {
                if (GameType == GameType.Chaarr)
                    return Resources.CaptionChaarr;
                if (GameType == GameType.Simulation)
                    return Resources.CaptionSimulation;
                return string.Empty;
            }
        }

        private GameType _gameType = GameType.Simulation;
        public GameType GameType
        {
            get { return _gameType; }
            set
            {
                _gameType = value;
                RaisePropertyChanged(nameof(CurrentGameType));
            }
        }

        private GameState _gameState = new GameState();
        public GameState GameState
        {
            get { return _gameState; }
            set
            {
                _gameState = value;
                RaisePropertyChanged(nameof(GameState));
            }
        }

        #endregion CurrentGameStates

        #region ViewDataSources

        public IEnumerable<string> PosibleActions { get; set; } = new List<string>()
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
        
        public IEnumerable<string> PosibleRepairing { get; set; } = new List<string>()
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

        public IEnumerable<string> PosiblePlaces { get; set; } = new List<string>()
        {
            string.Empty,
            Resources.CaptionChaarr,
            Resources.CaptionEsthajnalcsillag,
            Resources.CaptionShuttle,
            Resources.CaptionAsteroids,
            Resources.CaptionPołudnica,
        };      

        public IEnumerable<string> PosibleProductions { get; set; } = new List<string>()
        {
            string.Empty,
            Resources.CaptionDecoy,
            Resources.CaptionWeapons,
            Resources.CaptionSupplies,
            Resources.CaptionTools,
            Resources.CaptionEnergy,
            Resources.CaptionShuttlewrench,
        };   
        
        public IEnumerable<string> PosibleOrders { get; set; } = new List<string>()
        {
            string.Empty,
            Resources.CaptionHelp,
            Resources.CaptionFinalWar,
            Resources.CaptionEvacScience,
            Resources.CaptionEvacSurvivors,
            Resources.CaptionRetreat
        };

        #endregion ViewDataSources

        #region ControlsEnablingFlags

        public bool PlacesEnabled
        {
            get
            {
                return (CurrentAction == Resources.CaptionHarvest ||
                    CurrentAction == Resources.CaptionMove ||
                    CurrentAction == Resources.CaptionScan ||
                    CurrentAction == Resources.CaptionOrder);
            }
        }

        public bool RepairingEnabled
        {
            get { return (CurrentAction == Resources.CaptionRepair); }
        }

        public bool SuppliesEnabled
        {
            get
            {
                return (CurrentAction == Resources.CaptionProduce &&
                    CurrentProduction == Resources.CaptionSupplies);
            }
        }

        public bool ProductionsEnabled
        {
            get { return (CurrentAction == Resources.CaptionProduce); }
        }

        public bool OrdersEnabled
        {
            get { return (CurrentAction == Resources.CaptionOrder); }
        }

        #endregion ControlsEnablingFlags

        #region Commands

        public ICommand ExecuteSend
        {
            get
            {
                return new NoParameterCommand(
                    () => ExecuteRequest(),
                    () => CanExecuteRequest());
            }
        }

        public ICommand SaveJson
        {
            get
            {
                return new NoParameterCommand(
                    () => FileManager.SaveToFile(Json));
            }
        }

        public ICommand Clear
        {
            get
            {
                return new NoParameterCommand(
                    () =>
                    {
                        CurrentAction = string.Empty;
                        CurrentOrderType = string.Empty;
                        CurrentPlace = string.Empty;
                        CurrentProduction = string.Empty;
                        CurrentRepairing = string.Empty;
                        SuppliesValue = int.Parse(Resources.CaptionSuppliesDefault);
                    });
            }
        }

        public ICommand SwitchChaarrSimulation
        {
            get
            {
                return new NoParameterCommand(
                    () =>
                    {
                        if (GameType == GameType.Chaarr)
                            GameType = GameType.Simulation;
                        else
                            GameType = GameType.Chaarr;
                        _communicationManager = new CommunicationManager(GameType);
                    },
                    () => IsGameTerminated());
            }
        }

        #endregion Commands

        #region Private Methods

        private void ExecuteRequest()
        {
            Json = _communicationManager.Send(
                CargoFactory.Create(CurrentAction, CurrentPlace,
                CurrentRepairing, CurrentProduction,
                CurrentOrderType, SuppliesValue.ToString()));
            GameState = JsonConverter.Parse(Json);
        }

        private bool CanExecuteRequest()
        {
            return !((IsGameTerminated() && !IsGameWantedToRestart()) ||
                IsActionInputIncomplete());
        }

        private bool IsGameTerminated()
        {
            return (bool.Parse(GameState.IsTerminated) == true ||
                GameState.Parameters.ChaarrHatred == Resources.CaptionMaxChaarrHatred);
        }

        private bool IsGameWantedToRestart()
        {
            return (CurrentAction == Resources.CaptionRestart);
        }

        private bool IsActionInputIncomplete()
        {
            return ((CurrentAction == string.Empty) ||
                   (OrdersEnabled && CurrentOrderType == string.Empty) ||
                   (PlacesEnabled && CurrentPlace == string.Empty) ||
                   (ProductionsEnabled && CurrentProduction == string.Empty) ||
                   (RepairingEnabled && CurrentRepairing == string.Empty));
        }

        #endregion Private Methods
    }
}
