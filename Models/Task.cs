using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Task
    {
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project? Project { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? AssignedTo { get; set; }
        public DateTime Deadline { get; set; }
        public string? Status { get; set; }

        public ICollection<UserTask>? UserTasks { get; set; }
    }
}
