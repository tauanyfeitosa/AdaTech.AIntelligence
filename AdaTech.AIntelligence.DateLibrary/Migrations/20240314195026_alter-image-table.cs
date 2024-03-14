using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaTech.AIntelligence.DateLibrary.Migrations
{
    /// <inheritdoc />
    public partial class alterimagetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ByteImage",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "URLImage",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Images",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Images");

            migrationBuilder.AddColumn<byte[]>(
                name: "ByteImage",
                table: "Images",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URLImage",
                table: "Images",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
