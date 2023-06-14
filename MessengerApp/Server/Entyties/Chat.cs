using MessengerApp.Server.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessengerApp.Server.Entyties
{
    public class Chat
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;

    }
}
