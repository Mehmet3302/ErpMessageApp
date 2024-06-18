using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp___Kurum_Ici_Haberlesme_2.Data.Migrations
{
    public partial class Guncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RolAd",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RolAd",
                table: "AspNetRoles");
        }
    }
}
