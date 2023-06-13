using MessengerApp.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Server.Entyties
{
    public class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public int ChatId { get; set; }

        [ForeignKey("ChatId")]
        public virtual Chat? Chat { get; set; }

        [NotMapped]
        public string SenderName { get; set; }

        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }
    }
}
