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
    [Route("")]
    [ApiController]
    
    public class FlightController : ControllerBase
    {
        AirLineContext ac = new AirLineContext();

        /// <summary>
        /// This will give Get all flights 
        /// </summary>
        /// <returns></returns>
        // GET: api/<FlightController>
        [HttpGet]
        [Route("GetAllFlights")]
        
        public IActionResult Get()
        {
            try
            {
                //var data = from Flight in ac.Flights
                //           select new
                //           {
                //               FlightNumber = Flight.FlightNumber,
                //               FlightName = Flight.FlightName,
                //               timeOfArrival = Flight.TimeOfArr,
                //               DateOfArrival = Flight.DateOfArr,
                //               timeOfDeparture = Flight.TimeOfDept,
                //               DateOfDeparture = Flight.DateOfDept,
                //               Duration = Flight.Duration,
                //               From = Flight.ArrCity,
                //               To = Flight.DepCity,
                //               PriceOfEco = Flight.PriceEco,
                //               PriceOfBuiss = Flight.PriceBn,
                //               EconomicalSeat = Flight.SeatsEco,
                //               BuisenessSeat = Flight.SeatsBussiness
                //           };
                return Ok(ac.Flights.ToList());
            }catch(Exception ex)
            {
                return BadRequest("The exception occured is " + ex);
            }
        }


        /// <summary>
        /// Getting Flight on basis of flight
        /// </summary>
        /// <param name="flightnumber"></param>
        /// <returns></returns>
        // GET api/<FlightController>/5
        [HttpGet()]
        [Route("GetById")]
        public IActionResult GetById(string flightnumber)
        {
            try
            {
                var data = from Flight in ac.Flights where Flight.FlightNumber == flightnumber select Flight;
                if (data != null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return BadRequest("The exception ouccured is " + ex);
            }
        }

        
        /// <summary>
        /// /*
        ///This is searching flight on basis of
        /// -->One way / Return
        ///-->Departure date
        ///-->From to
        ///
        /// </summary>
        /// <param name="depDate"></param>
        /// <param name="arCity"></param>
        /// <param name="dpCity"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOneWay")]
        public IActionResult GetOneWay(string depDate,string arCity,string dpCity)
        {
            try
            {
                var data = from Flight in ac.Flights where (Flight.ArrCity == arCity &&  Flight.DepCity== dpCity && Flight.DateOfDept.Date.ToString() == (depDate)) select  Flight;
                if(data!=null)
                {
                    return Ok(data);
                }
                else
                {
                    return NotFound("The flight is not present");
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest("The exception ouccured is " + ex);
            }
        }

        /// <summary>
        /// Admin will add flight using this 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/<FlightController>
        [HttpPost]
        [Route("AddFlight")]
        public IActionResult AddFlight([FromBody] Flight value)
        {
            try
            {
                using (ac)
                {
                    ac.Flights.Add(value);
                    ac.SaveChanges();
                    return Created("Flight Added Successfully", value);
                }
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<FlightController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        /// <summary>
        /// Admin Can Delete the flight using this 
        /// </summary>
        /// <param name="flightnumber"></param>
        /// <returns></returns>
        // DELETE api/<FlightController>/5
        [HttpDelete]
        [Route("DeleteFlight")]
        public IActionResult DeleteFlight(string flightnumber)
        {
            try
            {
                using (ac)
                {
                    Flight f = ac.Flights.Find(flightnumber);
                    if (f == null)
                    {
                        return NotFound($"Flight with {flightnumber} is not present");
                    }
                    else
                    {
                        ac.Flights.Remove(f);
                        ac.SaveChanges();
                        return Ok("Flight deleted successfully");
                    }
                }
            }catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// City of arrival
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AllarrCity")]
        public IActionResult getArrCity()
        {
            try
            {
                var data = from Flight in ac.Flights
                           select new
                           {                               
                               From = Flight.ArrCity,         
                           };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest("The exception occured is " + ex);
            }
        }

        /// <summary>
        /// City of departure
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("AlldepCity")]
        public IActionResult getDepCity()
        {
            try
            {
                var data = from Flight in ac.Flights
                           select new
                           {

                               From = Flight.DepCity,

                           };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest("The exception occured is " + ex);
            }
        }
    }
}
