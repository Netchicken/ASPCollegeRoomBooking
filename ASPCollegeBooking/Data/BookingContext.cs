using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPCollegeBooking.Data
{
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }
        public DbSet<Rooms> Rooms { get; set; }
        public DbSet<RoomBooking> RoomBookings { get; set; }
        public DbSet<CalendarDB> CalendarDbs { get; set; }

        //https://docs.microsoft.com/en-us/ef/core/modeling/keys

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set the primary key of the tables
            modelBuilder.Entity<RoomBooking>()
                .HasKey(c => c.BookingID);
            modelBuilder.Entity<Rooms>()
                .HasKey(c => c.ID);
            modelBuilder.Entity<CalendarDB>()
                .HasKey(c => c.EventID);
        }

        //https://docs.microsoft.com/en-us/ef/core/modeling/keys

        public DbSet<ASPCollegeBooking.Models.Events> Events { get; set; }
    }
}
