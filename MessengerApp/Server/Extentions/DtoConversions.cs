using MessengerApp.Server.Entyties;
using MessengerApp.Shared.DTOs;

namespace MessengerApp.Server.Extentions
{
    public static class DtoConversions
    {
        public static IEnumerable<ChatDTO> ConvertToDto(this IEnumerable<Chat> chats)
        {
            return (from chat in chats
                    select new ChatDTO
                    {
                        Id = chat.Id,
                        MessagesId = chat.Messages?.Select(m => m.Id).ToList(),
                        UsersId = chat.Users?.Select(m => m.Id).ToList()
                    });
        }

        public static IEnumerable<MessageDTO> ConvertToDto(this IEnumerable<Message> messages)
        {
            return (from message in messages
                    select new MessageDTO
                    {
                        Id = message.Id,
                        SenderId = message.Sender.Id,
                        Text = message.Text
                    });
        }
    }
}
