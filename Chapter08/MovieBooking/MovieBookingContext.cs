using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using MovieBooking.Models;

namespace MovieBooking
{
    public class MovieBookingContext : DbContext
    {
        public DbSet<Auditorium> Auditoriums { get; set; }
        
        public DbSet<Movie> Movies { get; set; }

        public DbSet<Show> Shows { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Ticket> Tickets { get; set; }

        public MovieBookingContext(DbContextOptions<MovieBookingContext> options) : base(options)
        {                
        }
    }
}
