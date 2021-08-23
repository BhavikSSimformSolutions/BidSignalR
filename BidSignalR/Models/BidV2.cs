using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidSignalR.Models
{
    public class BidV2Model
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

    public class BidV2SuccessResponse
    {
        public List<ValidationResult> validationResults { get; set; }
        public LotDetail lotDetail { get; set; }
    }


}
