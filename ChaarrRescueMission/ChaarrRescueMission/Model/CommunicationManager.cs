using ChaarrRescueMission.Enum;
using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Model.Entity.Cargos;
using ChaarrRescueMission.Model.Json;
using ChaarrRescueMission.Properties;
using RestSharp;
using System.Net;

namespace ChaarrRescueMission.Model
{
    public class CommunicationManager
    {
        private string _executeConnectionString;
        private string _describeConnectionString;

        /// <summary>
        /// Constructor which sets game type configuration, Chaarr or Simulation.
        /// </summary>
        public CommunicationManager(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Chaarr:
                    _executeConnectionString = Resources.CaptionChaarrExecute;
                    _describeConnectionString = Resources.CaptionChaarrDescribe;
                    break;
                case GameType.Simulation:
                    _executeConnectionString = Resources.CaptionSimulationExecute;
                    _describeConnectionString = Resources.CaptionSimulationDescribe;
                    break;
            }
        }

        /// <summary>
        /// Sends Cargo (Action) through Json to server and receives Json's server response.
        /// </summary>
        public string Send(Entity.Cargos.Cargo cargo)
        {
            var executeClient = CreateClient(_executeConnectionString);

            var executeRequest = CreateJsonRequest(JsonConverter.Parse(cargo));
            IRestResponse executeResponse = executeClient.Execute(executeRequest);
            
            if (executeResponse.StatusCode != HttpStatusCode.OK)
                return executeResponse.ErrorMessage;

            var describeClient = CreateClient(_describeConnectionString);
            var describeRequest = new RestRequest(Method.GET);
            IRestResponse describeResponse = describeClient.Execute(describeRequest);
            if (describeResponse.StatusCode == HttpStatusCode.OK)
            {
                return describeResponse.Content;
            }
            else
            {
                return describeResponse.ErrorMessage;
            }
        }

        /// <summary>
        /// Restarts the game, used at start of application.
        /// </summary>
        public string Restart()
        {
            return Send(new Entity.Cargos.Cargo()
            {
                Command = Resources.CaptionRestart,
            });
        }

        /// <summary>
        /// Creates Request with POST Method for Executing Action and with Json parameter.
        /// </summary>
        private RestRequest CreateJsonRequest(string jsonCargo)
        {
            var request = new RestRequest(Method.POST);
            request.AddHeader(Resources.CaptionCacheControl, Resources.CaptionNoCache);
            request.AddHeader(Resources.CaptionContentType, Resources.CaptionJsonApp);

            request.AddParameter(Resources.CaptionJsonApp, jsonCargo, ParameterType.RequestBody);
            return request;
        }

        private RestClient CreateClient(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var client = new RestClient(url);
            return client;
        }
    }
}
