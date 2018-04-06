using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ASPCollegeBooking.Migrations
{
    public partial class Rooms2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "delete",
                table: "RoomBookings");

            migrationBuilder.AddColumn<string>(
                name: "EventColor",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventColor",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "delete",
                table: "RoomBookings",
                nullable: false,
                defaultValue: 0);
        }
    }
}
