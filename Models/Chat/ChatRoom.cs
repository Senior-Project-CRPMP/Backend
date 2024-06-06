using Backend.Models;

namespace Backend.Models
{
    public class ChatRoom
    {
        public int ChatRoomId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual ICollection<ChatRoomParticipant>? Participants { get; set; }
        public virtual ICollection<ChatMessage>? Messages { get; set; }
    }

}
