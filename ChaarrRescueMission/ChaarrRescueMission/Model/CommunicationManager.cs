using ChaarrRescueMission.Enum;
using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Model.Json;
using ChaarrRescueMission.Properties;
using RestSharp;
using System.Net;

namespace ChaarrRescueMission.Model
{
    class CommunicationManager
    {
        private string executeConnectionString;
        private string describeConnectionString;

        /// <summary>
        /// Constructor which sets game type configuration, Chaarr or Simulation.
        /// </summary>
        public CommunicationManager(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Chaarr:
                    executeConnectionString = Resources.CaptionChaarrExecute;
                    describeConnectionString = Resources.CaptionChaarrDescribe;
                    break;
                case GameType.Simulation:
                    executeConnectionString = Resources.CaptionSimulationExecute;
                    describeConnectionString = Resources.CaptionSimulationDescribe;
                    break;
            }
        }

        /// <summary>
        /// Sends Cargo (Action) through Json to server and receives Json's server response.
        /// </summary>
        public string Send(Cargo cargo)
        {
            var executeClient = new RestClient(executeConnectionString);
            var executeRequest = CreateJsonRequest(JsonConverter.Parse(cargo));
            IRestResponse executeResponse = executeClient.Execute(executeRequest);
            if (executeResponse.StatusCode != HttpStatusCode.OK)
                return executeResponse.ErrorMessage;

            var describeClient = new RestClient(describeConnectionString);
            var describeRequest = new RestRequest(Method.GET);
            IRestResponse describeResponse = describeClient.Execute(describeRequest);
            if (describeResponse.StatusCode == HttpStatusCode.OK)
            {
                return describeResponse.Content;
            }
            else
                return describeResponse.ErrorMessage;
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
    }
}
