using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwizzez.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Quizzes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Quizzes");
        }
    }
}
