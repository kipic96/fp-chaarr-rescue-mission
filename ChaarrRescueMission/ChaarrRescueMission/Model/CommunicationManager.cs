using ChaarrRescueMission.Model.Entity;
using ChaarrRescueMission.Properties;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Windows;

namespace ChaarrRescueMission.Model
{
    class CommunicationManager
    {
        public string Send(Cargo cargo)
        {
            var executeClient = new RestClient(Resources.CaptionSimulationExecute);
            var executeRequest = CreateJsonRequest(
                JsonConvert.SerializeObject(cargo, Formatting.Indented,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));
            IRestResponse executeResponse = executeClient.Execute(executeRequest);
            if (executeResponse.StatusCode != HttpStatusCode.OK)
                return executeResponse.ErrorMessage;

            var describeClient = new RestClient(Resources.CaptionSimulationDescribe);
            var describeRequest = new RestRequest(Method.GET);
            IRestResponse describeResponse = describeClient.Execute(describeRequest);
            if (describeResponse.StatusCode == HttpStatusCode.OK)
            {
                return describeResponse.Content;
            }
            else
                return describeResponse.ErrorMessage;
        }

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
