using Backend.Models.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Project
{
    public class UserProject
    {
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public string? Role { get; set; }
    }
}
