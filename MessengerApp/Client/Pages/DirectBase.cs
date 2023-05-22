using MessengerApp.Client.Sevices.Interfaces;
using MessengerApp.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MessengerApp.Client.Pages
{
    public class DirectBase: ComponentBase
    {
        [Inject]
        public IUserDirectService UserDirectService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public string? UserId { get; set; }
        public List<ChatDTO>? UserChats { get; set;}
        public List<MessageDTO>? ChatMessages { get; set;}

        protected override async Task OnInitializedAsync()
        {
            try
            {
                var user = (await AuthenticationStateProvider.GetAuthenticationStateAsync()).User.Identity?.Name;

                UserChats = (await UserDirectService.GetChats(UserId)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
