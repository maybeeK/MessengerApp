using MessengerApp.Server.Data;
using MessengerApp.Server.Entyties;
using MessengerApp.Server.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MessengerApp.Server.Services
{
    public class DirectService : IDirectService
    {
        private readonly ApplicationDbContext _context;
        public DirectService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Chat>> GetUserChats(string userId)
        {
            return await _context.Chats.Where(e => e.Users.Any(e => e.Id == userId)).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetChatMessages(int chatId)
        {
            var chat = await _context.Chats.FirstOrDefaultAsync(e => e.Id == chatId);
            return chat.Messages;
        }
        public async Task<bool> AddUserToChat(string userId, int chatId)
        {
            //To Do
            return true;
        }
    }
}
