using MessengerApp.Server.Data;
using MessengerApp.Server.Entyties;
using MessengerApp.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddUserToOnlineStatus(string userId, string userConnId)
        {
            await _context.OnlineUsers.AddAsync(new OnlineUser { OnlineUserId = userId, OnlineUserConnection = userConnId});
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserOnline(string userId)
        {
            return await _context.OnlineUsers.AnyAsync(e=>e.OnlineUserId==userId);
        }

        public async Task RemoveUserFromOnlineStatus(string userConnId)
        {
            var user = _context.OnlineUsers.Where(e=>e.OnlineUserConnection== userConnId);
            if (user!=null)
            {
                _context.OnlineUsers.RemoveRange(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
