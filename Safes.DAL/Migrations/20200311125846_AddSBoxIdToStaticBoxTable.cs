using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Safes.DAL.Migrations
{
    public partial class AddSBoxIdToStaticBoxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFrom",
                table: "StaticBoxReuses",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "StaticBoxReuses",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<int>(
                name: "SBoxId",
                table: "StaticBoxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StaticBoxes_SBoxId",
                table: "StaticBoxes",
                column: "SBoxId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StaticBoxes_SBoxId",
                table: "StaticBoxes");

            migrationBuilder.DropColumn(
                name: "SBoxId",
                table: "StaticBoxes");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFrom",
                table: "StaticBoxReuses",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "StaticBoxReuses",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }
    }
}
