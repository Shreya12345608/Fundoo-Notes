using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class Labels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_FondooNotes_Userid",
                table: "Label");

            migrationBuilder.DropForeignKey(
                name: "FK_Label_NotesDB_NotesId1",
                table: "Label");

            migrationBuilder.DropIndex(
                name: "IX_Label_NotesId1",
                table: "Label");

            migrationBuilder.DropColumn(
                name: "NotesId",
                table: "Label");

            migrationBuilder.DropColumn(
                name: "NotesId1",
                table: "Label");

            migrationBuilder.AlterColumn<int>(
                name: "Userid",
                table: "Label",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_FondooNotes_Userid",
                table: "Label",
                column: "Userid",
                principalTable: "FondooNotes",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_FondooNotes_Userid",
                table: "Label");

            migrationBuilder.AlterColumn<int>(
                name: "Userid",
                table: "Label",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotesId",
                table: "Label",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "NotesId1",
                table: "Label",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Label_NotesId1",
                table: "Label",
                column: "NotesId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_FondooNotes_Userid",
                table: "Label",
                column: "Userid",
                principalTable: "FondooNotes",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Label_NotesDB_NotesId1",
                table: "Label",
                column: "NotesId1",
                principalTable: "NotesDB",
                principalColumn: "NotesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
