using Microsoft.EntityFrameworkCore.Migrations;

namespace FinAppApi.Migrations
{
    public partial class FinAppDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "user");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "user",
                type: "varchar(200) CHARACTER SET utf8mb4",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }
    }
}
