using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BadgeSpace.Data.Migrations
{
    public partial class CPF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CPF",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPF",
                table: "AspNetUsers");
        }
    }
}
