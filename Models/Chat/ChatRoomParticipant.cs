using Backend.Models.Account;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class ChatRoomParticipant
    {
        public int? ChatRoomId { get; set; }
        public string UserId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ChatRoom? ChatRoom { get; set; }
        public virtual User? User { get; set; }
    }

}
