namespace Backend.Models
{
    public class TaskModel
    {
        public int Id { get; set; } 
        public int ProjectId { get; set; }
        public virtual ProjectModel? Project { get; set; }
        public string? Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? AssignedTo { get; set; }
        public DateTime Deadline { get; set; }
        public string? Status { get; set; }


    }
}
