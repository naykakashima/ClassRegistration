using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassRegistrationApplication2025.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendedAtToRegistration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Classes_ClassId",
                table: "Surveys");

            migrationBuilder.AddColumn<DateTime>(
                name: "AttendedAt",
                table: "Registrations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Classes_ClassId",
                table: "Surveys",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Classes_ClassId",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "AttendedAt",
                table: "Registrations");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Classes_ClassId",
                table: "Surveys",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
