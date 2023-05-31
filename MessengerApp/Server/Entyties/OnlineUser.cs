using MessengerApp.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerApp.Server.Entyties
{
    public class OnlineUser
    {
        public string OnlineUserConnection { get; set; }
        public string OnlineUserId { get; set; }

        [ForeignKey("OnlineUserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
