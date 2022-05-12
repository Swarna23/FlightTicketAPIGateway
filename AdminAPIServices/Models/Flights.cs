using System;
using System.Collections.Generic;

namespace AdminAPIServices.Models
{
    public partial class Flights
    {
        public Flights()
        {
            PassengerDetails = new HashSet<PassengerDetails>();
            TicketBooking = new HashSet<TicketBooking>();
        }

        public string FlightId { get; set; }
        public string AirLine { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public TimeSpan? Departure { get; set; }
        public TimeSpan? Arrival { get; set; }
        public int? TotalSeats { get; set; }
        public int? Cost { get; set; }
        public string Meal { get; set; }
        public string ScheduleDays { get; set; }
        public string FlightStatus { get; set; }

        public ICollection<PassengerDetails> PassengerDetails { get; set; }
        public ICollection<TicketBooking> TicketBooking { get; set; }
    }
}
