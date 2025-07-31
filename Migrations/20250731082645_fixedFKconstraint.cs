using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassRegistrationApplication2025.Migrations
{
    /// <inheritdoc />
    public partial class fixedFKconstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Surveys_SurveyId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Surveys_SurveyId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_SurveyId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Classes_SurveyId",
                table: "Classes");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_ClassId",
                table: "Surveys",
                column: "ClassId",
                unique: true,
                filter: "[ClassId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_SubjectId",
                table: "Surveys",
                column: "SubjectId",
                unique: true,
                filter: "[SubjectId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Classes_ClassId",
                table: "Surveys",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Subjects_SubjectId",
                table: "Surveys",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Classes_ClassId",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Subjects_SubjectId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_ClassId",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_SubjectId",
                table: "Surveys");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SurveyId",
                table: "Subjects",
                column: "SurveyId",
                unique: true,
                filter: "[SurveyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SurveyId",
                table: "Classes",
                column: "SurveyId",
                unique: true,
                filter: "[SurveyId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Surveys_SurveyId",
                table: "Classes",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Surveys_SurveyId",
                table: "Subjects",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
