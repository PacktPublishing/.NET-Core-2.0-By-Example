using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBooking.Models
{
    public class Auditorium
    {
        public Auditorium()
        {
            this.Seats = new List<Seat>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual IList<Seat> Seats { get; set; }

        public int TotalSeats => this.Seats == null ? 0 : this.Seats.Count;
    }
}
