using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class updatedLoans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Copies_bookCopyId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_memberId",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "memberId",
                table: "Loans",
                newName: "MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_memberId",
                table: "Loans",
                newName: "IX_Loans_MemberId");

            migrationBuilder.AlterColumn<int>(
                name: "MemberId",
                table: "Loans",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "bookCopyId",
                table: "Loans",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Returned",
                table: "Loans",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Copies_bookCopyId",
                table: "Loans",
                column: "bookCopyId",
                principalTable: "Copies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Members_MemberId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Copies_bookCopyId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Returned",
                table: "Loans");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Loans",
                newName: "memberId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_MemberId",
                table: "Loans",
                newName: "IX_Loans_memberId");

            migrationBuilder.AlterColumn<int>(
                name: "bookCopyId",
                table: "Loans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "memberId",
                table: "Loans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Copies_bookCopyId",
                table: "Loans",
                column: "bookCopyId",
                principalTable: "Copies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Members_memberId",
                table: "Loans",
                column: "memberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
