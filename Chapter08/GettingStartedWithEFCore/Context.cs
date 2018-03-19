using System;
using System.Collections.Generic;
using System.Text;
using GettingStartedWithEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace GettingStartedWithEFCore
{
    public class Context : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //// Get the connection string from configuration 
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=PersonDatabase;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().Property(nameof(Person.Name)).IsRequired();
        }
    }
}
