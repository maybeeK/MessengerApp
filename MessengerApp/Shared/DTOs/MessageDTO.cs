using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Shared.DTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public string SenderId { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
    }
}
