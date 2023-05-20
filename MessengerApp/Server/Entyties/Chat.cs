using MessengerApp.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Server.Entyties
{
    public class Chat
    {
        public int Id { get; set; }

        [ForeignKey("Id")]
        public virtual List<ApplicationUser>? Users { get; set; } = new List<ApplicationUser>();

        [ForeignKey("Id")]
        public virtual List<Message>? Messages { get; set; } = new List<Message>();
    }
}
