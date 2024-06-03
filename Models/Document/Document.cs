using Backend.Models.Project;

namespace Backend.Models.Document
{
    public class Document
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Data { get; set; }
        public int? ProjectId { get; set; }
        public virtual Models.Project.Project? Project { get; set; }
    }
    
}
