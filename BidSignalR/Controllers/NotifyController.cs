using BidSignalR.Models;
using BidSignalR.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net;
using RestSharp;
using BidSignalR.Constants;

namespace BidSignalR.Controllers
{
    public class NotifyController : Controller
    {
        private readonly IMessageHandler _messageHandler;
        private readonly IRestClientApiCall _restClientApiCall;

        public NotifyController(IMessageHandler messageHandler, IRestClientApiCall restClientApiCall)
        {
            _messageHandler = messageHandler;
            _restClientApiCall = restClientApiCall;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Bid bid)
        {
            var request = new RestRequest(Method.POST);

            request.AddHeader("Atg-Client-Id", "1");
            request.AddHeader("Atg-Client-Ip", "1");
            request.AddHeader("Atg-App-Id", "1");
            request.AddHeader("Atg-User-Id", "1");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", JsonConvert.SerializeObject(bid), ParameterType.RequestBody);

            IRestResponse response = _restClientApiCall.Execute(request, ApplicationContants.BID_API);

            string bidResponse;

            if (response.Content.Contains("isValid"))
            {
                bidResponse = JsonConvert.DeserializeObject<BidSuccessResponse>(response.Content).validationResults.FirstOrDefault().description;
            }
            else
            {
                bidResponse = JsonConvert.DeserializeObject<BidErrorResponse[]>(response.Content).FirstOrDefault().description;
            }

            _messageHandler.BroadcastMessage(bid);

            return Json(bidResponse);
        }
        public ActionResult Bidv2()
        {
            return View()
        }
        [HttpPost]
        public ActionResult Bidv2(BidV2Model model)
        {
            BidV2SuccessResponse bidV2SuccessResponse = new BidV2SuccessResponse();
            return Json(bidV2SuccessResponse);
        }
    }
}
