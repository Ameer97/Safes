using Microsoft.EntityFrameworkCore.Migrations;

namespace Safes.DAL.Migrations
{
    public partial class AddingEventToBoxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Boxes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_EventId",
                table: "Boxes",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_Events_EventId",
                table: "Boxes",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_Events_EventId",
                table: "Boxes");

            migrationBuilder.DropIndex(
                name: "IX_Boxes_EventId",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Boxes");
        }
    }
}
