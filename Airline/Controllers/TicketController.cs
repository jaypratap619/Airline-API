using Airline.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        AirLineContext ac = new AirLineContext();

        /// <summary>
        /// Get all tickets 
        /// </summary>
        /// <returns></returns>
        // GET: api/<TicketController>
        [HttpGet]
        public IEnumerable<Ticket> Get()
        {
            using (ac)
            {
                return ac.Tickets.ToList();
            }
        }


        /// <summary>
        /// This will delete the ticket 
        /// </summary>
        /// <param name="tickenumber"></param>
        /// <returns></returns>
        // GET api/<TicketController>/5
        [HttpDelete]
        [Route("TDelete")]
        public IActionResult DeleteTicket(string tickenumber)
        {
            try
            {
                using (ac)
                {
                    Ticket t = ac.Tickets.Find(tickenumber);
                    if (t == null)
                    {
                        return NotFound("Ticket is not found");
                    }
                    ac.Tickets.Remove(t);
                    ac.SaveChanges();
                    return Ok("Ticket has been deleted");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("The exception is " + ex);
            }
        }


        /// <summary>
        /// This will book the ticket
        /// </summary>
        /// <param name="Uemail"></param>
        /// <param name="flightnumber"></param>
        /// <param name="type"></param>
        /// <param name="passengers"></param>
        /// <returns></returns>
        // PUT api/<TicketController>/5
        [HttpPost]
        [Route("Book")]
        public IActionResult BookTicket(string Uemail, string flightnumber, string type,[FromBody] Passenger.passdetails[] passengers)
        {

            try
            {
                User u = ac.Users.Find(Uemail);
                if (u == null)
                {
                    return NotFound($"User with {Uemail} not found");
                }
                Flight f = ac.Flights.Find(flightnumber);
                int numberOfPassenger = passengers.Length;

                if (type == "buisness")
                {
                    if (f.SeatsBussiness < numberOfPassenger)
                    {
                        return BadRequest("Seats are not available");
                    }
                    else
                    { 
                        f.SeatsBussiness--;   
                    }
                }else if (type == "economy")
                {
                    if (f.SeatsEco < numberOfPassenger)
                    {
                        return BadRequest("Seats are not available");
                    }
                    else
                    {
                        f.SeatsEco--;
                    }
                }
                else
                {
                    return BadRequest($"{type} class is Invalid");
                }
                Ticket t = new Ticket();
                t.FlightNumber = flightnumber;
                t.EmailId = u.EmailId;
                t.TicketStatus = "Booked";
                t.DateOfIssue = DateTime.Now.Date;
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                var chars2 = "0123456789";
                var stringChars = new char[12];
                var random = new Random();

                for (int i = 0; i < 2; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }
                for (int i = 2; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars2[random.Next(chars2.Length)];
                }
                var finalString = new String(stringChars);

                t.TicketId = finalString;
                ac.Tickets.Add(t);
                ac.SaveChanges();

                for (int i = 0; i < numberOfPassenger; i++)
                {
                    Passenger p = new Passenger();
                    p.TicketId = finalString;
                    p.PFullName = passengers[i].pname;
                    p.PAge = passengers[i].page;

                    ac.Passengers.Add(p);
                    ac.SaveChanges();
                }
                u.Tickets.Add(t);
                return Ok("Tickek booked successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("The exception is " + ex);
            }
        }

      
        /// <summary>
        /// This will cancel Ticket using ticketnumber
        /// </summary>
        /// <param name="tickenumber"></param>
        /// <returns></returns>
        //// DELETE api/<TicketController>/5
        [HttpPut]
        [Route("Cancelled")]
        public IActionResult CancelTicket(string tickenumber)
        {
            try
            {
                using (ac)
                {
                    Ticket t = ac.Tickets.Find(tickenumber);
                    if (t == null)
                    {
                        return NotFound("Ticket is not found");
                    }
                    t.TicketStatus = "Cancelled";
                    ac.SaveChanges();
                    return Ok("Ticket has been cancelled");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("The exception is " + ex);
            }
        }




        /// <summary>
        /// This will get status as booked or cancelled
        /// </summary>
        /// <param name="ticketnumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Status")]
        public IActionResult GetStatus(string ticketnumber)
        {
            try
            {
                using (ac)
                {
                    Ticket data = ac.Tickets.Where(t=>t.TicketId==ticketnumber).FirstOrDefault();
                    if (data == null)
                    {
                        return NotFound("Ticket Not Found");
                    }
                    else
                    {
                        return Ok(data.TicketStatus);
                    }
                }
            }catch(Exception ex)
            {
                return BadRequest("The exception is " + ex);
            }
        }

        
    }
}
