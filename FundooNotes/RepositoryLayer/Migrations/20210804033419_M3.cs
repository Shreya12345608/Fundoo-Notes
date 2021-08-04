using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class M3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userid",
                table: "NotesDB");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountUserid",
                table: "NotesDB",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotesDB_UserAccountUserid",
                table: "NotesDB",
                column: "UserAccountUserid");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesDB_FondooNotes_UserAccountUserid",
                table: "NotesDB",
                column: "UserAccountUserid",
                principalTable: "FondooNotes",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesDB_FondooNotes_UserAccountUserid",
                table: "NotesDB");

            migrationBuilder.DropIndex(
                name: "IX_NotesDB_UserAccountUserid",
                table: "NotesDB");

            migrationBuilder.DropColumn(
                name: "UserAccountUserid",
                table: "NotesDB");

            migrationBuilder.AddColumn<string>(
                name: "userid",
                table: "NotesDB",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
