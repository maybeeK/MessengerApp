using MessengerApp.Shared.DTOs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Server.Hubs
{
    public class DirectHub:Hub
    {
        public async Task AddMessageToChat()
        {
            await Clients.All.SendAsync("GetMessageOnChat");
        }
        public async Task AddUserToChat()
        {
            await Clients.All.SendAsync("GetChat");
        }
    }
}
