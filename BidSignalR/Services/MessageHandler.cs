using BidSignalR.ConHub;
using BidSignalR.Models;
using BidSignalR.Services.IServices;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidSignalR.Services
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IHubContext<ConnectionHub> _hubContext;

        public MessageHandler(IHubContext<ConnectionHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public void BroadcastMessage(Bid bid)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", JsonConvert.SerializeObject(bid));
        }

        public async Task Notify(Bid bid)
        {
            await _hubContext.Clients.Group(bid.bidderid).SendAsync("ReceiveMessage", JsonConvert.SerializeObject(bid));
        }
    }
}
