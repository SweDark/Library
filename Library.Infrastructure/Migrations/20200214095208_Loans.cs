using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Infrastructure.Migrations
{
    public partial class Loans : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoanDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    memberId = table.Column<int>(nullable: true),
                    bookCopyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Loans_Copies_bookCopyId",
                        column: x => x.bookCopyId,
                        principalTable: "Copies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Loans_Members_memberId",
                        column: x => x.memberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_bookCopyId",
                table: "Loans",
                column: "bookCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_memberId",
                table: "Loans",
                column: "memberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");
        }
    }
}
