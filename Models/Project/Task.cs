using Backend.Models.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Project
{
    public class Task
    {
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        [ForeignKey("User")]
        public string? UserId { get; set; }
        public virtual User? User { get; set; }
        public DateTime Deadline { get; set; }
        public string? Status { get; set; }

    }
}
