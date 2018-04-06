using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ASPCollegeBooking.Migrations
{
    public partial class RoomClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomID",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Events_RoomID",
                table: "Events",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Rooms_RoomID",
                table: "Events",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Rooms_RoomID",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_RoomID",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "Events");
        }
    }
}
