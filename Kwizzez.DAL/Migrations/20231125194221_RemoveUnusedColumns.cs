using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kwizzez.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnusedColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "HasLimitedTime",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Quizzes");

            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "Quizzes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Code",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "HasLimitedTime",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Quizzes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Quizzes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "TimeLimit",
                table: "Quizzes",
                type: "time",
                nullable: true);
        }
    }
}
