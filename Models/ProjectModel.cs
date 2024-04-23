namespace Backend.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty ;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public string? ManagerId { get; set; }

        public virtual ICollection<TaskModel> Tasks { get; set; }
    }
}
