using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleDictionary.Infrastructure.Migrations
{
    public partial class adjustnullableprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_people_PersonalId",
                table: "people",
                column: "PersonalId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_people_PersonalId",
                table: "people");
        }
    }
}
