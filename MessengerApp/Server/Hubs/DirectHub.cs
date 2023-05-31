using MessengerApp.Server.Models;
using MessengerApp.Server.Services.Interfaces;
using MessengerApp.Shared.DTOs;
using Microsoft.AspNet.Identity;
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
        private readonly IUserService _userService;
        public DirectHub(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("OnConnectToApp");
            Console.WriteLine("Connecting!111111111111111111111111111111111111111111111111111");

            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connId = Context.ConnectionId;
            await _userService.RemoveUserFromOnlineStatus(connId);
            
            Console.WriteLine("Disconnecting!111111111111111111111111111111111111111111111111111");

            await base.OnDisconnectedAsync(exception);
        }
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
