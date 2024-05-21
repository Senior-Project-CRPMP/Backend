namespace Backend.Models.Chat
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ChatMessage>? Messages { get; set; }
        public IList<string> Participants { get; set; }
    }
}
