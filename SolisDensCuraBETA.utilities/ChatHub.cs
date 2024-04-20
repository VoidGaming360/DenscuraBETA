using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SolisDensCuraBETA.utilities
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Broadcast the received message to all clients
            await Clients.All.SendAsync("ReceiveMessage", user, message, DateTime.Now);
        }
    }
}
