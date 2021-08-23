using BidSignalR.Services.IServices;
using RestSharp;

namespace BidSignalR.Services
{
    public class RestClientApiCall : IRestClientApiCall
    {
        public IRestResponse Execute(IRestRequest request, string URL)
        {
            var client = new RestClient(URL);
            client.Timeout = -1;

            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
