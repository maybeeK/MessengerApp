using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Server.Services.Interfaces
{
    public interface IUserService
    {
        public Task AddUserToOnlineStatus(string userId, string userConnId);
        public Task RemoveUserFromOnlineStatus(string userId);
        public Task<bool> IsUserOnline(string userId);
    }
}
