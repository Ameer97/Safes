using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Safes.DAL.Migrations
{
    public partial class CreatedDateInTableBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Boxes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Boxes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Boxes");

            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Boxes",
                type: "bytea",
                rowVersion: true,
                nullable: true);
        }
    }
}
