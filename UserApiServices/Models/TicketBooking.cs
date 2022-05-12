using System;
using System.Collections.Generic;

namespace UserApiServices.Models
{
    public partial class TicketBooking
    {
        public TicketBooking()
        {
            PassengerDetails = new HashSet<PassengerDetails>();
        }

        public long? Pnr { get; set; }
        public string EmailId { get; set; }
        public string FlightId { get; set; }
        public int? NoOfSeats { get; set; }
        public string Meal { get; set; }
        public DateTime? DepartureDate { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public DateTime? TimeOfBooking { get; set; }
        public string BookingStatus { get; set; }

        public Users Email { get; set; }
        public Flights Flight { get; set; }
        public ICollection<PassengerDetails> PassengerDetails { get; set; }
    }
}
