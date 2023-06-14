using MessengerApp.Client.Sevices.Interfaces;
using MessengerApp.Server.Data;
using MessengerApp.Server.Entyties;
using MessengerApp.Server.Models;
using MessengerApp.Server.Services.Interfaces;
using MessengerApp.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MessengerApp.Server.Services
{
    public class DirectService : IDirectService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEncryptionService _encryptionService;
        public DirectService(ApplicationDbContext context, IEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
        }
        public async Task<IEnumerable<Chat>> GetUserChats(string userId)
        {
            return await (from chatUser in _context.ChatUsers
                          where chatUser.UserId == userId
                          join chat in _context.Chats
                          on chatUser.ChatId equals chat.Id
                          select new Chat() { Id = chat.Id, Name = chat.Name }).ToListAsync();
        }
        public async Task<IEnumerable<Message>> GetChatMessages(int chatId)
        {
            var messages = await _context.Messages.Where(e => e.ChatId == chatId).ToListAsync();
            await Parallel.ForEachAsync(messages, new ParallelOptions() { MaxDegreeOfParallelism = 1 }, async (message, _) =>
            {
                message.SenderName = (await _context.Users.FirstAsync(e => e.Id == message.SenderId)).UserName;
                message.Text = await _encryptionService.DecryptAsync(message.Text, message.SenderId);
            });
            return messages;
        }
        public async Task<ChatUser> CreateChat(string creatorId)
        {
            var chatToCreate = new Chat();
            await _context.Chats.AddAsync(chatToCreate);
            await _context.SaveChangesAsync();
            var chatUser = await AddUserToChat(creatorId, chatToCreate.Id);
            return chatUser;
        }
        public async Task<ChatUser> AddUserToChat(string userId, int chatId)
        {
            var chatUser = new ChatUser { UserId = userId, ChatId = chatId };
            await _context.ChatUsers.AddAsync(chatUser);
            await _context.SaveChangesAsync();
            return chatUser;
        }
        public async Task<Message> AddMessageToChat(MessageDTO messageDTO)
        {
            var encryptedMessage = new Message
            {
                Text = await _encryptionService.EncryptAsync(messageDTO.Text, messageDTO.SenderId),
                ChatId = messageDTO.ChatId,
                SenderId = messageDTO.SenderId,
                Time = messageDTO.Time
            };

            await _context.Messages.AddAsync(encryptedMessage);
            await _context.SaveChangesAsync();

            return encryptedMessage;
        }
        private async Task<bool> IsUserInChat(string userId, int chatId)
        {
            return await _context.ChatUsers.AnyAsync(e => e.UserId == userId && e.ChatId == chatId);
        }
        public async Task<IEnumerable<ApplicationUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<IEnumerable<ChatUser>> GetChatUsers(int chatId)
        {
            return await _context.ChatUsers.Where(e => e.ChatId == chatId).ToListAsync();
        }
        public async Task<ChatUser> RemoveUserFromChat(ChatUser chatUserToDelete)
        {
            var item = await _context.ChatUsers.FirstOrDefaultAsync(e => e.UserId == chatUserToDelete.UserId && e.ChatId == chatUserToDelete.ChatId);

            if (item != null)
            {
                _context.ChatUsers.Remove(item);
                await _context.SaveChangesAsync();
            }

            return item;
        }

        public async Task<Chat> RenameChat(ChatDTO renamedChat)
        {
            var chatToRename = await _context.Chats.FindAsync(renamedChat.Id);

            if (chatToRename != null)
            {
                chatToRename.Name = renamedChat.Name;
                await _context.SaveChangesAsync();
                return chatToRename;
            }

            return null;
        }
    }
}
