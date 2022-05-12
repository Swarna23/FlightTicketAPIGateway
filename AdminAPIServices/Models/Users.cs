using System;
using System.Collections.Generic;

namespace AdminAPIServices.Models
{
    public partial class Users
    {
        public Users()
        {
            TicketBooking = new HashSet<TicketBooking>();
        }

        public string EmailId { get; set; }
        public string Password { get; set; }
        public int? Role { get; set; }

        public ICollection<TicketBooking> TicketBooking { get; set; }
    }
}
