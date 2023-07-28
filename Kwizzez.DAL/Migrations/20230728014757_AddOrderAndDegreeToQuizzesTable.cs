using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwizzez.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderAndDegreeToQuizzesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Degree",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Questions");
        }
    }
}
