using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.FileUpload
{
    public class FileUpload
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Models.Project.Project? Project { get; set; }
    }
}
