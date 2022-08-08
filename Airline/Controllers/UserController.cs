using Airline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Airline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        AirLineContext ac = new AirLineContext();

        /// <summary>
        /// Getting all user
        /// </summary>
        /// <returns></returns>
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            using (ac)
            {
                return ac.Users.ToList();
            }
        }


        /// <summary>
        /// This is for new registraion to the app
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/<UserController>
        [HttpPost]
        [Route("SignUp")]
        public IActionResult SignUp([FromBody] User value)
        {
            if (ModelState.IsValid)
            {
                try
                {                   
                    ac.Users.Add(value);
                    ac.SaveChanges();
                    return Created("Record Successfully added", value);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }
            return BadRequest("Something went wrong while while sign-up.");
        }

        /// <summary>
        /// Login into app
        /// </summary>
        /// <param name="userlogin"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(User.userlogin userlogin)
        {
            string email = userlogin.email;
            string pass = userlogin.pass;
            try
            {
                using (ac)
                {
                    User u = ac.Users.Find(email);
                    if (u == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if (u.UserPwd == pass)
                        {
                            return Created("Found The User", u);
                        }
                        else
                        {
                            return BadRequest("Invalid creadential");
                        }
                    }
                }
            }catch(Exception ex)
            {
                return BadRequest("Something went wrong while login. \n " + ex); 
            }
        }


        /// <summary>
        /// If user wants to change password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult changePassword(string email,string pass)
        {
            try
            {
                if (email == null)
                {
                    return BadRequest("Email cannot be null");
                }
                var data = ac.Users.Where(d => d.EmailId == email).FirstOrDefault();

                if (data == null)
                {
                    return NotFound($"User Does not exist");
                }

                User ouser = ac.Users.Find(data.EmailId);
                ouser.UserPwd = pass;
                ac.SaveChanges();
                return Ok($"Hey {ouser.FirstName} your password has been updated successfully");
            }catch(Exception ex)
            {
                return BadRequest("Something went wrong while changing password. \n "+ex);
            }

        }

        /// <summary>
        /// If user want to see its bookings
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetUserTicket")]
        public IActionResult GetUserTicket(string email)
        {
            try
            {
                User u = ac.Users.Find(email);
                if (u == null)
                {
                    return NotFound($"User with email {email} is not found");
                }
                using (ac)
                {

                    var data = ac.Tickets.Where(t => t.EmailId == email).ToList();
                    if (data == null)
                    {
                        return NotFound($"{u.FirstName} you have no bookings yet.");
                    }
                    Object[] arr = new object[data.Count];

                    int i = 0;
                    foreach (var item in data)
                    {






                        Flight f = ac.Flights.Find(item.FlightNumber);

                        //List<Object> list = new List<object>();
                        Ticket.TicketDetails td = new Ticket.TicketDetails();

                        td.arrCity = f.ArrCity;
                        td.depCity = f.DepCity;
                        td.dateDep = f.DateOfDept;
                        td.dateArr = f.DateOfArr;
                        td.arrTime = f.TimeOfArr;
                        td.depTime = f.TimeOfDept;
                        td.FlightName = f.FlightName;
                        td.FlightNumber = f.FlightNumber;
                        td.TicketId = item.TicketId;
                        td.TicketStatus = item.TicketStatus;
                        td.duration = f.Duration;
                        PassengerController p = new PassengerController();

                        var pass = p.getBId(item.TicketId).ToArray();
                        td.passengers = pass;
                        arr[i] = td;
                        i++;


                    }
                    return Ok(arr);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
