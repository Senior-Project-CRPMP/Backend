using Backend.Models;
using Backend.Models.Account;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dto.Project
{
    public class TaskDto
    {
        public int Id { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? UserId { get; set; }
        public DateTime Deadline { get; set; }
        public string? Status { get; set; }
    }
}
