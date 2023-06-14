using MessengerApp.Client.Sevices.Interfaces;
using MessengerApp.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace MessengerApp.Client.Pages
{
    public class DirectBase : ComponentBase
    {
        [Inject]
        public IUserDirectService UserDirectService { get; set; }
        [Inject]
        public IUserStatusService UserStatusService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public HubConnection HubConnection { get; set; }
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

                HubConnection = new HubConnectionBuilder()
                                    .WithUrl(NavigationManager.ToAbsoluteUri("/directhub"))
                                    .Build();
                HubConnection.On("GetMessageOnChat", GetChatMessages);
                HubConnection.On("GetChat", GetUserChats);
                HubConnection.On("OnConnectToApp", () =>
                {
                    UserStatusService.AddUserToOnlineStatus(new OnlineUserDTO() {
                        OnlineUserId = UserId,
                        OnlineUserConnectionId = HubConnection.ConnectionId}
                    );
                    Console.WriteLine("Connecting!111111111111111111111111111111111111111111111111111");
                });
                HubConnection.On("GetRenamedChat", UpdateChatName);
                await HubConnection.StartAsync();
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
                await HubConnection.SendAsync("AddMessageToChat");

                await Task.Delay(100);
                JSRuntime.InvokeVoidAsync("ScrollChatToBottom", ChatAreaRef);
            }
        }
        public async Task OpenChat(int chatId)
        {
            OpenedChat = UserChats.First(e => e.Id == chatId);
            await GetChatMessages();
            JSRuntime.InvokeVoidAsync("ScrollChatToBottom", ChatAreaRef);
        }
        public async Task CreateChat()
        {
            await UserDirectService.CreateChat(UserId);
            await GetUserChats();
        }
        public async Task LeaveFromChat()
        {
            var userToLeave = new ChatUserDTO() { UserId = UserId, ChatId = OpenedChat.Id };

            await UserDirectService.RemoveUserFromChat(userToLeave);

            OpenedChat = null;

            await GetUserChats();
        }
        private async Task GetChatMessages()
        {
            ChatMessages = (await UserDirectService.GetChatMessages(OpenedChat.Id)).ToList();
            StateHasChanged();
        }
        private async Task GetUserChats()
        {
            UserChats = (await UserDirectService.GetChats(UserId)).ToList();
            StateHasChanged();
        }
        private async Task UpdateChatName()
        {
            await GetUserChats();
            if (OpenedChat != null)
            {
                OpenedChat = UserChats.First(e => e.Id == OpenedChat.Id);
            }
            StateHasChanged();
        }
    }
}
