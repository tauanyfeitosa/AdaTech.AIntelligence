using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaTech.AIntelligence.DbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UserExpense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserInfoId",
                table: "Expenses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserInfoId",
                table: "Expenses",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_AspNetUsers_UserInfoId",
                table: "Expenses",
                column: "UserInfoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_AspNetUsers_UserInfoId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserInfoId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Expenses");
        }
    }
}
