using AutoMapper;
using BidSignalR.Handler.Bid.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BidSignalR.Models
{
    public class ValidateAndTransformLot
    {
        [DisplayName("Auction Id")]
        public int auctionId { get; set; }
        [DisplayName("Lot Id")]
        public int lotId { get; set; }
        [DisplayName("Bidding Type")]
        public int biddingType { get; set; }
        [DisplayName("Opening Price")]
        public int openingPrice { get; set; }
        [DisplayName("Reserve Price")]
        public int reservePrice { get; set; }
        [DisplayName("But It Now")]
        public int buyItNow { get; set; }
        [DisplayName("Currency")]
        public string currency { get; set; }
        [DisplayName("Increment Type")]
        public string incrementType { get; set; }
        [DisplayName("Increment")]
        public List<Increment> increment { get; set; }
        [DisplayName("Quantity")]
        public int quantity { get; set; }
        [DisplayName("Is Piecemeal")]
        public bool isPiecemeal { get; set; }
        [DisplayName("Start Time")]
        public DateTime startTime { get; set; }
        [DisplayName("Ends From")]
        public DateTime endsFrom { get; set; }
        [DisplayName("Time Zone")]
        public string timeZone { get; set; }
        [DisplayName("Extension Time in Seconds")]
        public int extensionTimeInSeconds { get; set; }
        [DisplayName("Bidding Suspended")]
        public bool biddingSuspended { get; set; }
    }       
    public class ValidationResult
    {
        public int code { get; set; }
        public string value { get; set; }
        public string description { get; set; }
    }
    public class LotDetail
    {
        public int auctionId { get; set; }
        public int lotId { get; set; }
        public string biddingType { get; set; }
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
    }

    public class ValidateAndTransformLotSuccessResponse
    {
        public List<ValidationResult> validationResults { get; set; }
        public LotDetail lotDetail { get; set; }
    }
    public class BidV2ModelProfile : Profile
    {
        public BidV2ModelProfile()
        {
            CreateMap<PlaceBidCommand, ValidateAndTransformLot>().ReverseMap();
        }
    }
}
