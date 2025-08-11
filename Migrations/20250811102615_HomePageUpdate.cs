using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace rzlist.Migrations
{
    /// <inheritdoc />
    public partial class HomePageUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Genre",
                table: "Books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "Books",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
