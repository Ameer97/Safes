using Microsoft.EntityFrameworkCore.Migrations;

namespace Safes.DAL.Migrations
{
    public partial class ChangeIsActiveToIsDisabledInStaticBoxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "StaticBoxes");

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "StaticBoxes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "StaticBoxes");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "StaticBoxes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
