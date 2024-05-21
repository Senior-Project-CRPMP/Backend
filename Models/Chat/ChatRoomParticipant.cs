namespace Backend.Models.Chat
{
    public class ChatRoomParticipant
    {
        public int Id { get; set; }
        public int? ChatRoomId { get; set; }
        public ChatRoom? ChatRoom { get; set; }
        public string UserName { get; set; }
    }
}
