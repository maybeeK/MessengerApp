using MessengerApp.Server.Entyties;
using MessengerApp.Server.Models;
using MessengerApp.Shared.DTOs;

namespace MessengerApp.Server.Extentions
{
    public static class DtoConversions
    {
        public static IEnumerable<AppUserDTO> ConvertToDto(this IEnumerable<ApplicationUser> users)
        {
            return (from user in users
                    select new AppUserDTO
                    {
                        Id= user.Id,
                        Email = user.Email
                    }).ToList();
        }

        public static IEnumerable<ChatDTO> ConvertToDto(this IEnumerable<Chat> chats)
        {
            return (from chat in chats
                   select new ChatDTO { 
                        Id= chat.Id
                   }).ToList();
        }

        public static IEnumerable<MessageDTO> ConvertToDto(this IEnumerable<Message> messages)
        {
            return (from message in messages
                    select new MessageDTO
                    {
                        Id = message.Id,
                        Text = message.Text,
                        ChatId = message.ChatId,
                        SenderId = message.SenderId,
                        SenderName = message.SenderName,
                        Time = message.Time
                    }).ToList();
        }

        public static IEnumerable<ChatUserDTO> ConvertToDto(this IEnumerable<ChatUser> chatUsers)
        {
            return (from user in chatUsers 
                    select new ChatUserDTO {
                        Id = user.Id,
                        ChatId = user.ChatId,
                        UserId = user.UserId
                    }).ToList();
        }

        public static ChatUserDTO ConvertToDto(this ChatUser user)
        {
            return new ChatUserDTO {
                UserId= user.UserId,
                ChatId= user.ChatId,
                Id= user.Id
            };
        }
    }
}
