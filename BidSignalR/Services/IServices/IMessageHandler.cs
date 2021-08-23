using BidSignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BidSignalR.Services.IServices
{
    public interface IMessageHandler
    {
        /// <summary>
        /// Send message to specific group.
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        Task Notify(Bid bid);

        /// <summary>
        /// Send Message to all connected clients.
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        void BroadcastMessage(Bid bid);
    }
}
