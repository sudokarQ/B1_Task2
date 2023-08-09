using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1_Task2.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    AccountCode = table.Column<int>(type: "int", nullable: false),
                    AccountDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeginningDebitBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BeginningCreditBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitTurnover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreditTurnover = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EndingDebitBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    EndingCreditBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DataEntries_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataEntries_FileId",
                table: "DataEntries",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_BankId",
                table: "Files",
                column: "BankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataEntries");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Banks");
        }
    }
}
