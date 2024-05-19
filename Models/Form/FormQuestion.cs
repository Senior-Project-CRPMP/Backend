using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models.Form
{
    public class FormQuestion
    {
        public int Id { get; set; }
        [ForeignKey("Form")]
        public int FormId { get; set; }
        public Form? Form { get; set; }
        public string? InputType { get; set; }
        public string? InputLabel { get; set; }
        public int DisplayOrder { get; set; }

        public virtual ICollection<FormOption>? FormOptions { get; set; }
        public virtual ICollection<FormAnswer>? FormAnswers { get; set; }
    }

}
