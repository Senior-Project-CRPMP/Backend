namespace Backend.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; } = false;
    }

}
