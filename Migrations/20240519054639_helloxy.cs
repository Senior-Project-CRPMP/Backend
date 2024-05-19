using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class helloxy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormLinkQuestions");

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "FormQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "FormQuestions");

            migrationBuilder.CreateTable(
                name: "FormLinkQuestions",
                columns: table => new
                {
                    FormId = table.Column<int>(type: "int", nullable: false),
                    FormQuestionId = table.Column<int>(type: "int", nullable: false),
                    FormQuestionId1 = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    displayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormLinkQuestions", x => new { x.FormId, x.FormQuestionId });
                    table.ForeignKey(
                        name: "FK_FormLinkQuestions_FormQuestions_FormQuestionId",
                        column: x => x.FormQuestionId,
                        principalTable: "FormQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormLinkQuestions_FormQuestions_FormQuestionId1",
                        column: x => x.FormQuestionId1,
                        principalTable: "FormQuestions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FormLinkQuestions_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormLinkQuestions_FormQuestionId",
                table: "FormLinkQuestions",
                column: "FormQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormLinkQuestions_FormQuestionId1",
                table: "FormLinkQuestions",
                column: "FormQuestionId1");
        }
    }
}
