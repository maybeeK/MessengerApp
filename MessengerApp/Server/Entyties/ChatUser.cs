using MessengerApp.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Server.Entyties
{
    public class ChatUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        public int ChatId { get; set; }
        [ForeignKey("ChatId")]
        public virtual Chat? Chat { get; set; }
    }
}
