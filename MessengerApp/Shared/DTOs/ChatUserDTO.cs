using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Shared.DTOs
{
    public class ChatUserDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ChatId { get; set; }

    }
}
