using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class members : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_BookDetails_DetailsID",
                table: "Copies");

            migrationBuilder.RenameColumn(
                name: "DetailsID",
                table: "Copies",
                newName: "DetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Copies_DetailsID",
                table: "Copies",
                newName: "IX_Copies_DetailsId");

            migrationBuilder.AlterColumn<int>(
                name: "DetailsId",
                table: "Copies",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_BookDetails_DetailsId",
                table: "Copies",
                column: "DetailsId",
                principalTable: "BookDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_BookDetails_DetailsId",
                table: "Copies");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.RenameColumn(
                name: "DetailsId",
                table: "Copies",
                newName: "DetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_Copies_DetailsId",
                table: "Copies",
                newName: "IX_Copies_DetailsID");

            migrationBuilder.AlterColumn<int>(
                name: "DetailsID",
                table: "Copies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_BookDetails_DetailsID",
                table: "Copies",
                column: "DetailsID",
                principalTable: "BookDetails",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
