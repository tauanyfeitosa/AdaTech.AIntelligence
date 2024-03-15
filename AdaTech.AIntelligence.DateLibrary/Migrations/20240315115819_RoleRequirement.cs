using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdaTech.AIntelligence.DateLibrary.Migrations
{
    /// <inheritdoc />
    public partial class RoleRequirement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserInfoId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleRequirements_AspNetUsers_UserInfoId",
                        column: x => x.UserInfoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleRequirements_UserInfoId",
                table: "RoleRequirements",
                column: "UserInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleRequirements");
        }
    }
}
