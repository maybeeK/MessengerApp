using MessengerApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Client.Sevices.Interfaces
{
    public interface IUserDirectService
    {
        Task<IEnumerable<ChatDTO>> GetChats(string userId);
        Task<IEnumerable<MessageDTO>> GetChatMessages(int chatId);
        Task<ChatUserDTO> CreateChat(string creatorId);
        Task<ChatUserDTO> AddUserToChat(AddUserToChatDTO userTochat);
        Task<MessageDTO> SendMessage(MessageDTO messageDTO);
        Task<IEnumerable<AppUserDTO>> GetUsersWhichNotInChat(int chatId);
    }
}
