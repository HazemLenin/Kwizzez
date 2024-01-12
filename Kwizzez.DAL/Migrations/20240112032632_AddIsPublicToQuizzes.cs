using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwizzez.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPublicToQuizzes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Quizzes");
        }
    }
}
