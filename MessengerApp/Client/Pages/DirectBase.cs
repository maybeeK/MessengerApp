using MessengerApp.Client.Sevices.Interfaces;
using MessengerApp.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace MessengerApp.Client.Pages
{
    public class DirectBase : ComponentBase
    {
        [Inject]
        public IUserDirectService UserDirectService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public string? UserId { get; set; }
        public List<ChatDTO>? UserChats { get; set; }
        public List<MessageDTO>? ChatMessages { get; set; }
        public ChatDTO? OpenedChat { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public ElementReference ChatAreaRef;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                var state = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                UserId = state.User.FindFirst(e => e.Type == "sub")?.Value;
                await GetUserChats();
                ChatMessages = new();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task SendMessage()
        {
            if (MessageText != string.Empty)
            {
                var messageToSendDTO = new MessageDTO()
                {
                    Text = MessageText,
                    ChatId = OpenedChat.Id,
                    SenderId = UserId,
                    Time = DateTime.Now
                };

                Console.WriteLine($"Message to send:\n{messageToSendDTO.Text}\n{messageToSendDTO.ChatId}\n{messageToSendDTO.SenderId}\n{(messageToSendDTO.Time.ToString("hh:mm:ddd:yyyy"))}");

                await UserDirectService.SendMessage(messageToSendDTO);
                MessageText = string.Empty; 
            }
        }
        public async Task OpenChat(int chatId)
        {
            OpenedChat = UserChats.First(e => e.Id == chatId);
            ChatMessages = (await UserDirectService.GetChatMessages(OpenedChat.Id)).ToList();
            StateHasChanged();
            JSRuntime.InvokeVoidAsync("ScrollChatToBottom", ChatAreaRef);
        }
        public async Task CreateChat()
        {
            await UserDirectService.CreateChat(UserId);
            await GetUserChats();
            StateHasChanged();
        }
        private async Task GetUserChats()
        {
            UserChats = (await UserDirectService.GetChats(UserId)).ToList();
        }
    }
}
