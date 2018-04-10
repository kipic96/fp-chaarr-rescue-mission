using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Model.Entity.Cargos;
using ChaarrRescueMission.Model.Json;
using ChaarrRescueMission.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ChaarrRescueMission.Output
{
    class FileManager
    {
        public static void ToFile(string pathFile, Cargo cargo, int turn)
        {
            var turnString = $"{Resources.CaptionFileTurn}{turn.ToString()} ";
            var command = cargo.Command != null ? $"{Resources.CaptionFileCommand}{cargo.Command} " : string.Empty;
            var parameter = cargo.Parameter != null ? $"{Resources.CaptionFileParameter}{cargo.Parameter} " : string.Empty;
            var value = cargo.Value != null ? $"{Resources.CaptionFileValue}{cargo.Value} " : string.Empty;
            using (TextWriter textWriter = new StreamWriter(pathFile, append: true))
            {
                textWriter.WriteLine(turnString + command + parameter + value + Environment.NewLine);
            }
        }

        public static void ToFile(string pathFile, GameState gameState)
        {
            string jsonFormatted = JValue.Parse(JsonConvert.SerializeObject(gameState, new JsonSerializerSettings()
            {
                ContractResolver = new JsonPropertiesResolver(),
            })).ToString(Formatting.Indented);
            using (TextWriter textWriter = new StreamWriter(pathFile, append: gameState.Turn == Resources.CaptionFirstTurn ? false : true))
            {
                textWriter.WriteLine(jsonFormatted + Environment.NewLine);
            }
        }
    }
}
