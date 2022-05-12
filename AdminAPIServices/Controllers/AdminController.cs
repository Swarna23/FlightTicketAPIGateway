using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdminAPIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // GET: api/<AdminController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AdminController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        [HttpGet("Ping")]
        public string Ping()
        {
            return "Hello World";
        }


        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        [HttpGet("GetFlights")]
        [EnableCors("AllowOrigin")]
        public IEnumerable<Models.Flights> GetFlights()
        {
            using (var context = new Models.FlightTicketContext())
            {
                return context.Flights.Where(x=>x.FlightStatus == "Scheduled").ToList();
            }
        }
        [HttpGet("GetBFlights")]
        [EnableCors("AllowOrigin")]
        public IEnumerable<Models.Flights> GetBFlights()
        {
            using (var context = new Models.FlightTicketContext())
            {
                return context.Flights.Where(x => x.FlightStatus == "Blocked").ToList();
            }
        }

        [HttpGet("GetFlightsDetails/{fid}")]
        [EnableCors("AllowOrigin")]
        public IEnumerable<Models.Flights> GetFlightsDetails(string fid)
        {
            using (var context = new Models.FlightTicketContext())
            {
                return context.Flights.Where(x => x.FlightId == fid).ToList();
            }
        }
        [HttpPost("ScheduleAirLine/{fid}/{fname}/{from}/{to}/{dept}/{arr}/{seats}/{cost}/{mealType}/{days}/{status}")]
        [EnableCors("AllowOrigin")]
        public string AddAirline(string fid,string fname,string from,string to,TimeSpan dept,TimeSpan arr,int seats,int cost,string mealType,string days,string status)
        {
            try
            {
                Models.Flights flight = new Models.Flights();
                flight.FlightId = fid;
                flight.AirLine = fname;
                flight.FromPlace = from;
                flight.ToPlace = to;
                flight.Departure =dept;
                flight.Arrival = arr;
                flight.TotalSeats = seats;
                flight.Cost = cost;
                flight.Meal = mealType;
                flight.ScheduleDays = days;
                flight.FlightStatus = status;
                using (var context = new Models.FlightTicketContext())
                {
                    context.Flights.Add(flight);
                    context.SaveChanges();
                    return "true";
                }
            }
            catch (Exception e)
            {
                throw;
                return e.StackTrace;
            }
        }
        [HttpPut("BlockAirline/{flightId}")]
        //[EnableCors("AllowOrigin")]
        public bool CancelAirline(string flightId)
        {
            try
            {
                using (var context = new Models.FlightTicketContext())
                {
                    var airlines = (from i in context.Flights where i.FlightId == flightId select i);
                    foreach (var airline in airlines)
                    {
                        airline.FlightStatus = "Blocked";
                        context.Update(airline);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        [HttpPut("UnBlockAirline/{flightId}")]
        //[EnableCors("AllowOrigin")]
        public bool UnBlockAirline(string flightId)
        {
            try
            {
                using (var context = new Models.FlightTicketContext())
                {
                    var airlines = (from i in context.Flights where i.FlightId == flightId select i);
                    foreach (var airline in airlines)
                    {
                        airline.FlightStatus = "Scheduled";
                        context.Update(airline);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //[HttpPut("BlockAirline/{flightId}")]
        //public bool CancelAirline(int flightId)
        //{
        //    try
        //    {
        //        using (var context = new Models.FlightTicketContext())
        //        {
        //            var airlines = (from i in context.Flights where i.FlightNumber == flightId select i);
        //            foreach (var airline in airlines)
        //            {
        //                airline.BlockStatus = 0;
        //                context.Update(airline);
        //            }

        //            context.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}
    }
}
