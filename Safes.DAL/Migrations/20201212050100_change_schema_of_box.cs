using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Safes.DAL.Migrations
{
    public partial class change_schema_of_box : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Meditors_MeditorId",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "DateDeliverd",
                table: "Boxes");

            migrationBuilder.AlterColumn<int>(
                name: "MeditorId",
                table: "Boxes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "Amount",
                table: "Boxes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeliverdToMeditor",
                table: "Boxes",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeliverdToOwner",
                table: "Boxes",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Meditors_MeditorId",
                table: "Boxes",
                column: "MeditorId",
                principalTable: "Meditors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Meditors_MeditorId",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "DateDeliverdToMeditor",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "DateDeliverdToOwner",
                table: "Boxes");

            migrationBuilder.AlterColumn<int>(
                name: "MeditorId",
                table: "Boxes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Amount",
                table: "Boxes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeliverd",
                table: "Boxes",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Meditors_MeditorId",
                table: "Boxes",
                column: "MeditorId",
                principalTable: "Meditors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
