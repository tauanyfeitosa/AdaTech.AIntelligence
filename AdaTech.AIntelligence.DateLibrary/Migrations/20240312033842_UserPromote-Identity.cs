using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaTech.AIntelligence.DateLibrary.Migrations
{
    /// <inheritdoc />
    public partial class UserPromoteIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Expenses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PromoteStatus",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "PromoteStatus",
                table: "AspNetUsers");
        }
    }
}
