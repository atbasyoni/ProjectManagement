using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddBlockUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "BlockedUser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlockerID = table.Column<int>(type: "int", nullable: false),
                    BlockedID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockedUser", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BlockedUser_Users_BlockedID",
                        column: x => x.BlockedID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlockedUser_Users_BlockerID",
                        column: x => x.BlockerID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUser_BlockedID",
                table: "BlockedUser",
                column: "BlockedID");

            migrationBuilder.CreateIndex(
                name: "IX_BlockedUser_BlockerID",
                table: "BlockedUser",
                column: "BlockerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockedUser");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Users");
        }
    }
}
