using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidSignalR.Services.IServices
{
    public interface IRestClientApiCall
    {
        IRestResponse Execute(IRestRequest request, string URL);
    }
}
