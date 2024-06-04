using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class fight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomParticipant_ChatRooms_ChatRoomId",
                table: "ChatRoomParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_AspNetUsers_UserId",
                table: "UserProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatRoomParticipant",
                table: "ChatRoomParticipant");

            migrationBuilder.RenameTable(
                name: "ChatRoomParticipant",
                newName: "ChatRoomParticipants");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRoomParticipant_ChatRoomId",
                table: "ChatRoomParticipants",
                newName: "IX_ChatRoomParticipants_ChatRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatRoomParticipants",
                table: "ChatRoomParticipants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomParticipants_ChatRooms_ChatRoomId",
                table: "ChatRoomParticipants",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_AspNetUsers_UserId",
                table: "UserProjects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomParticipants_ChatRooms_ChatRoomId",
                table: "ChatRoomParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_AspNetUsers_UserId",
                table: "UserProjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatRoomParticipants",
                table: "ChatRoomParticipants");

            migrationBuilder.RenameTable(
                name: "ChatRoomParticipants",
                newName: "ChatRoomParticipant");

            migrationBuilder.RenameIndex(
                name: "IX_ChatRoomParticipants_ChatRoomId",
                table: "ChatRoomParticipant",
                newName: "IX_ChatRoomParticipant_ChatRoomId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatRoomParticipant",
                table: "ChatRoomParticipant",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomParticipant_ChatRooms_ChatRoomId",
                table: "ChatRoomParticipant",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_AspNetUsers_UserId",
                table: "UserProjects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
