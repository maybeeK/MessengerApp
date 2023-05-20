using MessengerApp.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Server.Entyties
{
    public class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        [ForeignKey("Id")]
        public virtual ApplicationUser Sender { get; set; }
    }
}
