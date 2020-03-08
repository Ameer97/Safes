using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Safes.DAL.Migrations
{
    public partial class FixesOnStaticBoxAndStaticBoxReuse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "StaticBoxes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTo",
                table: "StaticBoxReuses",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFrom",
                table: "StaticBoxReuses",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "StaticBoxReuses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MeditorId",
                table: "StaticBoxReuses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StaticBoxes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_StaticBoxReuses_MeditorId",
                table: "StaticBoxReuses",
                column: "MeditorId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaticBoxReuses_Meditors_MeditorId",
                table: "StaticBoxReuses",
                column: "MeditorId",
                principalTable: "Meditors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaticBoxReuses_Meditors_MeditorId",
                table: "StaticBoxReuses");

            migrationBuilder.DropIndex(
                name: "IX_StaticBoxReuses_MeditorId",
                table: "StaticBoxReuses");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "StaticBoxReuses");

            migrationBuilder.DropColumn(
                name: "MeditorId",
                table: "StaticBoxReuses");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StaticBoxes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTo",
                table: "StaticBoxReuses",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFrom",
                table: "StaticBoxReuses",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "StaticBoxes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
