using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Shared.DTOs
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public IEnumerable<string>? UsersId { get; set; }
        public IEnumerable<int>? MessagesId { get; set; }
    }
}
