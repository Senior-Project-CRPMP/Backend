namespace Backend.Models
{
    public class UserProject
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public UserModel? User { get; set; }
        public Project? Project { get; set; }
    }
}
