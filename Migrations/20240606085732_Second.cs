using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_AspNetUsers_UserId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomParticipants_AspNetUsers_UserId",
                table: "ChatRoomParticipants");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "ChatRoomParticipants",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatRoomParticipants_UserId1",
                table: "ChatRoomParticipants",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_AspNetUsers_UserId",
                table: "ChatMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                table: "ChatMessages",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "ChatRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomParticipants_AspNetUsers_UserId",
                table: "ChatRoomParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomParticipants_AspNetUsers_UserId1",
                table: "ChatRoomParticipants",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_AspNetUsers_UserId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                table: "ChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomParticipants_AspNetUsers_UserId",
                table: "ChatRoomParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomParticipants_AspNetUsers_UserId1",
                table: "ChatRoomParticipants");

            migrationBuilder.DropIndex(
                name: "IX_ChatRoomParticipants_UserId1",
                table: "ChatRoomParticipants");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "ChatRoomParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_AspNetUsers_UserId",
                table: "ChatMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMessages_ChatRooms_ChatRoomId",
                table: "ChatMessages",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "ChatRoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomParticipants_AspNetUsers_UserId",
                table: "ChatRoomParticipants",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
