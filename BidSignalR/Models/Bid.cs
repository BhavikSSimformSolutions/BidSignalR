using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BidSignalR.Models
{
    public class Bid
    {
        [DisplayName("Auction Id")]
        public int auctionid { get; set; }

        [DisplayName("Lot Id")]
        public int lotid { get; set; }

        [DisplayName("Plateform Id")]
        public int platformid { get; set; }

        [DisplayName("Market Place Id")]
        public int marketplaceid { get; set; }

        [DisplayName("Bidder Id")]
        public string bidderid { get; set; }

        [DisplayName("Amount")]
        public int amount { get; set; }
    }
}
