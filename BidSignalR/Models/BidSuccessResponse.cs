using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidSignalR.Models
{
    public class BidSuccessResponse
    {
        public bool isValid { get; set; }
        public Validationresult[] validationResults { get; set; }
        public Lotdetail lotDetail { get; set; }
        public Biddingstate[] biddingStates { get; set; }
    }

    public class Lotdetail
    {
        public int auctionId { get; set; }
        public int lotId { get; set; }
        public string biddingType { get; set; }
        public int openingPrice { get; set; }
        public object reservePrice { get; set; }
        public int buyItNow { get; set; }
        public string currency { get; set; }
        public object incrementType { get; set; }
        public Increment[] increment { get; set; }
        public int quantity { get; set; }
        public bool isPiecemeal { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endsFrom { get; set; }
        public string timeZone { get; set; }
        public int extensionTimeInSeconds { get; set; }
        public bool biddingSuspended { get; set; }
    }

    public class Increment
    {
        public int low { get; set; }
        public int high { get; set; }
        public int incrementValue { get; set; }
    }

    public class Validationresult
    {
        public int code { get; set; }
        public string value { get; set; }
        public string description { get; set; }
    }

    public class Biddingstate
    {
        public string id { get; set; }
        public int sequenceNumber { get; set; }
        public Action action { get; set; }
        public State state { get; set; }
        public Previousstate previousState { get; set; }
    }

    public class Action
    {
        public string actorType { get; set; }
        public string actorId { get; set; }
        public string actionType { get; set; }
        public string actionResult { get; set; }
        public DateTime timestamp { get; set; }
    }

    public class State
    {
        public int maxBid { get; set; }
        public string bidderId { get; set; }
        public string leadBidderId { get; set; }
        public int currentBid { get; set; }
        public object isReservedMet { get; set; }
        public int minimumBid { get; set; }
        public DateTime endTime { get; set; }
        public int endTimeExtensionCount { get; set; }
        public int activeBids { get; set; }
        public string activeBidsUrl { get; set; }
        public string status { get; set; }
    }

    public class Previousstate
    {
        public object link { get; set; }
        public object _ref { get; set; }
    }
}
