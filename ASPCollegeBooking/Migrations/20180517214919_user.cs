using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ASPCollegeBooking.Migrations
{
    public partial class user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Organizer",
                table: "Events",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Events",
                newName: "Organizer");
        }
    }
}
