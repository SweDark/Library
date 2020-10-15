using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class copies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookCopy_BookDetails_DetailsID",
                table: "BookCopy");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy");

            migrationBuilder.RenameTable(
                name: "BookCopy",
                newName: "Copies");

            migrationBuilder.RenameIndex(
                name: "IX_BookCopy_DetailsID",
                table: "Copies",
                newName: "IX_Copies_DetailsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Copies",
                table: "Copies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_BookDetails_DetailsID",
                table: "Copies",
                column: "DetailsID",
                principalTable: "BookDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_BookDetails_DetailsID",
                table: "Copies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Copies",
                table: "Copies");

            migrationBuilder.RenameTable(
                name: "Copies",
                newName: "BookCopy");

            migrationBuilder.RenameIndex(
                name: "IX_Copies_DetailsID",
                table: "BookCopy",
                newName: "IX_BookCopy_DetailsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCopy",
                table: "BookCopy",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookCopy_BookDetails_DetailsID",
                table: "BookCopy",
                column: "DetailsID",
                principalTable: "BookDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
