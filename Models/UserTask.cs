namespace Backend.Models
{
    public class UserTask
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public UserModel User { get; set; }
        public TaskModel Task { get; set; }
    }
}
