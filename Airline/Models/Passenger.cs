using System;
using System.Collections.Generic;

#nullable disable

namespace Airline.Models
{
    public partial class Passenger
    {
        public int PId { get; set; }
        public string PFullName { get; set; }
        public int? PAge { get; set; }
        public string TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }

        public class passdetails
        {
            public string pname { get; set; }
            public int? page { get; set; }
        }
      
    }



}
