using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.FileUpload
{
    public class FileUploadDto
    {
        public IFormFile? File { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("Project")]
        public int ProjectId { get; set; }
    }
}
