using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Model.Entity.Cargos;
using ChaarrRescueMission.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ChaarrRescueMission.Output
{
    class LogManager
    {
        private IList<Cargo> _commandLists = new List<Cargo>();
        private IList<GameState> _gameStates = new List<GameState>();

        public void AddTurnReport(Cargo command, GameState gameState)
        {
            if (gameState == null || command == null)
                return;
            _commandLists.Add(command);
            _gameStates.Add(gameState);
        }

        public void AddTurnReport(GameState gameState)
        {
            if (gameState == null)
                return;
            _gameStates.Add(gameState);
        }

        public void GenerateLog()
        {
            string pathFile = AppDomain.CurrentDomain.BaseDirectory + Resources.CaptionFileTitle;
            for (int i = 0; i < _gameStates.Count; i++)
            {
                FileManager.ToFile(pathFile, _gameStates[i]);
                if (i < _commandLists.Count)
                    FileManager.ToFile(pathFile, _commandLists[i], (int)(double.Parse(_gameStates[i].Turn, CultureInfo.InvariantCulture)));                
            }
        }
    }
}
