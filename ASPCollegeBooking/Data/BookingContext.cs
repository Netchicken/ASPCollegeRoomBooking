using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCollegeBooking.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ASPCollegeBooking.Data
{
    //, IDesignTimeDbContextFactory<BookingContext>
    public class BookingContext : DbContext
    {
        public BookingContext(DbContextOptions<BookingContext> options) : base(options)
        {
        }

        protected BookingContext()
        {

        }
        public DbSet<Rooms> Rooms { get; set; }
        // public DbSet<RoomBooking> RoomBookings { get; set; }
        // public DbSet<CalendarDB> CalendarDbs { get; set; }
        public DbSet<Events> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = RoomBooking.db");
        }




        //https://docs.microsoft.com/en-us/ef/core/modeling/keys dont need it as its specified in class

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //set the primary key of the tables
        //    //modelBuilder.Entity<RoomBooking>()
        //    //    .HasKey(c => c.BookingID);
        //    //modelBuilder.Entity<Rooms>()
        //    //    .HasKey(c => c.ID);
        //    //modelBuilder.Entity<CalendarDB>()
        //    //    .HasKey(c => c.EventID);
        //}

        //https://docs.microsoft.com/en-us/ef/core/modeling/keys



        //public BookingContext CreateDbContext(string[] args)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
