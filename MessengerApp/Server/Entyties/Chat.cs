using MessengerApp.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Server.Entyties
{
    public class Chat
    {
        public int Id { get; set; }

        [ForeignKey("Id")]
        public List<ApplicationUser>? Users { get; set; }

        [ForeignKey("Id")]
        public List<Message>? Messages { get; set; }
    }
}
