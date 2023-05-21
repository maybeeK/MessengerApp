using MessengerApp.Server.Entyties;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual List<ChatUser> UserChats { get; set; } = new List<ChatUser>();
    }
}