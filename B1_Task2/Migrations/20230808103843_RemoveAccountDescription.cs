using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B1_Task2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAccountDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountDescription",
                table: "DataEntries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountDescription",
                table: "DataEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
