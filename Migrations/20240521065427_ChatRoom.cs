using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class ChatRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomParticipant_ChatRooms_ChatRoomId",
                table: "ChatRoomParticipant");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatRoomParticipant");

            migrationBuilder.AlterColumn<int>(
                name: "ChatRoomId",
                table: "ChatRoomParticipant",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "ChatRoomParticipant",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomParticipant_ChatRooms_ChatRoomId",
                table: "ChatRoomParticipant",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatRoomParticipant_ChatRooms_ChatRoomId",
                table: "ChatRoomParticipant");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "ChatRoomParticipant");

            migrationBuilder.AlterColumn<int>(
                name: "ChatRoomId",
                table: "ChatRoomParticipant",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ChatRoomParticipant",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatRoomParticipant_ChatRooms_ChatRoomId",
                table: "ChatRoomParticipant",
                column: "ChatRoomId",
                principalTable: "ChatRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
