using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBooking.Models
{
    public class Ticket
    {
        public Ticket()
        {
            this.SeatIds = new List<int>();
        }

        public int ShowId { get; set; }
        
        public List<int> SeatIds { get; set; }

        public int Qunatity => this.SeatIds == null ? 0 : this.SeatIds.Count;
    }
}
