using Backend.Models.Account;

namespace Backend.Models.Chat
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ChatMessage>? Messages { get; set; }
        public ICollection<User> Participants { get; set; }
    }
}
