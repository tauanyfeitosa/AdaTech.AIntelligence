using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaTech.AIntelligence.DbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class removeSuperUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSuperUser",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "AspNetUsers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatAt",
                table: "AspNetUsers",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "AspNetUsers",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "AspNetUsers",
                newName: "CreatAt");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuperUser",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
