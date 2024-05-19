using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormQuestions_FormQuestionId",
                table: "FormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormResponses_FormResponseId",
                table: "FormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_FormResponses_Forms_FormId",
                table: "FormResponses");

            migrationBuilder.AddColumn<int>(
                name: "FormOptionId",
                table: "FormAnswers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormAnswers_FormOptionId",
                table: "FormAnswers",
                column: "FormOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormOptions_FormOptionId",
                table: "FormAnswers",
                column: "FormOptionId",
                principalTable: "FormOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormQuestions_FormQuestionId",
                table: "FormAnswers",
                column: "FormQuestionId",
                principalTable: "FormQuestions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormResponses_FormResponseId",
                table: "FormAnswers",
                column: "FormResponseId",
                principalTable: "FormResponses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormResponses_Forms_FormId",
                table: "FormResponses",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormOptions_FormOptionId",
                table: "FormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormQuestions_FormQuestionId",
                table: "FormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_FormAnswers_FormResponses_FormResponseId",
                table: "FormAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_FormResponses_Forms_FormId",
                table: "FormResponses");

            migrationBuilder.DropIndex(
                name: "IX_FormAnswers_FormOptionId",
                table: "FormAnswers");

            migrationBuilder.DropColumn(
                name: "FormOptionId",
                table: "FormAnswers");

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormQuestions_FormQuestionId",
                table: "FormAnswers",
                column: "FormQuestionId",
                principalTable: "FormQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormAnswers_FormResponses_FormResponseId",
                table: "FormAnswers",
                column: "FormResponseId",
                principalTable: "FormResponses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormResponses_Forms_FormId",
                table: "FormResponses",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
