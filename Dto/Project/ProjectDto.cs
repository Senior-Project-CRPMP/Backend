using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dto.Project
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public string? UserId { get; set; }
    }
}
