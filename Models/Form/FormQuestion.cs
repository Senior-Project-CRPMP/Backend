using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Form
{
    public class FormQuestion
    {
        public int Id { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
        public Form? Form { get; set; }
        public string? Type { get; set; }
        public string? Label { get; set; }
        public bool Required { get; set; }
        public bool IncludeComment { get; set; }
        public string? Comment { get; set; }
        public int MaxLength { get; set; }
        public int MaxUploadSize { get; set; }
        public string? AllowedTypes { get; set; }

        public virtual ICollection<FormOption>? FormOptions { get; set; }
        public virtual ICollection<FormAnswer>? FormAnswers { get; set; }
    }

}
