using ChaarrRescueMission.Model.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ChaarrRescueMission.Model.Json
{
    public class JsonConverter
    {
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
