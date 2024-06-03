using Backend.Models;

namespace Backend.Dto.Project
{
    public class TaskDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? AssignedTo { get; set; }
        public DateTime Deadline { get; set; }
        public string? Status { get; set; }
    }
}
