using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwizzez.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionsNumberToQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionsNumber",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionsNumber",
                table: "Quizzes");
        }
    }
}
