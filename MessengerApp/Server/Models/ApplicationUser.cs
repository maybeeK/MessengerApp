using MessengerApp.Server.Entyties;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        [ForeignKey("Id")]
        public virtual List<Chat> UserChats { get; set; } = new List<Chat>();
    }
}