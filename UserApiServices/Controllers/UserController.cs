using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserApiServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet("Login/{email}/{password}")]
        [EnableCors("AllowOrigin")]
        public int? GetUserorAdmin(string email,string password)
        {
            try
            {
                using (var context = new Models.FlightTicketContext())
                {
                    var role = context.Users.Where(x => x.EmailId == email && x.Password == password).FirstOrDefault();
                    return role.Role;
                }
            }
            catch(Exception e)
            {
                return -1;
            }
        }

        [HttpGet("GetPassengerDetails/{pnr}")]
        [EnableCors("AllowOrigin")]
        public IEnumerable<Models.PassengerDetails> GetTicketByPNR(long pnr)
        {
            using (var context = new Models.FlightTicketContext())
            {
                return context.PassengerDetails.Where(x => x.Pnr == pnr).ToList();
            }
        }
        [HttpGet("GetTicket")]
        public IEnumerable<Models.TicketBooking> GetTicket()
        {
            using (var context = new Models.FlightTicketContext())
            {
                return context.TicketBooking.ToList();
            }
        }
        [HttpGet("GetBookedTickets/{email}")]
        [EnableCors("AllowOrigin")]
        public List<Models.TicketBooking> GetBookedTicketByEmailId(string email)
        {
            using (var context = new Models.FlightTicketContext())
            {
                return context.TicketBooking.Where(x => x.EmailId == email).ToList();
            }
        }
        List<Models.Flights> searchedFlights = new List<Models.Flights>();
        [HttpGet("SearchFlights/{fromPlace}/{toPlace}")]
        [EnableCors("AllowOrigin")]
        public List<Models.Flights> SearchFlights(string fromPlace, string toPlace)
        {
            using (var context = new Models.FlightTicketContext())
            {
                var flights = context.Flights.Where(x => x.FlightStatus=="Scheduled" && x.FromPlace == fromPlace && x.ToPlace == toPlace).ToList();
                foreach (var flight in flights)
                {
                    searchedFlights.Add(flight);
                }
                return flights;
            }
        }
        //[HttpPost("BookTickets")]
        //public bool BookTicket(Models.BookedTicket bookedTicket)
        //{
        //    try
        //    {

        //        using(var context = new Models.FlightTicketContext())
        //        {
        //            context.BookedTicket.Add(bookedTicket);

        //            context.SaveChanges();
        //            //var pnr = (from i in context.BookedTicket where i.UserName == bookedTicket.UserName
        //            //           orderby i.Pnr
        //            //           select i.Pnr).LastOrDefault();
        //            //return pnr;
        //            return true;
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        return false;
        //    }
        //} 
        //[HttpPost("BookTickets/{flightId}/{mailId}/{seats}/{meal}/{deptDate}/{arrDate}")]
        //public string BookTicket(string fID,string mailId, int seats, string meal,DateTime deptDate,DateTime arrDate, List<Models.PassengerDetails> passenger)
        //{
        //    try
        //    {
        //        Models.TicketBooking ticket = new Models.TicketBooking();
        //        ticket.EmailId = mailId;
        //        ticket.NoOfSeats = seats;
        //        ticket.DepartureDate = deptDate;
        //        ticket.ArrivalDate = arrDate;
        //        ticket.Meal = meal;
        //        ticket.TimeOfBooking = DateTime.Now;
        //        ticket.FlightId = fID;
        //        ticket.BookingStatus = "Booked";
        //        long pnr;
        //        string flightID;
        //        using (var context = new Models.FlightTicketContext())
        //        {
        //            context.TicketBooking.Add(ticket);
        //            context.SaveChanges();
        //            pnr = (long)(from i in context.TicketBooking
        //                         where i.EmailId == ticket.EmailId
        //                         orderby i.Pnr
        //                         select i.Pnr).LastOrDefault();
        //            flightID = fID;
        //            if (String.Equals(PassengerDetails(passenger, pnr, flightID),"true"))
        //            {
        //                //context.SaveChanges();
        //                return "Passenger Added. Ticket Booked!!";
        //            }
        //            else
        //                return "Error in Adding Passenger";
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return "Some error occurred";
        //    }
        //}
        [HttpPost("PassengerDetails")]
        public string PassengerDetails(string passName,int age,string passGender,int seat, long PNR,string flightID)
        {
            try
            {
                //for (int i = 0; i < passengers.Count; i++)
                //{
                    Models.PassengerDetails traveller = new Models.PassengerDetails();
                traveller.PassengerAge = age;
                traveller.PassengerGender = passGender;
                traveller.PassengerName = passName;
                    traveller.SeatNumber = seat;
                    traveller.FlightId = flightID;
                    traveller.Pnr = PNR;
                    using (var context = new Models.FlightTicketContext())
                    {
                    context.PassengerDetails.Add(traveller);
                        context.SaveChanges();
                    }
                //}
                return "true";
            }
            catch (Exception e)
            {
                return e.StackTrace;
            }
        }
        [HttpGet("Check/{flight}/{mail}")]
        public string Check(string flight,string mail)
        {
            return flight;
        }
        [HttpPost("BookTickets/{flightId}/{mailId}/{seats}/{meal}/{deptDate}/{arrDate}/{passName}/{age}/{passGender}/{seat}")]
        public string BookTicket(string flightId, string mailId, int seats, string meal, DateTime deptDate, DateTime arrDate, string passName,int age, string passGender, int seat)
        //public string BookTicket(string flightId, string mailId, int seats, string meal, DateTime deptDate, DateTime arrDate, List<Models.PassengerDetails> passenger)
        {
            try
            {
                Models.TicketBooking ticket = new Models.TicketBooking();
                ticket.EmailId = mailId;
                ticket.NoOfSeats = seats;
                ticket.DepartureDate = deptDate;
                ticket.ArrivalDate = arrDate;
                ticket.Meal = meal;
                ticket.TimeOfBooking = DateTime.Now;
                ticket.FlightId = flightId;
                ticket.BookingStatus = "Booked";
                long pnr;
                using (var context = new Models.FlightTicketContext())
                {
                    context.TicketBooking.Add(ticket);
                    context.SaveChanges();
                    pnr = (long)(from i in context.TicketBooking
                                 where i.EmailId == ticket.EmailId
                                 orderby i.Pnr
                                 select i.Pnr).LastOrDefault();
                    //flightID = flightId;
                    if (String.Equals(PassengerDetails(passName,age,passGender,seat, pnr, flightId), "true"))
                    {
                        //context.SaveChanges();
                        using (var con = new Models.FlightTicketContext())
                        {
                            var flightDetail = (from i in context.Flights where i.FlightId == flightId select i).FirstOrDefault();
                            flightDetail.TotalSeats = flightDetail.TotalSeats - seats;
                            con.Update(flightDetail);
                            con.SaveChanges();

                        }
                        return "Passenger Added. Ticket Booked!!";
                    }
                    else
                        return "Error in Adding Passenger";
                }
            }
            catch (Exception e)
            {
                return "Some error occurred";
            }
        }

        [HttpDelete("CancelTicket/{PNR}")]
        public IActionResult CancelTicket(long PNR)
        {
            try
            {
                using (var context = new Models.FlightTicketContext())
                {
                    var tickets = (from p in context.TicketBooking where p.Pnr == PNR select p).FirstOrDefault();
                    TimeSpan value = DateTime.Now.Subtract((DateTime)tickets.DepartureDate);
                    if (value.Days <1 )
                        tickets.BookingStatus = "Cancelled";
                    else
                        return Ok("Sorry, Ticket cannot be cancelled at this moment");
                    var cancelledTickets = context.PassengerDetails.Where(x => x.Pnr == PNR).ToList();
                    context.PassengerDetails.RemoveRange(cancelledTickets);
                    context.SaveChanges();
                    // return "Ticket Cancelled";
                    return Ok("Ticket Cancelled");

                }
                
            }

            catch (Exception e)
            {
                return Ok("Soory, Something went wrong");
            }
        }
    }
}
