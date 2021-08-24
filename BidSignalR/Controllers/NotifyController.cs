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
using AutoMapper;
using MediatR;
using BidSignalR.Handler.Bid.Command;
namespace BidSignalR.Controllers
{
    public class NotifyController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMessageHandler _messageHandler;
        private readonly IRestClientApiCall _restClientApiCall;
        public NotifyController(IMessageHandler messageHandler, IRestClientApiCall restClientApiCall, IMapper mapper, IMediator mediator)
        {
            _messageHandler = messageHandler;
            _restClientApiCall = restClientApiCall;
            _mapper = mapper;
            _mediator = mediator;
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

        [HttpGet]
        public ActionResult ValidateAndTransformLotDetail()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ValidateAndTransformLotDetail(ValidateAndTransformLot validateAndTransformLot)
        {
            ValidateAndTransformLotSuccessResponse ValidateAndTransformLotSuccessResponse = new ValidateAndTransformLotSuccessResponse();
            var PlaceBidCommand = _mapper.Map<PlaceBidCommand>(validateAndTransformLot);
            ValidateAndTransformLotSuccessResponse = await _mediator.Send(PlaceBidCommand);
            return Json(ValidateAndTransformLotSuccessResponse);
        }
    }
}
