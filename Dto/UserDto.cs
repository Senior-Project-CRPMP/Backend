using Backend.Models;

namespace Backend.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleModel Role { get; set; }
        
    }
}
