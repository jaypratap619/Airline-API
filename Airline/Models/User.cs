using System;
using System.Collections.Generic;

#nullable disable

namespace Airline.Models
{
    public partial class User
    {
        public User()
        {
            Tickets = new HashSet<Ticket>();
        }

        public int Userid { get; set; }
        public string Mobile { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string EmailId { get; set; }
        public string UserPwd { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }

        public class userlogin
        {
            public string email { get; set; }

            public string pass { get; set; }
        }
    }
}
