using ChaarrRescueMission.Model.Entity.Cargos;
using ChaarrRescueMission.Model.Json;
using ChaarrRescueMission.Properties;
using RestSharp;
using System.Net;

namespace ChaarrRescueMission.Model
{
    public class CommunicationManager
    {
        public string ExecuteConnectionString { get; set; }
        public string DescribeConnectionString { get; set; }

        /// <summary>
        /// Sends Cargo (Action) through Json to server and receives Json's server response.
        /// </summary>
        public string Send(Cargo cargo)
        {
            var executeClient = CreateClient(ExecuteConnectionString);

            var executeRequest = CreateJsonRequest(JsonConverter.Parse(cargo));
            IRestResponse executeResponse = executeClient.Execute(executeRequest);
            
            if (executeResponse.StatusCode != HttpStatusCode.OK)
                throw new WebException(Resources.CaptionWebConnectionError + ExecuteConnectionString);

            var describeClient = CreateClient(DescribeConnectionString);
            var describeRequest = new RestRequest(Method.GET);
            IRestResponse describeResponse = describeClient.Execute(describeRequest);
            if (describeResponse.StatusCode == HttpStatusCode.OK)
                return describeResponse.Content;
            else
                throw new WebException(Resources.CaptionWebConnectionError + DescribeConnectionString);
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public string Restart()
        {
            return Send(new Cargo()
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

        /// <summary>
        /// Creates RestClient with Security Protocol.
        /// </summary>
        private RestClient CreateClient(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var client = new RestClient(url);
            return client;
        }
    }
}
