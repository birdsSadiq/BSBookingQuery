using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Web.Data.Migrations
{
    public partial class Demo_modify : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demo_AppUser_AppUserId",
                table: "Demo");

            migrationBuilder.DropIndex(
                name: "IX_Demo_AppUserId",
                table: "Demo");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Demo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "Demo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Demo_AppUserId",
                table: "Demo",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Demo_AppUser_AppUserId",
                table: "Demo",
                column: "AppUserId",
                principalTable: "AppUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
