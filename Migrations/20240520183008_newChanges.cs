using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class newChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InputType",
                table: "FormQuestions",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "InputLabel",
                table: "FormQuestions",
                newName: "Label");

            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "FormQuestions",
                newName: "MaxUploadSize");

            migrationBuilder.RenameColumn(
                name: "OptionText",
                table: "FormOptions",
                newName: "label");

            migrationBuilder.AddColumn<string>(
                name: "AllowedTypes",
                table: "FormQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "FormQuestions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncludeComment",
                table: "FormQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MaxLength",
                table: "FormQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "FormQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowedTypes",
                table: "FormQuestions");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "FormQuestions");

            migrationBuilder.DropColumn(
                name: "IncludeComment",
                table: "FormQuestions");

            migrationBuilder.DropColumn(
                name: "MaxLength",
                table: "FormQuestions");

            migrationBuilder.DropColumn(
                name: "Required",
                table: "FormQuestions");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "FormQuestions",
                newName: "InputType");

            migrationBuilder.RenameColumn(
                name: "MaxUploadSize",
                table: "FormQuestions",
                newName: "DisplayOrder");

            migrationBuilder.RenameColumn(
                name: "Label",
                table: "FormQuestions",
                newName: "InputLabel");

            migrationBuilder.RenameColumn(
                name: "label",
                table: "FormOptions",
                newName: "OptionText");
        }
    }
}
