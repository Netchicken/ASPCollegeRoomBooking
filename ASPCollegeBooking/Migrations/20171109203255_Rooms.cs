using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ASPCollegeBooking.Migrations
{
    public partial class Rooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "delete",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Rooms");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rooms",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "delete",
                table: "Rooms",
                nullable: false,
                defaultValue: 0);
        }
    }
}
