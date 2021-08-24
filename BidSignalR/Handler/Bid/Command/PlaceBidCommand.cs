using AutoMapper;
using BidSignalR.Constants;
using BidSignalR.Models;
using BidSignalR.Services.IServices;
using MediatR;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BidSignalR.Handler.Bid.Command
{
    public class PlaceBidCommand : IRequest<ValidateAndTransformLotSuccessResponse>
    {
        public int auctionId { get; set; }
        public int lotId { get; set; }
        public int biddingType { get; set; }
        public int openingPrice { get; set; }
        public int reservePrice { get; set; }
        public int buyItNow { get; set; }
        public string currency { get; set; }
        public string incrementType { get; set; }
        public List<Increment> increment { get; set; }
        public int quantity { get; set; }
        public bool isPiecemeal { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endsFrom { get; set; }
        public string timeZone { get; set; }
        public int extensionTimeInSeconds { get; set; }
        public bool biddingSuspended { get; set; }

        public class PlaceBidCommandHandler : IRequestHandler<PlaceBidCommand, ValidateAndTransformLotSuccessResponse>
        {
            private readonly IRestClientApiCall _restClientApiCall;
            private readonly IMapper _mapper;
            public PlaceBidCommandHandler(IRestClientApiCall restClientApiCall, IMapper mapper)
            {
                _restClientApiCall = restClientApiCall;
                _mapper = mapper;
            }
            public async Task<ValidateAndTransformLotSuccessResponse> Handle(PlaceBidCommand request, CancellationToken cancellationToken)
            {
                ValidateAndTransformLotSuccessResponse SuccessResponse = new ValidateAndTransformLotSuccessResponse();
                var BidV2Model = _mapper.Map<ValidateAndTransformLot>(request);

                var RestRequest = new RestRequest(Method.POST);
                RestRequest.AddHeader("accept", "*/*");
                RestRequest.AddHeader("Atg-Client-Id", "1");
                RestRequest.AddHeader("Atg-Client-Ip", "1");
                RestRequest.AddHeader("Atg-App-Id", "1");
                RestRequest.AddHeader("Atg-User-Id", "1");
                RestRequest.AddHeader("Content-Type", "application/json");
                RestRequest.AddParameter("application/json", JsonConvert.SerializeObject(BidV2Model), ParameterType.RequestBody);

                IRestResponse response = _restClientApiCall.Execute(RestRequest, ApplicationContants.BID_APIV2);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    SuccessResponse = JsonConvert.DeserializeObject<ValidateAndTransformLotSuccessResponse>(response.Content);
                }
                else if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
                {
                    SuccessResponse.validationResults = JsonConvert.DeserializeObject<List<ValidationResult>>(response.Content);
                }

                return SuccessResponse;
            }
        }
    }
}
