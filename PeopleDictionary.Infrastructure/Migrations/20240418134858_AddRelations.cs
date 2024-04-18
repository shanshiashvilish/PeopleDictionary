using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeopleDictionary.Infrastructure.Migrations
{
    public partial class AddRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_related_people_people_PersonId",
                table: "related_people");

            migrationBuilder.DropForeignKey(
                name: "FK_related_people_people_PersonId1",
                table: "related_people");

            migrationBuilder.DropIndex(
                name: "IX_related_people_PersonId1",
                table: "related_people");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "related_people");

            migrationBuilder.AddForeignKey(
                name: "FK_related_people_people_PersonId",
                table: "related_people",
                column: "PersonId",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_related_people_people_PersonId",
                table: "related_people");

            migrationBuilder.AddColumn<int>(
                name: "PersonId1",
                table: "related_people",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_related_people_PersonId1",
                table: "related_people",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_related_people_people_PersonId",
                table: "related_people",
                column: "PersonId",
                principalTable: "people",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_related_people_people_PersonId1",
                table: "related_people",
                column: "PersonId1",
                principalTable: "people",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
