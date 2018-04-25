﻿// <auto-generated />
using ASPCollegeBooking.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace ASPCollegeBooking.Migrations
{
    [DbContext(typeof(BookingContext))]
    [Migration("20180420003718_newIDInc")]
    partial class newIDInc
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASPCollegeBooking.Models.CalendarDB", b =>
                {
                    b.Property<string>("EventID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("End");

                    b.Property<bool>("IsFullDay");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Subject");

                    b.Property<string>("ThemeColor");

                    b.Property<string>("delete");

                    b.HasKey("EventID");

                    b.ToTable("CalendarDbs");
                });

            modelBuilder.Entity("ASPCollegeBooking.Models.Events", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Days");

                    b.Property<DateTime>("End");

                    b.Property<string>("EventColor");

                    b.Property<int>("IdInc")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsFullDay");

                    b.Property<string>("ResourceId");

                    b.Property<string>("RoomID");

                    b.Property<DateTime>("Start");

                    b.Property<string>("Title");

                    b.Property<int>("Weeks");

                    b.HasKey("ID");

                    b.HasIndex("RoomID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("ASPCollegeBooking.Models.RoomBooking", b =>
                {
                    b.Property<string>("BookingID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientIDFK");

                    b.Property<int>("Days");

                    b.Property<DateTime>("From");

                    b.Property<bool>("IsFullDay");

                    b.Property<string>("Rooms");

                    b.Property<string>("ThemeColor");

                    b.Property<DateTime>("To");

                    b.Property<int>("Weeks");

                    b.HasKey("BookingID");

                    b.HasIndex("Rooms");

                    b.ToTable("RoomBookings");
                });

            modelBuilder.Entity("ASPCollegeBooking.Models.Rooms", b =>
                {
                    b.Property<string>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EventColor");

                    b.Property<string>("Title");

                    b.HasKey("ID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("ASPCollegeBooking.Models.Events", b =>
                {
                    b.HasOne("ASPCollegeBooking.Models.Rooms", "Room")
                        .WithMany()
                        .HasForeignKey("RoomID");
                });

            modelBuilder.Entity("ASPCollegeBooking.Models.RoomBooking", b =>
                {
                    b.HasOne("ASPCollegeBooking.Models.Rooms", "Room")
                        .WithMany()
                        .HasForeignKey("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
