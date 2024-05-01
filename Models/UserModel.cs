namespace Backend.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleModel Role { get; set; }
        public ICollection<UserProject> UserProjects { get; set; }
        public ICollection<UserTask> UserTasks { get; set; }

    }
}
