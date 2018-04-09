using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Model.Entity.Cargos;
using ChaarrRescueMission.Properties;
using System;
using System.Collections.Generic;

namespace ChaarrRescueMission.Output
{
    class LogManager
    {
        private IList<Cargo> _commandLists = new List<Cargo>();
        private IList<GameState> _gameStates = new List<GameState>();

        public void AddTurnReport(Cargo command, GameState gamestate)
        {
            _commandLists.Add(command);
            _gameStates.Add(gamestate);
        }

        public void GenerateLog()
        {
            string pathFile = AppDomain.CurrentDomain.BaseDirectory + Resources.CaptionFileTitle;
            for (int i = 0; i < _commandLists.Count && i < _gameStates.Count; i++)
            {
                FileManager.ToFile(pathFile, _commandLists[i]);
            }
        }
    }
}
