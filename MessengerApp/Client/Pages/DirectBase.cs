using MessengerApp.Client.Sevices.Interfaces;
using MessengerApp.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MessengerApp.Client.Pages
{
    public class DirectBase : ComponentBase
    {
        [Inject]
        public IUserDirectService UserDirectService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public string? UserId { get; set; }
        public List<ChatDTO>? UserChats { get; set; }
        public List<MessageDTO>? ChatMessages { get; set; }
        public ChatDTO? OpenedChat { get; set; }

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
        public async Task OpenChat(int chatId)
        {
            OpenedChat = UserChats.First(e => e.Id == chatId);
            StateHasChanged();
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
