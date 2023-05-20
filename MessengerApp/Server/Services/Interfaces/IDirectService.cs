using MessengerApp.Server.Entyties;

namespace MessengerApp.Server.Services.Interfaces
{
    public interface IDirectService
    {
        Task<IEnumerable<Chat>> GetUserChats(string userId);
        Task<IEnumerable<Message>> GetChatMessages(int chatId);
    }
}
