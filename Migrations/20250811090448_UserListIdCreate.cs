using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rzlist.Migrations
{
    /// <inheritdoc />
    public partial class UserListIdCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserListId",
                table: "UserBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBooks_UserListId",
                table: "UserBooks",
                column: "UserListId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLists_UserId",
                table: "UserLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBooks_UserLists_UserListId",
                table: "UserBooks",
                column: "UserListId",
                principalTable: "UserLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBooks_UserLists_UserListId",
                table: "UserBooks");

            migrationBuilder.DropTable(
                name: "UserLists");

            migrationBuilder.DropIndex(
                name: "IX_UserBooks_UserListId",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "UserListId",
                table: "UserBooks");
        }
    }
}
