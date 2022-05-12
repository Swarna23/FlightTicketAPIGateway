using System;
using System.Collections.Generic;

namespace AdminAPIServices.Models
{
    public partial class PassengerDetails
    {
        public int Sno { get; set; }
        public long? Pnr { get; set; }
        public string PassengerName { get; set; }
        public int? PassengerAge { get; set; }
        public string PassengerGender { get; set; }
        public int? SeatNumber { get; set; }
        public string FlightId { get; set; }

        public Flights Flight { get; set; }
        public TicketBooking PnrNavigation { get; set; }
    }
}
