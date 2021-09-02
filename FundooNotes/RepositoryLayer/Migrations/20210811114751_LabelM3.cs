using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class LabelM3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Label_FondooNotes_Userid",
                table: "Label");

            migrationBuilder.DropIndex(
                name: "IX_Label_Userid",
                table: "Label");

            migrationBuilder.RenameColumn(
                name: "Userid",
                table: "Label",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Label",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoteId",
                table: "Label",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoteId",
                table: "Label");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Label",
                newName: "Userid");

            migrationBuilder.AlterColumn<int>(
                name: "Userid",
                table: "Label",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Label_Userid",
                table: "Label",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Label_FondooNotes_Userid",
                table: "Label",
                column: "Userid",
                principalTable: "FondooNotes",
                principalColumn: "Userid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
