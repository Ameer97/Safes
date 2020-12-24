using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Safes.DAL.Migrations
{
    public partial class change_owner_schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "IsMale",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "IsStaticBox",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "IsStopped",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "SecondName",
                table: "Owners");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Owners",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Boxes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Boxes");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Owners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BirthYear",
                table: "Owners",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "Owners",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Owners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMale",
                table: "Owners",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStaticBox",
                table: "Owners",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsStopped",
                table: "Owners",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Owners",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondName",
                table: "Owners",
                type: "text",
                nullable: true);
        }
    }
}
