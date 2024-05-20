using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Dto.Form
{
    public class FormQuestionDto
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string? Type { get; set; }
        public string? Label { get; set; }
        public bool Required { get; set; }
        public bool IncludeComment { get; set; }
        public string? Comment { get; set; }
        public int MaxLength { get; set; }
        public int MaxUploadSize { get; set; }
        public string? AllowedTypes { get; set; }
    }
}
