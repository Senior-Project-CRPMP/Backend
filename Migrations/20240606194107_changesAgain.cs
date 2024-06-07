using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class changesAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "FileUploads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_FileUploads_ProjectId",
                table: "FileUploads",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploads_Projects_ProjectId",
                table: "FileUploads",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploads_Projects_ProjectId",
                table: "FileUploads");

            migrationBuilder.DropIndex(
                name: "IX_FileUploads_ProjectId",
                table: "FileUploads");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "FileUploads");
        }
    }
}
