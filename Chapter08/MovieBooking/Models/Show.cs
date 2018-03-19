using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieBooking.Models
{
    public class Show
    {
        public int Id { get; set; }

        public int AuditoriumId { get; set; }

        public int MovieId { get; set; }

        public TimeSpan Timing { get; set; }

        public IList<Seat> AvailableSeats { get; set; }
        
        public IList<Seat> BookedSeats { get; set; }

        public int TotalSeats { get; set; }
    }
}
