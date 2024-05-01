namespace Backend.Models
{
    public class RoleModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<UserModel>? Users { get; set; }
    }
}
