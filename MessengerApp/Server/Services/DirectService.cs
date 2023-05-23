using MessengerApp.Server.Data;
using MessengerApp.Server.Entyties;
using MessengerApp.Server.Services.Interfaces;
using MessengerApp.Shared.DTOs;
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
            return await _context.ChatUsers.Where(e=>e.UserId == userId).Select(e=> new Chat { Id = e.Id}).ToListAsync();
        }

        public async Task<IEnumerable<Message>> GetChatMessages(int chatId)
        {
            return await _context.Messages.Where(e=>e.ChatId==chatId).ToListAsync();
        }
        public async Task<ChatUser> CreateChat(string creatorId)
        {
            var createdChat = await _context.AddAsync(new Chat());
            var chatUser = await AddUserToChat(creatorId, createdChat.Entity.Id);
            await _context.SaveChangesAsync();
            return chatUser;
        }
        public async Task<ChatUser> AddUserToChat(string userId, int chatId)
        {
            if (await IsUserInChat(userId, chatId))
            {
                return null;
            }
            else
            {
                var chatUser = new ChatUser { UserId = userId, ChatId = chatId };
                await _context.ChatUsers.AddAsync(chatUser);
                await _context.SaveChangesAsync();
                return chatUser;
            }
        }
        public async Task<Message> AddMessageToChat(MessageDTO messageDTO)
        {
            var message = new Message {
                Text = messageDTO.Text,
                ChatId = messageDTO.ChatId,
                SenderId = messageDTO.SenderId,
                Time = messageDTO.Time
            };

            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
            
            return message;
        }
        private async Task<bool> IsUserInChat(string userId, int chatId)
        {
           return await _context.ChatUsers.AnyAsync(e => e.UserId == userId && e.ChatId == chatId);
        }
    }
}
