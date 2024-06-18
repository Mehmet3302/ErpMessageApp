using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Erp___Kurum_Ici_Haberlesme_2.Data.Migrations
{
    public partial class MessageDurumGuncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MessageDurum",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageDurum",
                table: "Messages");
        }
    }
}
