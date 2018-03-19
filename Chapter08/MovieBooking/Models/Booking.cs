using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBooking.Models
{
    public class Booking
    {
        public Booking()
        {
            this.Tickets = new List<Ticket>();
        }


        public int Id { get; set; }

        public IList<Ticket> Tickets { get; set;}
    }
}
