using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Model.Entity.Cargos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChaarrRescueMission.Model.Json
{
    public class JsonConverter
    {
        /// <summary>
        /// Conversion from Json string to GameState object
        /// </summary>
        public static GameState Parse(string json)
        {
            var gameState = new GameState();
            if (json != null)
            {
                JToken gameStatusToken = JObject.Parse(json);
                gameState = JsonConvert.DeserializeObject<GameState>(json);
            }
            return gameState;
        }

        /// <summary>
        /// Conversion from Cargo object to Json string
        /// </summary>
        public static string Parse(Entity.Cargos.Cargo cargo)
        {
            return JsonConvert.SerializeObject(cargo, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
        }
    }
}
