using MessengerApp.Shared.DTOs;

namespace MessengerApp.Client.Sevices.Interfaces
{
    public interface IUserStatusService
    {
        public Task AddUserToOnlineStatus(OnlineUserDTO onlineUserDTO);
        public Task RemoveUserFromOnlineStatus(string connectionId);
        public Task<bool> IsUserOnline(string userId);
    }
}
