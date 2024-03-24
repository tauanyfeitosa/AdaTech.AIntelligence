using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaTech.AIntelligence.DbLibrary.Migrations
{
    /// <inheritdoc />
    public partial class atualizaNomeDosCampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "RoleRequirements",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatAt",
                table: "RoleRequirements",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Expenses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatAt",
                table: "Expenses",
                newName: "CreatedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "RoleRequirements",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "RoleRequirements",
                newName: "CreatAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Expenses",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Expenses",
                newName: "CreatAt");
        }
    }
}
