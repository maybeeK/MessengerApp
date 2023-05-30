using MessengerApp.Server.Entyties;
using MessengerApp.Server.Models;
using MessengerApp.Shared.DTOs;

namespace MessengerApp.Server.Services.Interfaces
{
    public interface IDirectService
    {
        Task<IEnumerable<ApplicationUser>> GetUsers();
        Task<IEnumerable<ChatUser>> GetChatUsers(int chatId);
        Task<IEnumerable<Chat>> GetUserChats(string userId);
        Task<IEnumerable<Message>> GetChatMessages(int chatId);
        Task<ChatUser> CreateChat(string creatorId);
        Task<ChatUser> AddUserToChat(string userId, int chatId);
        Task<Message> AddMessageToChat(MessageDTO messageDTO);
        Task<ChatUser> RemoveUserFromChat(ChatUser chatUserToDelete);
    }
}
